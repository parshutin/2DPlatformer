using Assets.Scripts;

using UnityEngine;

namespace Scripts
{
    public class PlatformMover : MonoBehaviour
    {
        #region Fields

        private Vector2 speed;

        #endregion

        #region Methods

        private void Awake()
        {
            this.speed = new Vector2(2, 0);
            this.rigidbody2D.velocity = this.speed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == Tags.Untagged)
            {
                this.speed *= -1;
                this.rigidbody2D.velocity = this.speed;
            }
        }

        #endregion
    }
}