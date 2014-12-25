using UnityEngine;

using UnitySampleAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class Gun : MonoBehaviour
    {
        #region Constants

        private const float BombSpeed = 5f;

        private const float RoketSpeed = 20f;

        #endregion

        #region Fields

        public Rigidbody2D bomb;

        public Rigidbody2D rocket;

        private UserControl playerCtrl;

        #endregion

        #region Methods

        private void Awake()
        {
            this.playerCtrl = this.transform.root.GetComponent<UserControl>();
        }

        private void Shoot(Rigidbody2D rigidbody, Vector2 velocity, Vector3 rotation)
        {
            var bullet = Instantiate(rigidbody, this.transform.position, Quaternion.Euler(rotation)) as Rigidbody2D;
            bullet.velocity = velocity;
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown(Buttons.RocketFire))
            {
                this.Shoot(
                    this.rocket,
                    this.playerCtrl.facingRight ? new Vector2(RoketSpeed, 0) : new Vector2(-RoketSpeed, 0),
                    this.playerCtrl.facingRight ? new Vector3(0, 0, 0) : new Vector3(0, 0, 180f));
            }

            if (CrossPlatformInputManager.GetButtonDown(Buttons.BombFire))
            {
                this.Shoot(
                    this.bomb,
                    this.playerCtrl.facingRight ? new Vector2(BombSpeed, BombSpeed) : new Vector2(-BombSpeed, BombSpeed),
                    this.playerCtrl.facingRight ? new Vector3(0, 0, 0) : new Vector3(0, 0, 180f));
            }
        }

        #endregion
    }
}