using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs; 
    public Tilemap towerTilemap; // The tilemap where towers CAN be placed
    public TileBase validPlacementTile; // The tile that allows placement
    private Camera cam;

    private bool isPlacingTower = false; 
    private GameObject ghostTower; // The ghost tower object
    private GameObject currentTowerPrefab; // The currently selected tower prefab

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (isPlacingTower && ghostTower != null)
        {
            HandleGhostTowerPlacement();
        }

        if (Input.GetMouseButton(0) && isPlacingTower) // Left click to place tower
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = towerTilemap.WorldToCell(mouseWorldPos); // Convert to tilemap grid position

            if (CanPlaceTower(cellPosition))
            {
                PlaceTower(cellPosition);
                isPlacingTower = false; // Disable placement after placing the tower
                DestroyGhostTower(); // Destroy the ghost tower after placement
            }
            else
            {
                Debug.Log("❌ Can't place tower here!");
            }
        }
    }

    void HandleGhostTowerPlacement()
    {
        if (ghostTower != null)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = towerTilemap.WorldToCell(mouseWorldPos); // Convert to tilemap grid position

            // Move the ghost tower to the current mouse position (snapping to the grid)
            Vector3 placePosition = towerTilemap.GetCellCenterWorld(cellPosition);
            ghostTower.transform.position = placePosition;
        }
    }

    void PlaceTower(Vector3Int cellPosition)
    {
        // Place the actual tower on the tilemap
        Vector3 placePosition = towerTilemap.GetCellCenterWorld(cellPosition);
        Instantiate(currentTowerPrefab, placePosition, Quaternion.identity);
        FindFirstObjectByType<TowerUISelection>().isPlacingTower = false;
    }

    public void SetCurrentTower(int towerIndex, bool isPlacing)
    {
        if (towerIndex >= 0 && towerIndex < towerPrefabs.Length)
        {
            currentTowerPrefab = towerPrefabs[towerIndex];
        }

        isPlacingTower = isPlacing;

        if (isPlacingTower && currentTowerPrefab != null)
        {
            // Create the ghost tower when placing starts
            CreateGhostTower();
        }
        else
        {
            // Destroy the ghost tower when exiting placing mode
            DestroyGhostTower();
        }
    }

    void CreateGhostTower()
    {
        if (currentTowerPrefab != null && ghostTower == null) // Prevent multiple ghosts from being created
        {
            ghostTower = Instantiate(currentTowerPrefab);
            ghostTower.SetActive(true); // Make sure the ghost tower is active

            Renderer renderer = ghostTower.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = 0.5f; // Set transparency to 50%
                renderer.material.color = color;
            }

            // Disable any gameplay scripts like shooting or interaction for the ghost tower
            DisableGhostTowerGameplay();
        }
    }

    void DisableGhostTowerGameplay()
    {
        
        var towerScripts = ghostTower.GetComponents<MonoBehaviour>();
        foreach (var script in towerScripts)
        {
            script.enabled = false;
        }
    }

    void DestroyGhostTower()
    {
        if (ghostTower != null)
        {
            Destroy(ghostTower);
            ghostTower = null;
        }
    }

    bool CanPlaceTower(Vector3Int cellPosition)
    {
        // Get the tile at the cell position
        TileBase tileAtCell = towerTilemap.GetTile(cellPosition);

        // ✅ Allow placement ONLY if the tile is the valid placement tile
        return tileAtCell == validPlacementTile && !IsTowerAlreadyPlaced(cellPosition);
    }

    bool IsTowerAlreadyPlaced(Vector3Int cellPosition)
    {
        // Check if a tower already exists at the given position
        Vector3 worldPos = towerTilemap.GetCellCenterWorld(cellPosition);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPos, 0.5f);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Tower"))
            {
                return true;
            }
        }

        return false;
    }
}
