using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        #region Fields

        public Vector2 maxXAndY;

        public Vector2 minXAndY;

        public float xMargin = 1f;

        public float xSmooth = 8f;

        public float yMargin = 1f;

        public float ySmooth = 8f;

        private Transform player;

        #endregion

        #region Methods

        private void Awake()
        {
           this.player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private bool CheckXMargin()
        {
            return Mathf.Abs(this.transform.position.x - this.player.position.x) > this.xMargin;
        }

        private bool CheckYMargin()
        {
            return Mathf.Abs(this.transform.position.y - this.player.position.y) > this.yMargin;
        }

        private void FixedUpdate()
        {
            this.TrackPlayer();
        }

        private void TrackPlayer()
        {
            float targetX = this.transform.position.x;
            float targetY = this.transform.position.y;
            if (this.CheckXMargin())
            {
                targetX = Mathf.Lerp(this.transform.position.x, this.player.position.x, this.xSmooth * Time.deltaTime);
            }

            if (this.CheckYMargin())
            {
                targetY = Mathf.Lerp(this.transform.position.y, this.player.position.y, this.ySmooth * Time.deltaTime);
            }

            targetX = Mathf.Clamp(targetX, this.minXAndY.x, this.maxXAndY.x);
            targetY = Mathf.Clamp(targetY, this.minXAndY.y, this.maxXAndY.y);
            this.transform.position = new Vector3(targetX, targetY, this.transform.position.z);
        }

        #endregion
    }
}