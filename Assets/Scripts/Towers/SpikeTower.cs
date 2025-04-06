using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Tilemaps;

public class SpikeTower : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] private GameObject spikeProjectile; // The spike projectile prefab
    [SerializeField] private Transform firePoint; // Firepoint is not used here, as we spawn the spikes on the path
    [SerializeField] private Tilemap path; // Reference to the Tilemap of the path

    [SerializeField] private float fireRate = 1f; // How often the tower fires
    [SerializeField] private float fireCooldown = 0f; // Timer for the next shot
    [SerializeField] private float attackRange = 5f; // The range within which the tower can spawn spikes on the path
    [SerializeField] private int spikeCount = 1; // How many spikes to spawn each time

    [Header("Gizmos")]
    [SerializeField] private Color rangeGizmoColor = Color.green; // For visualizing the attack range

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0)
        {
            SpawnSpikes();
            fireCooldown = fireRate; // Reset cooldown after firing
        }
    }

    private void SpawnSpikes()
    {
        Vector2 towerPosition = transform.position; // Get the position of the tower
        Vector3Int towerTilePosition = path.WorldToCell(towerPosition); // Convert tower position to tilemap cell position

        // Get the bounds of the attack range in terms of tile positions
        int rangeTiles = Mathf.CeilToInt(attackRange);

        for (int i = 0; i < spikeCount; i++)
        {
            // Randomly choose a position within the attack range
            int randomXOffset = Random.Range(-rangeTiles, rangeTiles);
            int randomYOffset = Random.Range(-rangeTiles, rangeTiles);

            Vector3Int tilePosition = new Vector3Int(towerTilePosition.x + randomXOffset, towerTilePosition.y + randomYOffset, towerTilePosition.z);
            TileBase tile = path.GetTile(tilePosition); // Check if there's a tile at that position

            if (tile != null)
            {
                Vector3 worldPosition = path.CellToWorld(tilePosition); // Convert tile position to world position
                ShootSpikes(worldPosition); // Shoot spike towards the found tile
            }
        }
    }

    private void ShootSpikes(Vector3 targetPosition)
    {
        GameObject spike = Instantiate(spikeProjectile, transform.position, Quaternion.identity);
        SpikeProjectile spikeScript = spike.GetComponent<SpikeProjectile>();

        if (spikeScript != null)
        {
            spikeScript.Launch(targetPosition); // Pass the target position directly
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = rangeGizmoColor; // Set color for the attack range visualization
        Gizmos.DrawWireSphere(transform.position, attackRange); // Draw a wire sphere to show range
    }
}
