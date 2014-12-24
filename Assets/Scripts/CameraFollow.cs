using UnityEngine;

namespace Assets.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        #region Fields

        private Vector2 maxXAndY = new Vector2(80,34);

        private Vector2 minXAndY = new Vector2(-30, -34);

        private const float xMargin = 1f;

        private const float xSmooth = 8f;

        private const float yMargin = 1f;

        private const float ySmooth = 8f;

        private Transform player;

        #endregion

        #region Methods

        private void Awake()
        {
           this.player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        }

        private bool CheckXMargin()
        {
            return Mathf.Abs(this.transform.position.x - this.player.position.x) > xMargin;
        }

        private bool CheckYMargin()
        {
            return Mathf.Abs(this.transform.position.y - this.player.position.y) > yMargin;
        }

        private void Update()
        {
            this.TrackPlayer();
        }

        private void TrackPlayer()
        {
            float targetX = this.transform.position.x;
            float targetY = this.transform.position.y;
            if (this.CheckXMargin())
            {
                targetX = Mathf.Lerp(this.transform.position.x, this.player.position.x, xSmooth * Time.deltaTime);
            }

            if (this.CheckYMargin())
            {
                targetY = Mathf.Lerp(this.transform.position.y, this.player.position.y, ySmooth * Time.deltaTime);
            }

            targetX = Mathf.Clamp(targetX, this.minXAndY.x, this.maxXAndY.x);
            targetY = Mathf.Clamp(targetY, this.minXAndY.y, this.maxXAndY.y);
            this.transform.position = new Vector3(targetX, targetY, this.transform.position.z);
        }

        #endregion
    }
}