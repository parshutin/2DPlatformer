using UnityEngine;

using UnitySampleAssets.CrossPlatformInput;
using UnitySampleAssets._2D;

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

        private Animator anim;

        private PlatformerCharacter2D playerCtrl;

        #endregion

        #region Methods

        private void Awake()
        {
            this.anim = this.transform.root.gameObject.GetComponent<Animator>();
            this.playerCtrl = this.transform.root.GetComponent<PlatformerCharacter2D>();
        }

        private void Shoot(Rigidbody2D rigidbody, Vector2 velocity, Vector3 rotation)
        {
            if (this.playerCtrl.facingRight)
            {
                var bullet = Instantiate(rigidbody, this.transform.position, Quaternion.Euler(rotation)) as Rigidbody2D;
                bullet.velocity = velocity;
            }
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                this.Shoot(
                    this.rocket,
                    this.playerCtrl.facingRight ? new Vector2(RoketSpeed, 0) : new Vector2(-RoketSpeed, 0),
                    this.playerCtrl.facingRight ? new Vector3(0, 0, 0) : new Vector3(0, 0, 180f));
            }

            if (CrossPlatformInputManager.GetButtonDown("Fire2"))
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