using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGravity : MonoBehaviour
{
    public float bounceForce = 5f;
    public float gravityScale = 0.17f;
    public float speedIncrement = 2f;
    public float maxBounceForce = 50f;
    public float maxSpeed = 20f;
    public float spawnTime = 5f;
    public int maxBalls = 5; // The maximum number of balls that can be spawned from the duplicate

    private Rigidbody rb;
    private int numBalls = 0; // The number of balls that have been spawned from the duplicate

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Invoke("SpawnDuplicate", spawnTime);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * Physics.gravity.magnitude * gravityScale, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        if (transform.position.y > 30f)
        {
            rb.AddForce(Vector3.down * Physics.gravity.magnitude * gravityScale * 50f, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
            float angle = Random.Range(10f, 30f); // Random angle of deflection in degrees
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.Cross(Vector3.up, randomDirection));
            Vector3 direction = rotation * randomDirection;

            bounceForce = Mathf.Clamp(bounceForce + speedIncrement, 0f, maxBounceForce);
            float currentSpeed = rb.velocity.magnitude;
            float speedFactor = 1.2f; // Factor to increase speed
            if (currentSpeed > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            rb.AddForce((direction + Vector3.up) * bounceForce * speedFactor, ForceMode.Impulse);
        }
    }

    private void SpawnDuplicate()
    {
        if (numBalls < maxBalls && transform.parent == null) // Only spawn a ball if the maximum number has not been reached
        {
            GameObject duplicate = Instantiate(gameObject);
            duplicate.transform.position = transform.position; // Set position of duplicate to current object's position
            duplicate.transform.localScale = transform.localScale; // Set scale of duplicate to current object's scale
            duplicate.GetComponent<MoonGravity>().numBalls = numBalls + 1;
            numBalls++;// Increment the numBalls counter of the duplicate
            
        }
    }
}
