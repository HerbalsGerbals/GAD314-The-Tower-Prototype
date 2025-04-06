using UnityEngine;

public class BalloonEnemey : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxHealth = 10f;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemySpawner1.onEnemyDestroy.Invoke();
        Destroy(gameObject);
    }
}
