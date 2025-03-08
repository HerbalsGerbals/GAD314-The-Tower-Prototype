using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;
    public float spawnInterval = 2f; // How fast enemy waves spawan
    public int enemiesPerWave = 5; // How many enemys spawn per per wave

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyMovement>().waypoints = waypoints;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

