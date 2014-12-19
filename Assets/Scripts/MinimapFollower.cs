using UnityEngine;

namespace Assets.Scripts
{
    public class MinimapFollower : MonoBehaviour
    {
        #region Fields

        public Transform Target;

        #endregion

        #region Public Methods and Operators

        public void LateUpdate()
        {
            this.transform.position = new Vector3(this.Target.position.x, this.Target.position.y, this.Target.position.z);
        }

        #endregion
    }
}