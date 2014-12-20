using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerHealth : MonoBehaviour
    {
        #region Fields

        public float damageAmount = 10f;

        public float health = 100f;

        public float hurtForce = 10f;

        public AudioClip[] ouchClips;

        public float repeatDamagePeriod = 2f;

        private Animator anim;

        private SpriteRenderer healthBar;

        private Vector3 healthScale;

        private float lastHitTime;

        private Platformer2DUserControl playerControl;

        #endregion

        #region Public Methods and Operators

        public void UpdateHealthBar()
        {
            this.healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - this.health * 0.01f);
            this.healthBar.transform.localScale = new Vector3(this.healthScale.x * this.health * 0.01f, 1, 1);
        }

        #endregion

        #region Methods

        private void Awake()
        {
            this.playerControl = this.GetComponent<Platformer2DUserControl>();
            this.healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
            this.anim = this.GetComponent<Animator>();
            this.healthScale = this.healthBar.transform.localScale;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            this.CheckCollision(col);
        }

        private void CheckCollision(Collision2D col)
        {
           if (col.gameObject.tag == Tags.Enemy || col.gameObject.tag == Tags.Fire)
            {
                if (Time.time > this.lastHitTime + this.repeatDamagePeriod)
                {
                    if (this.health > 0f)
                    {
                        this.TakeDamage(col.transform);
                        this.lastHitTime = Time.time;
                    }
                    else
                    {
                        Collider2D[] cols = this.GetComponents<Collider2D>();
                        foreach (Collider2D c in cols)
                        {
                            c.isTrigger = true;
                        }

                        SpriteRenderer[] spr = this.GetComponentsInChildren<SpriteRenderer>();
                        foreach (SpriteRenderer s in spr)
                        {
                            s.sortingLayerName = Layers.UI;
                        }

                        this.GetComponent<Platformer2DUserControl>().enabled = false;
                        this.GetComponentInChildren<Gun>().enabled = false;
                        this.anim.SetTrigger(Triggers.Die);
                    }
                }
            }
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            this.CheckCollision(col);
        }

        private void TakeDamage(Transform damageDealer)
        {
            this.playerControl.jump = false;
            Vector3 hurtVector = this.transform.position - damageDealer.position + Vector3.up * 5f;
            this.rigidbody2D.AddForce(hurtVector * this.hurtForce);
            this.health -= this.damageAmount;
            this.UpdateHealthBar();
        }

        #endregion
    }
}