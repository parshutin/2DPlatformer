using UnityEngine;

namespace Assets.Scripts
{
    public class Rocket : MonoBehaviour
    {
        #region Fields

        public GameObject explosion;
       
        #endregion

        #region Methods

        private void OnExplode()
        {
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            Instantiate(this.explosion, this.transform.position, randomRotation);
        }
        

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == Tags.Enemy)
            {
                col.gameObject.GetComponent<Enemy>().Hurt();
            }

            this.OnExplode();
            Destroy(this.gameObject);
        }

        #endregion
    }
}