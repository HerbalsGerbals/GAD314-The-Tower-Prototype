using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f; // fire rate
    [SerializeField] private float fireCooldown = 0f;  // how long before can shoot again
    [SerializeField] private float attackRange = 5f;  // Range in which tower can detect enemies

    [Header("Editor Gizmos")]
    [SerializeField] private Color rangeGizmoColor = Color.green;

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
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.SetTarget(target.transform);
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
