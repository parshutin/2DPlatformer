using UnityEngine;

namespace Assets.Scripts
{
    public class SetParticleSortingLayer : MonoBehaviour
    {
        #region Fields

        public string sortingLayerName;

        #endregion

        #region Methods

        private void Start()
        {
            this.particleSystem.renderer.sortingLayerName = this.sortingLayerName;
        }

        #endregion
    }
}