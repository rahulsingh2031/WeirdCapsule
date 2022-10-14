using UnityEngine;
using System.Collections;
public class Spawner : MonoBehaviour
{
    public event System.Action<int> OnNextWave;
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;

    LivingEntity playerEntity;
    Transform playerTransform;

    int enemyRemainingToSpawn;
    int enemyRemainingAlive;
    float nextSpawnTime;
    MapGenerator map;


    float timeBetweenCampingChecks = 2f;
    float nextCampCheckTime;
    float campThresholdDistance = 1.5f;

    bool isCamping;
    bool isDisabled;
    Vector3 campPositionOld;
    void Start()
    {
        playerEntity = FindObjectOfType<Player>();
        playerTransform = playerEntity.transform;
        playerEntity.OnDeath += OnPlayerDeath;

        nextCampCheckTime = timeBetweenCampingChecks + Time.time;
        campPositionOld = playerTransform.position;
        map = FindObjectOfType<MapGenerator>();
        NextWave();
    }


    private void Update()
    {
        if (!isDisabled)
        {
            if (Time.time > nextCampCheckTime)
            {
                nextCampCheckTime = timeBetweenCampingChecks + Time.time;
                isCamping = (Vector3.Distance(playerTransform.position, campPositionOld) < campThresholdDistance);
                campPositionOld = playerTransform.position;
            }
            if (enemyRemainingToSpawn > 0 && Time.time > nextSpawnTime)
            {
                enemyRemainingToSpawn--;
                nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;

                StartCoroutine(spawnEnemy());

            }
        }
    }
    void OnPlayerDeath()
    {
        isDisabled = true;
    }
    void OnEnemyDeath()
    {
        enemyRemainingAlive--;

        if (enemyRemainingAlive == 0)
        {
            NextWave();
        }
    }

    IEnumerator spawnEnemy()
    {
        float spawnDelay = 1f;
        float tileFlashSpeed = 4f;

        Transform spawnTile = map.GetRandomOpenTile();
        if (isCamping)
        {
            spawnTile = map.GetTileFromPosition(playerTransform.position);
        }
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColor = tileMat.color;
        Color flashColor = Color.red;
        float spawnTimer = 0;
        while (spawnTimer < spawnDelay)
        {

            tileMat.color = Color.Lerp(initialColor, flashColor, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

            spawnTimer += Time.deltaTime;
            yield return null;

        }


        Enemy spawnedEnemy = Instantiate(enemy, spawnTile.position, Quaternion.identity);
        spawnedEnemy.OnDeath += OnEnemyDeath;

    }

    void NextWave()
    {
        currentWaveNumber++;
        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];
            enemyRemainingToSpawn = currentWave.enemyCount;
            enemyRemainingAlive = enemyRemainingToSpawn;

            OnNextWave?.Invoke(currentWaveNumber);
            ResetPlayerPosition();
        }
    }

    void ResetPlayerPosition()
    {
        playerTransform.position = map.GetTileFromPosition(Vector3.zero).position + Vector3.up * 3;
    }

    // To enable serialization, apply the [Serializable] attribute.just allow unity to convert the member to be able to read in inspector
    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawn;
    }
}
