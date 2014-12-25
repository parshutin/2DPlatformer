using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        #region Fields

        public bool facingRight = true;

        private bool airControl;

        private Animator anim;

        private const float checkRadius = .2f;

        private const float crouchSpeed = .36f;

        private Transform groundCheck;

        [SerializeField]
        private LayerMask groundLayerMask;

        private bool grounded;

        private bool inFire;

        private const float jumpForce = 400f;

        private const float maxSpeed = 5f;

        #endregion

        #region Public Methods and Operators

        public void Move(float move, bool crouch, bool jump)
        {
                move = (crouch ? move * crouchSpeed : move);
                if (this.grounded)
            {
                this.anim.SetFloat("Speed", Mathf.Abs(move));
            }
                
                this.rigidbody2D.velocity = new Vector2(move * maxSpeed, this.rigidbody2D.velocity.y);
                if (move > 0 && !this.facingRight)
                {
                    this.Flip();
                }
                else if (move < 0 && this.facingRight)
                {
                    this.Flip();
                }

                /*anim.SetFloat("Speed", Mathf.Abs(move));
                if (move * rigidbody2D.velocity.x < maxSpeed)
                {
                    rigidbody2D.AddForce(Vector2.right * move * 365f);
                }

                if (Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
                {
                    rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
                }

                if (move > 0 && !facingRight)
                {
                    Flip();
                }
                else if (move < 0 && facingRight)
                {
                    Flip();
                }*/
                    
            

            if (this.grounded && jump)
            {
                this.anim.SetTrigger(Triggers.Jump);
                this.rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }

        #endregion

        #region Methods

        private void Awake()
        {
            this.groundCheck = this.transform.Find("GroundCheck");
            this.anim = this.GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            this.grounded = Physics2D.OverlapCircle(this.groundCheck.position, checkRadius, this.groundLayerMask);
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