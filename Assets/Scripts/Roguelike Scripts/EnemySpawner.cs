using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int maxEnemyCount = 1;

    public Vector2 center;
    public Vector2 size;
    [SerializeField] int enemyCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount < maxEnemyCount)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Vector2 pos = center + new Vector2(Random.Range(-size.x * 0.5f, size.x * 0.5f), Random.Range(-size.y * 0.5f, size.y * 0.5f));

        Instantiate(enemyPrefab, pos, Quaternion.identity);

    }

}
