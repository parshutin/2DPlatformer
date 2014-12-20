using System.Collections;

using UnityEngine;

namespace Assets.Scripts
{
    public class Bomb : MonoBehaviour
    {
        #region Constants

        private const float BombForce = 100f;

        private const float BombRadius = 10f;

        private const float FuseTime = 1.5f;

        #endregion

        #region Fields

        public GameObject explosion;

        private ParticleSystem explosionFX;

        #endregion

        #region Public Methods and Operators

        public void Explode()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, BombRadius, 1 << LayerMask.NameToLayer(Layers.Enemy));
            foreach (Collider2D en in enemies)
            {
                var rb = en.rigidbody2D;
                if (rb != null && rb.tag == Tags.Enemy)
                {
                    rb.gameObject.GetComponent<Enemy>().HP = 0;
                    Vector3 deltaPos = rb.transform.position - this.transform.position;
                    Vector3 force = deltaPos.normalized * BombForce;
                    rb.AddForce(force);
                }
            }

            this.explosionFX.transform.position = this.transform.position;
            this.explosionFX.Play();
            Instantiate(this.explosion, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        #endregion

        #region Methods

        private void Awake()
        {
            this.explosionFX = GameObject.FindGameObjectWithTag(Tags.Explosion).GetComponent<ParticleSystem>();
        }

        private IEnumerator BombDetonation()
        {
            yield return new WaitForSeconds(FuseTime);
            this.Explode();
        }

        private void Start()
        {
            if (this.transform.root == this.transform)
            {
                this.StartCoroutine(this.BombDetonation());
            }
        }

        #endregion
    }
}