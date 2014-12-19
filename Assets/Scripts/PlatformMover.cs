using UnityEngine;

namespace Scripts
{
    public class PlatformMover : MonoBehaviour
    {
        #region Fields

        private Vector3 speed;

        #endregion

        #region Methods

        private void Awake()
        {
            this.speed = new Vector2(5, 0);
            this.rigidbody2D.velocity = this.speed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Untagged")
            {
                this.speed *= -1;
                this.rigidbody2D.velocity = this.speed;
            }
        }

        #endregion
    }
}