using UnityEngine;

public class ArrowTower : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float fireCooldown = 0f;
    public float attackRange = 5f;  // Range in which tower can detect enemies

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
            projScript.SetTarget(target);  // Pass target to the projectile
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
}
