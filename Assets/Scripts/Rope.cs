using UnityEngine;

namespace Assets.Scripts
{
    public class Rope : MonoBehaviour
    {
        public GameObject fragmentPrefab;

        int fragmentNum = 3;

        GameObject[] fragments;

        float activeFragmentNum = 3;

        Vector3 interval = new Vector3(0f, 0f, 0.25f);

        float[] xPositions;
        float[] yPositions;
        float[] zPositions;

        CatmullRomSpline splineX;
        CatmullRomSpline splineY;
        CatmullRomSpline splineZ;

        int splineFactor = 4;

        void Start()
        {
            fragments = new GameObject[fragmentNum];

            Vector3 position = Vector3.zero;

            for (int i = 0; i < fragmentNum; i++)
            {
                fragments[i] = (GameObject)Instantiate(fragmentPrefab, position, Quaternion.identity);
                fragments[i].transform.parent = transform;

                SpringJoint2D joint = fragments[i].GetComponent<SpringJoint2D>();
                if (i > 0)
                {
                    joint.connectedBody = fragments[i - 1].rigidbody2D;
                }
                else
                {
                    var arrow = GameObject.FindGameObjectWithTag("Arrow");
                    fragments[i].transform.parent = arrow.transform;
                    joint.connectedBody = arrow.rigidbody2D;
                }

                position += interval;
            }

            LineRenderer renderer = GetComponent<LineRenderer>();
            renderer.SetVertexCount((fragmentNum - 1) * splineFactor + 1);

            xPositions = new float[fragmentNum];
            yPositions = new float[fragmentNum];
            zPositions = new float[fragmentNum];

            splineX = new CatmullRomSpline(xPositions);
            splineY = new CatmullRomSpline(yPositions);
            splineZ = new CatmullRomSpline(zPositions);
        }

        void Update()
        {
            float vy = Input.GetAxisRaw("Vertical") * 20f * Time.deltaTime;
            activeFragmentNum = Mathf.Clamp(activeFragmentNum + vy, 0, fragmentNum);

            for (int i = 0; i < fragmentNum; i++)
            {
                if (i <= fragmentNum - activeFragmentNum)
                {
                    fragments[i].rigidbody2D.position = Vector3.zero;
                    fragments[i].rigidbody2D.isKinematic = true;
                }
                else
                {
                    fragments[i].rigidbody2D.isKinematic = false;
                }
            }
        }

        void LateUpdate()
        {
           LineRenderer renderer = GetComponent<LineRenderer>();
            int i;
            for (i = 0; i < fragmentNum; i++)
            {
                Vector3 position = fragments[i].transform.position;
                xPositions[i] = position.x;
                yPositions[i] = position.y;
                zPositions[i] = position.z;
            }

            for (i = 0; i < (fragmentNum - 1) * splineFactor + 1; i++)
            {
                renderer.SetPosition(i, new Vector3(
                    splineX.GetValue(i / (float)splineFactor),
                    splineY.GetValue(i / (float)splineFactor),
                    splineZ.GetValue(i / (float)splineFactor)));
            }
        }
    }
}
