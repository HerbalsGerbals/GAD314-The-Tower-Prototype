using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Tilemaps;

public class SpikeTower : Tower
{
    [Header("Tower Settings")]
    [SerializeField] private GameObject spikeProjectile; 
    [SerializeField] private Transform firePoint; 
    [SerializeField] private Tilemap path; 

    [SerializeField] private float fireRate = 1f; 
    [SerializeField] private float fireCooldown = 0f; 
    [SerializeField] private float attackRange = 5f; 
    [SerializeField] private int spikeCount = 1; 

    [Header("Gizmos")]
    [SerializeField] private Color rangeGizmoColor = Color.green;

    private void Awake()
    {
        cost = 10;
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0)
        {
            SpawnSpikes();
            fireCooldown = fireRate; 
        }
    }

    private void SpawnSpikes()
    {
        Vector2 towerPosition = transform.position; 
        Vector3Int towerTilePosition = path.WorldToCell(towerPosition); 

        int rangeTiles = Mathf.CeilToInt(attackRange);

        for (int i = 0; i < spikeCount; i++)
        {
            int randomXOffset = Random.Range(-rangeTiles, rangeTiles);
            int randomYOffset = Random.Range(-rangeTiles, rangeTiles);

            Vector3Int tilePosition = new Vector3Int(towerTilePosition.x + randomXOffset, towerTilePosition.y + randomYOffset, towerTilePosition.z);
            TileBase tile = path.GetTile(tilePosition); 

            if (tile != null)
            {
                Vector3 worldPosition = path.CellToWorld(tilePosition); 
                ShootSpikes(worldPosition);
            }
        }
    }

    private void ShootSpikes(Vector3 targetPosition)
    {
        GameObject spike = Instantiate(spikeProjectile, transform.position, Quaternion.identity);
        SpikeProjectile spikeScript = spike.GetComponent<SpikeProjectile>();

        if (spikeScript != null)
        {
            spikeScript.Launch(targetPosition); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeGizmoColor; 
        Gizmos.DrawWireSphere(transform.position, attackRange); 
    }
}
