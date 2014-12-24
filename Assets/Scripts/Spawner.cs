using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        #region Constants

        private const int MaxEnemiesCount = 2;

        private const float SpawnDelay = 3f;

        private const float SpawnTime = 5f;

        private int enemiesCount;

        #endregion

        #region Fields

        [SerializeField]
        private GameObject enemy;

        #endregion

        #region Methods

        private void Spawn()
        {
            if (this.enemiesCount < MaxEnemiesCount)
            {
                Instantiate(this.enemy, this.transform.position, this.transform.rotation);
                this.enemiesCount++;
            }
        }

        public void KillEnemy()
        {
            this.enemiesCount--;
        }

        private void Start()
        {
            this.InvokeRepeating("Spawn", SpawnDelay, SpawnTime);
        }

        #endregion
    }
}