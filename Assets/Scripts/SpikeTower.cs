using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Tilemaps;

public class SpikeTower : MonoBehaviour
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float fireCooldown = 10f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;


    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown >= 0)
        {
            SpawnSpikes();
        }
    }

    private void SpawnSpikes()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();


    }
}
