using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public Tilemap towerTilemap; // The tilemap where towers CAN be placed
    public TileBase validPlacementTile; // The tile that allows placement
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click to place tower
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = towerTilemap.WorldToCell(mouseWorldPos); // Convert to tilemap grid position

            if (CanPlaceTower(cellPosition))
            {
                Vector3 placePosition = towerTilemap.GetCellCenterWorld(cellPosition); // Snap to tile center
                Instantiate(towerPrefab, placePosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("❌ Can't place tower here!");
            }
        }
    }

    bool CanPlaceTower(Vector3Int cellPosition)
    {
        // Get the tile at the cell position
        TileBase tileAtCell = towerTilemap.GetTile(cellPosition);

        // ✅ Allow placement ONLY if the tile is the valid placement tile
        return tileAtCell == validPlacementTile;
    }
}
