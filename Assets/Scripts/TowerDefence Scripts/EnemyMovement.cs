using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;  // List of waypoints
    private int currentWaypointIndex = 0;
    public float speed = 2f;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            transform.position = Vector2.MoveTowards(transform.position,
                targetWaypoint.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;  // Move to next waypoint
            }
        }
        else
        {
            Destroy(gameObject); // Destroy the enemy when it reaches the end
        }
    }
}
