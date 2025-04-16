using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;

    [Header("Attributes")]
    [SerializeField] public float maxHealth = 10f;

    public bool canChangeStats = true;

    private Transform target;
    private int pathIndex = 0;

    public EnemySpawner1 enemySpawner;

    private void Start()
    {
        target = LevelManager1.main.path[pathIndex];

        canChangeStats = false;
    }


    private void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager1.main.path.Length)
            {
                EnemySpawner1.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager1.main.path[pathIndex];
            }
        }

        if (!canChangeStats)
        {
            maxHealth = maxHealth * Mathf.Pow(enemySpawner.currentWave, enemySpawner.difficultyScalingFactor);

            moveSpeed = moveSpeed * Mathf.Pow(enemySpawner.currentWave, enemySpawner.difficultyScalingFactor);

            canChangeStats = true;
        }
    }


    public void TakeDamage(float amount)
    {
        maxHealth -= amount;

        if (maxHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemySpawner1.onEnemyDestroy.Invoke();
        Destroy(gameObject);
    }

    public void IncreaseStats()
    {
        maxHealth = maxHealth * 1.5f;
        moveSpeed = moveSpeed * 1.5f;
    }
}