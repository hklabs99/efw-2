using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;

    [Header ("PowerUps")]
    [SerializeField] GameObject[] powerUps;

    [Header("Coins")]
    [SerializeField] GameObject _bronzeCoinPrefab;
    [SerializeField] GameObject _coinContainer;

    [Header ("Snowflakes")]
    [Tooltip ("Only for the 'Snowflakes' scene")]
    [SerializeField] GameObject _snowFlakesPrefab;

    bool _stopSpawning = false;

    void Awake ()
    {
        StartCoroutine (SpawnSnowflakesRoutine ());
    }

    public void StartSpawning ()
    {
        StartCoroutine (SpawnEnemyRoutine ());
        StartCoroutine (SpawnPowerUpRoutine ());
        StartCoroutine (SpawnCoinRoutine ());
    }

    IEnumerator SpawnCoinRoutine ()
    {
        yield return new WaitForSeconds (3f);
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3 (Random.Range (-8f, 8f), 7, 0);
            GameObject newCoin = Instantiate (_bronzeCoinPrefab, postToSpawn, Quaternion.identity);
            newCoin.transform.parent = _coinContainer.transform;

            yield return new WaitForSeconds (Random.Range (10, 21));
        }
    }

    IEnumerator SpawnEnemyRoutine ()
    {
        yield return new WaitForSeconds (3f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3 (Random.Range (-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate (_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds (5.0f);
        }
    }

    IEnumerator SpawnSnowflakesRoutine ()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3 (Random.Range (-8f, 8f), 7, 0);
            Instantiate (_snowFlakesPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds (Random.Range (1, 2));
        }
    }

    IEnumerator SpawnPowerUpRoutine ()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3 (Random.Range (-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range (0, 3);
            Instantiate (powerUps [randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds (Random.Range (3, 8));
        }
    }

    public void OnPlayerDeath ()
    {
        _stopSpawning = true;
    }
}
