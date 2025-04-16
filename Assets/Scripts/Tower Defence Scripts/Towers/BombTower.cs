using UnityEngine;

public class BombTower : Tower
{
    [Header("Tower Settings")]
    [SerializeField] private GameObject bombProjectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCooldown = 0f;
    [SerializeField] private float attackRange = 5f;

    [Header("Gizmos")]
    [SerializeField] private Color rangeGizmoColor = Color.blue;

    private void Awake()
    {
        cost = 15;
    }
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            GameObject target = FindNearestEnemy();
            if (target != null)
            {
                Fire(target);
                fireCooldown = fireRate;
            }
        }
    }

    void Fire(GameObject target)
    {
        GameObject projectile = Instantiate(bombProjectilePrefab, firePoint.position, Quaternion.identity);
        BombProjectile projScript = projectile.GetComponent<BombProjectile>();

        if (projScript != null)
        {
            projScript.SetTarget(target.transform);
        }
        else
        {
            Debug.LogError("BombProjectile script missing from bomb projectile prefab!");
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Slimes");
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeGizmoColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
