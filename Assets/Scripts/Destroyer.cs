using UnityEngine;

namespace Assets.Scripts
{
    public class Destroyer : MonoBehaviour
    {
        #region Fields

        public float awakeDestroyDelay;

        public bool destroyOnAwake;

        public bool findChild = false;

        public string namedChild;

        #endregion

        #region Methods

        private void Awake()
        {
            if (this.destroyOnAwake)
            {
                if (this.findChild)
                {
                    Destroy(this.transform.Find(this.namedChild).gameObject);
                }
                else
                {
                    Destroy(this.gameObject, this.awakeDestroyDelay);
                }
            }
        }

        private void DestroyChildGameObject()
        {
            if (this.transform.Find(this.namedChild).gameObject != null)
            {
                Destroy(this.transform.Find(this.namedChild).gameObject);
            }
        }

        private void DestroyGameObject()
        {
            Destroy(this.gameObject);
        }

        private void DisableChildGameObject()
        {
            if (this.transform.Find(this.namedChild).gameObject.activeSelf)
            {
                this.transform.Find(this.namedChild).gameObject.SetActive(false);
            }
        }

        #endregion
    }
}