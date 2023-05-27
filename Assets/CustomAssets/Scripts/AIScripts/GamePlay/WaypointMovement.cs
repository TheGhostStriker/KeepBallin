using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints to follow
    public float speed = 5f;       // Movement speed of the object
    public float waypointRadius = 0.5f;  // Radius around waypoints to consider as reached

    private int currentWaypointIndex = 0;
    private bool isReversed = false;

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
            // Check if it reached the final waypoint
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                isReversed = true;  // Set reverse flag to true
            }
            // Check if it reached the first waypoint in reverse mode
            else if (currentWaypointIndex == 0 && isReversed)
            {
                isReversed = false;  // Reset reverse flag to false
            }

            // Move to the next or previous waypoint based on reverse flag
            if (isReversed)
            {
                currentWaypointIndex--;
            }
            else
            {
                currentWaypointIndex++;
            }
        }
    }
}






