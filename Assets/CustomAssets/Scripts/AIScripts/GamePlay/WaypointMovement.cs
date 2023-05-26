using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints to follow
    public float speed = 5f;       // Movement speed of the object
    public float waypointRadius = 0.5f;  // Radius around waypoints to consider as reached

    private int currentWaypointIndex = 0;

    private void Update()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned!");
            return;
        }

        // Calculate direction towards the current waypoint
        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
        direction.Normalize();

        // Move the object towards the waypoint
        transform.position += direction * speed * Time.deltaTime;

        // Check if the object has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= waypointRadius)
        {
            // Move to the next waypoint
            currentWaypointIndex++;

            // Check if all waypoints have been visited
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;  // Reset to the first waypoint
            }
        }
    }
}





