using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public int maxRooms = 10;
    public Vector2Int roomSize = new Vector2Int(10, 10); // Set room size

    private List<RoomData> placedRooms = new List<RoomData>();

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        Vector2Int startPosition = Vector2Int.zero;
        GameObject startRoom = InstantiateRandomRoom(startPosition);
        Vector2Int startSize = GetRoomSize(startRoom);
        placedRooms.Add(new RoomData(startPosition, startSize, startRoom));

        for (int i = 1; i < maxRooms; i++)
        {
            (Vector2Int newPosition, Vector2Int newSize) = GetValidPosition();
            if (newPosition != Vector2Int.zero)
            {
                GameObject newRoom = InstantiateRandomRoom(newPosition);
                placedRooms.Add(new RoomData(newPosition, newSize, newRoom));

                ConnectDoors(newPosition, newSize);
            }
        }
    }

    (Vector2Int, Vector2Int) GetValidPosition()
    {
        List<(Vector2Int, Vector2Int)> possiblePositions = new List<(Vector2Int, Vector2Int)>();

        foreach (RoomData room in placedRooms)
        {
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (Vector2Int dir in directions)
            {
                GameObject potentialRoom = roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)];
                Vector2Int newSize = GetRoomSize(potentialRoom);
                Vector2Int newPos = room.position + (dir * newSize);

                if (!IsOverlapping(newPos, newSize))
                {
                    possiblePositions.Add((newPos, newSize));
                }
            }
        }

        if (possiblePositions.Count == 0) return (Vector2Int.zero, Vector2Int.zero);
        return possiblePositions[UnityEngine.Random.Range(0, possiblePositions.Count)];
    }

    bool IsOverlapping(Vector2Int newPos, Vector2Int newSize)
    {
        foreach (RoomData room in placedRooms)
        {
            if (AreRoomsOverlapping(room.position, room.size, newPos, newSize))
                return true;
        }
        return false;
    }

    bool AreRoomsOverlapping(Vector2Int pos1, Vector2Int size1, Vector2Int pos2, Vector2Int size2)
    {
        Rect roomRect1 = new Rect(pos1.x, pos1.y, size1.x, size1.y);
        Rect roomRect2 = new Rect(pos2.x, pos2.y, size2.x, size2.y);

        return roomRect1.Overlaps(roomRect2);  // Check if the rooms overlap based on their Rect bounds
    }

    GameObject InstantiateRandomRoom(Vector2Int position)
    {
        GameObject roomPrefab = roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)];
        GameObject newRoom = Instantiate(roomPrefab, new Vector3(position.x, position.y, 0), Quaternion.identity);

        // Ensure the room has a BoxCollider2D
        BoxCollider2D collider = newRoom.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = newRoom.AddComponent<BoxCollider2D>();
        }

        // Update the door positions after instantiating the room
        UpdateDoorPositions(newRoom);

        return newRoom;
    }

    void UpdateDoorPositions(GameObject room)
    {
        // Get the room's size and adjust the door positions accordingly
        BoxCollider2D collider = room.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            float roomWidth = collider.size.x;
            float roomHeight = collider.size.y;

            // Update the door positions
            string[] doorNames = { "DoorTop", "DoorBottom", "DoorLeft", "DoorRight" };
            foreach (string doorName in doorNames)
            {
                Transform doorTransform = room.transform.Find(doorName);
                if (doorTransform != null)
                {
                    // Calculate door offsets and apply them
                    if (doorName.Contains("Top") || doorName.Contains("Bottom"))
                    {
                        doorTransform.localPosition = new Vector3(roomWidth / 2f, doorTransform.localPosition.y, 0);
                    }
                    else if (doorName.Contains("Left") || doorName.Contains("Right"))
                    {
                        doorTransform.localPosition = new Vector3(doorTransform.localPosition.x, roomHeight / 2f, 0);
                    }
                }
            }
        }
    }

    void ConnectDoors(Vector2Int position, Vector2Int roomSize)
    {
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        string[] doorNames = { "DoorTop", "DoorBottom", "DoorLeft", "DoorRight" };

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2Int adjacentPos = position + (directions[i] * roomSize);
            RoomData adjacentRoom = placedRooms.Find(room => room.position == adjacentPos);

            if (adjacentRoom != null)
            {
                GameObject currentRoom = GetRoomAtPosition(position);
                GameObject adjacentRoomObject = adjacentRoom.room;

                // Calculate the door positions based on room size and door locations
                Transform currentDoor = GetDoorTransform(currentRoom, doorNames[i]);
                Transform adjacentDoor = GetDoorTransform(adjacentRoomObject, doorNames[(i + 2) % 4]); // Opposite door

                // Make sure the doors align properly before disabling them
                if (currentDoor != null && adjacentDoor != null)
                {
                    // Align the doors correctly based on room size
                    AlignDoors(currentDoor, adjacentDoor);
                    currentDoor.gameObject.SetActive(false);  // Disable the door once it's aligned
                    adjacentDoor.gameObject.SetActive(false); // Disable the opposite door

                    // Remove tiles at door locations
                    RemoveTileAtDoor(currentRoom, currentDoor);
                    RemoveTileAtDoor(adjacentRoomObject, adjacentDoor);
                }
            }
        }
    }

    // Remove the tile at the door location for the specific room's Tilemap
    void RemoveTileAtDoor(GameObject room, Transform door)
    {
        // Get the Tilemap component for the room
        Tilemap roomTilemap = room.GetComponentInChildren<Tilemap>();

        if (roomTilemap != null)
        {
            Vector3 doorWorldPosition = door.position;
            Vector3Int doorGridPosition = roomTilemap.WorldToCell(doorWorldPosition);

            // Set the tile at the door position to null (remove it)
            roomTilemap.SetTile(doorGridPosition, null);
        }
        else
        {
            Debug.LogError("Tilemap not found in room!");
        }
    }

    // Align doors based on position and room size
    void AlignDoors(Transform currentDoor, Transform adjacentDoor)
    {
        // Example: Adjust the door's local position based on room size
        float roomWidth = currentDoor.parent.GetComponent<BoxCollider2D>().size.x;
        float roomHeight = currentDoor.parent.GetComponent<BoxCollider2D>().size.y;

        if (currentDoor.name.Contains("Top") || currentDoor.name.Contains("Bottom"))
        {
            // Adjust vertical doors (top and bottom)
            currentDoor.localPosition = new Vector3(roomWidth / 2f, currentDoor.localPosition.y, currentDoor.localPosition.z);
            adjacentDoor.localPosition = new Vector3(roomWidth / 2f, adjacentDoor.localPosition.y, adjacentDoor.localPosition.z);
        }
        else if (currentDoor.name.Contains("Left") || currentDoor.name.Contains("Right"))
        {
            // Adjust horizontal doors (left and right)
            currentDoor.localPosition = new Vector3(currentDoor.localPosition.x, roomHeight / 2f, currentDoor.localPosition.z);
            adjacentDoor.localPosition = new Vector3(adjacentDoor.localPosition.x, roomHeight / 2f, adjacentDoor.localPosition.z);
        }
    }

    Vector2Int GetRoomSize(GameObject room)
    {
        BoxCollider2D collider = room.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            return new Vector2Int(Mathf.RoundToInt(collider.size.x), Mathf.RoundToInt(collider.size.y));
        }
        return new Vector2Int(10, 10); // Default size if no BoxCollider2D is found
    }

    Transform GetDoorTransform(GameObject room, string doorName)
    {
        return room.transform.Find(doorName);
    }

    GameObject GetRoomAtPosition(Vector2Int position)
    {
        RoomData room = placedRooms.Find(r => r.position == position);
        return room != null ? room.room : null;
    }

    private class RoomData
    {
        public Vector2Int position;
        public Vector2Int size;
        public GameObject room;

        public RoomData(Vector2Int pos, Vector2Int sz, GameObject rm)
        {
            position = pos;
            size = sz;
            room = rm;
        }
    }

    // Draw Gizmos in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Color for the gizmo

        foreach (RoomData room in placedRooms)
        {
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            string[] doorNames = { "DoorTop", "DoorBottom", "DoorLeft", "DoorRight" };

            foreach (var direction in directions)
            {
                // Get the door transform for each direction
                GameObject currentRoom = GetRoomAtPosition(room.position);
                if (currentRoom != null)
                {
                    Transform doorTransform = GetDoorTransform(currentRoom, doorNames[Array.IndexOf(directions, direction)]);
                    if (doorTransform != null)
                    {
                        // Draw a sphere at the door location
                        Gizmos.DrawSphere(doorTransform.position, 0.2f); // 0.2f is the radius of the gizmo sphere
                    }
                }
            }
        }
    }
}
