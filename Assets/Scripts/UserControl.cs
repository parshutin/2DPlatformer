using UnityEngine;

using UnitySampleAssets.CrossPlatformInput;

namespace Assets.Scripts
{
    public class UserControl : MonoBehaviour
    {
        #region Fields

        public bool jump;

        public bool facingRight = true;

        private bool airControl;

        private Animator anim;

        private const float checkRadius = .2f;

        private const float crouchSpeed = .36f;

        private Transform groundCheck;

        [SerializeField]
        private LayerMask groundLayerMask;

        private bool grounded;

        private bool onMovingPlatform;

        private const float jumpForce = 400f;

        public float moveForce = 365f;

        private const float maxSpeed = 10f;

        private Vector2 movingPlatformVelocity;

        #endregion

        #region Methods

        private void Awake()
        {
            this.groundCheck = this.transform.Find("GroundCheck");
            this.anim = this.GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            this.Move(h, crouch);
            this.jump = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Platform")
            {
                this.onMovingPlatform = true;
                movingPlatformVelocity = col.rigidbody2D.velocity;
            }
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.tag == "Platform")
            {
                movingPlatformVelocity = col.rigidbody2D.velocity;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.tag == "Platform")
            {
                this.onMovingPlatform = false;
            }
        }
        
        private void Update()
        {
            if (!this.jump)
            {
                this.jump = CrossPlatformInputManager.GetButtonDown(Buttons.Jump);
            }

            this.grounded = Physics2D.OverlapCircle(this.groundCheck.position, checkRadius, this.groundLayerMask);
        }

        public void Move(float move, bool crouch)
        {
            move = (crouch ? move : move * crouchSpeed);
            if (this.grounded)
            {
                this.anim.SetFloat("Speed", Mathf.Abs(move));
            }

            this.rigidbody2D.velocity = new Vector2(move * maxSpeed, this.rigidbody2D.velocity.y);
            if (this.onMovingPlatform)
            {
                this.rigidbody2D.velocity += movingPlatformVelocity;
            }

            if (move > 0 && !this.facingRight)
            {
                this.Flip();
            }
            else if (move < 0 && this.facingRight)
            {
                this.Flip();
            }

            if (this.grounded && this.jump)
            {
                this.anim.SetTrigger(Triggers.Jump);
                this.rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }

        private void Flip()
        {
            this.facingRight = !this.facingRight;
            Vector3 theScale = this.transform.localScale;
            theScale.x *= -1;
            this.transform.localScale = theScale;
        }

        #endregion
    }
}