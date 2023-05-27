using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    public Transform[] waypoints;  // Array of waypoints to follow
    public float baseSpeed = 5f;       // Base movement speed of the object
    public float waypointRadius = 0.5f;  // Radius around waypoints to consider as reached

    private int currentWaypointIndex = 0;
    private bool isReversed = false;
    [SerializeField] private int cycleCount = 0;
    [SerializeField] private float currentSpeed;

    private void Start()
    {
        currentSpeed = baseSpeed;
    }

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

        // Rotate the object to face the direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            if (isReversed)
            {
                targetRotation *= Quaternion.Euler(0f, 180f, 0f); // Rotate by 180 degrees
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // Move the object towards the waypoint with the current speed
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Check if the object has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= waypointRadius)
        {
            // Check if it reached the final waypoint
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                isReversed = true; // Set reverse flag to true
                cycleCount++; // Increase the cycle count

                // Increase the speed by 5f for each completed cycle
                currentSpeed = baseSpeed + (cycleCount * 5f);
            }
            // Check if it reached the first waypoint in reverse mode
            else if (currentWaypointIndex == 0 && isReversed)
            {
                isReversed = false; // Reset reverse flag to false
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












