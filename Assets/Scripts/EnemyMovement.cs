using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public float speed = 2f;

    void Update()
    {
        if (currentWaypoint < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[currentWaypoint].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
            {
                currentWaypoint++;
            }
        }
    }
}
