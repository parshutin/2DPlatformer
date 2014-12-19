using UnityEngine;

namespace Assets.Scripts
{
    public class Spawner : MonoBehaviour
    {
        #region Constants

        private const int MaxEnemiesCount = 2;

        private const float SpawnDelay = 3f;

        private const float SpawnTime = 5f;

        #endregion

        #region Fields

        [SerializeField]
        private GameObject enemy;

        #endregion

        #region Public Properties

        public int EnemiesCount { get; set; }

        #endregion

        #region Methods

        private void Spawn()
        {
            if (this.EnemiesCount < MaxEnemiesCount)
            {
                Instantiate(this.enemy, this.transform.position, this.transform.rotation);
                this.EnemiesCount++;
            }
        }

        private void Start()
        {
            this.InvokeRepeating("Spawn", SpawnDelay, SpawnTime);
        }

        #endregion
    }
}