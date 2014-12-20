using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        #region Fields

        public int HP = 2;

        public Sprite damagedEnemy;

        public Sprite deadEnemy;

        public AudioClip[] deathClips;

        public float deathSpinMax = 100f;

        public float deathSpinMin = -100f;

        public GameObject hundredPointsUI;

        public float moveSpeed = 2f;

        private bool dead;

        private Transform frontCheck;

        private SpriteRenderer ren;

        [SerializeField]
        private GameObject spawner;

        #endregion

        #region Public Methods and Operators

        public void Flip()
        {
            Vector3 enemyScale = this.transform.localScale;
            enemyScale.x *= -1;
            this.transform.localScale = enemyScale;
        }

        public void Hurt()
        {
            this.HP--;
        }

        #endregion

        #region Methods

        private void Awake()
        {
            this.ren = this.transform.Find("body").GetComponent<SpriteRenderer>();
            this.frontCheck = this.transform.Find("frontCheck").transform;
        }

        private void Death()
        {
            SpriteRenderer[] otherRenderers = this.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer s in otherRenderers)
            {
                s.enabled = false;
            }

            this.ren.enabled = true;
            this.ren.sprite = this.deadEnemy;
            this.dead = true;
            this.spawner.GetComponent<Spawner>().EnemiesCount--;
            this.rigidbody2D.fixedAngle = false;
            this.rigidbody2D.AddTorque(Random.Range(this.deathSpinMin, this.deathSpinMax));
            Collider2D[] cols = this.GetComponents<Collider2D>();
            foreach (Collider2D c in cols)
            {
                c.isTrigger = true;
            }
        }

        private void FixedUpdate()
        {
            Collider2D[] frontHits = Physics2D.OverlapPointAll(this.frontCheck.position);
            foreach (Collider2D c in frontHits)
            {
                if (c.tag == Tags.Wall)
                {
                    this.Flip();
                    break;
                }
            }
            this.rigidbody2D.velocity = new Vector2(this.transform.localScale.x * this.moveSpeed, this.rigidbody2D.velocity.y);

            if (this.HP == 1 && this.damagedEnemy != null)
            {
                this.ren.sprite = this.damagedEnemy;
            }
            if (this.HP <= 0 && !this.dead)
            {
                this.Death();
            }
        }

        #endregion
    }
}