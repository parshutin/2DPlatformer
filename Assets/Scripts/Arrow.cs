using UnityEngine;

namespace Assets.Scripts
{
    public class Arrow : MonoBehaviour
    {
        #region Fields

        private GameObject player;

        public float endWidth = 0.05f;

        public Material material;

        public float startWidth = 0.05f;

        private LineRenderer line;

        #endregion

        #region Methods

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == Tags.Wall)
            {
                this.rigidbody2D.isKinematic = true;
                var joint = this.player.GetComponent<SpringJoint2D>();
                joint.connectedBody = this.gameObject.rigidbody2D;
                joint.enabled = true;
            }
        }

        private void Start()
        {
            this.player = GameObject.Find("Player");
            this.line = this.gameObject.AddComponent<LineRenderer>();
            this.line.SetVertexCount(2);
            this.line.material = this.material;
            this.line.renderer.enabled = true;
        }

        private void Update()
        {
            this.line.SetPosition(0, this.gameObject.transform.position);
            this.line.SetPosition(1, this.player.transform.position);
        }

        #endregion
    }
}