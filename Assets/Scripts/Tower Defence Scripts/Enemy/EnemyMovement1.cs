using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement1 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    public float moveSpeed = 2f;

    [Header("Attributes")]
    public float maxHealth = 10f;

    public float damage = 5f;

    public TowerHealth towerHealth;


    private Transform target;
    private int pathIndex = 0;

    public EnemySpawner1 enemySpawner;

    private void Start()
    {
        target = LevelManager1.main.path[pathIndex];
        towerHealth = FindAnyObjectByType<TowerHealth>();

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
                towerHealth.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager1.main.path[pathIndex];
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            TakeDamage(5);
        }
    }
}