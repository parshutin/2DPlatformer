using UnityEngine;

namespace Assets.Scripts
{
    public class Arrow : MonoBehaviour
    {
        // Line start width
        public float startWidth = 0.05f;
        // Line end width
        public float endWidth = 0.05f;

        private LineRenderer line;

        public Material material;

        #region Methods

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == Tags.Wall)
            {
                this.rigidbody2D.isKinematic = true;
                var player = GameObject.Find("Player");
                SpringJoint2D joint = player.GetComponent<SpringJoint2D>();
                joint.connectedBody = this.gameObject.rigidbody2D;
                joint.enabled = true;
            }
        }

        void Start()
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.SetVertexCount(2);
            line.material = material;
            //we need to see the line... 
            line.renderer.enabled = true;
        }

        void Update()
        {
            //get the shooter object...
            var bob = GameObject.Find("Player");
            //set starting point of line to this object, in this case the grappling hook prefab
            line.SetPosition(0, this.gameObject.transform.position);
            //set the ending point of the line to the shooter object
            line.SetPosition(1, bob.transform.position);
        }

        #endregion
    }
}