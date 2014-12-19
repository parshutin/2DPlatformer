using UnityEngine;

namespace Assets.Scripts
{
    public class FollowPlayer : MonoBehaviour
    {
        #region Fields

        public GameObject Player;

        public Vector3 offset;

        #endregion

        #region Methods

        private void Update()
        {
            this.transform.position = this.Player.transform.position + this.offset;
        }

        #endregion
    }
}