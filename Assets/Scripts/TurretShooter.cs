using System.Collections;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Attributes")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float range = 5f;

    [Header("Editor Debug")]
    [SerializeField] private Color rangeGizmoColor = Color.cyan; 

    private float fireCooldown;

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        GameObject target = FindClosestEnemy();
        if (target != null && fireCooldown <= 0f)
        {
            Shoot(target.transform);
            fireCooldown = 1f / fireRate;
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= range)
            {
                shortestDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    private void Shoot(Transform target)
    {
        GameObject projGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile proj = projGO.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.SetTarget(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) Gizmos.color = rangeGizmoColor;
        else Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
