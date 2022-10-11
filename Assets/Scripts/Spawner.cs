using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemyRemainingToSpawn;
    int enemyRemainingAlive;
    float nextSpawnTime;

    void Start()
    {
        NextWave();
    }

    private void Update()
    {
        if (enemyRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemyRemainingToSpawn--;
            Enemy spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity);

            nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;
            spawnedEnemy.OnDeath += OnEnemyDeath;

        }
    }

    void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            NextWave();
        }
    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            enemyRemainingToSpawn = currentWave.enemyCount;
            enemyRemainingAlive = enemyRemainingToSpawn;
        }
    }

    // To enable serialization, apply the [Serializable] attribute.just allow unity to convert the member to be able to read in inspector
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawn;
    }
}
