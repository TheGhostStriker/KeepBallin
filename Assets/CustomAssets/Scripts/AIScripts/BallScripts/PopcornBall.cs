using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornBall : MonoBehaviour
{
    public float bounceForce = 5f;
    public float gravityScale = 0.17f;
    public float speedIncrement = 2f;
    public float maxBounceForce = 50f;
    public float maxSpeed = 20f;
    public int numSplits = 2; // The number of times the ball can split
    public float splitBounceForce = 20f; // The bounce force when the ball splits
    public float splitSpeedBoost = 10f; // The speed boost when the ball splits

    private Rigidbody rb;
    private int numBounces = 0; // The number of times the ball has bounced
    private int numSplitsLeft = 2; // The number of times the ball can still split

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * Physics.gravity.magnitude * gravityScale, ForceMode.Acceleration);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Bounce(collision.contacts[0].normal);
        }
        else if (collision.gameObject.CompareTag("Hot") && numSplitsLeft > 0)
        {
            Split();
        }
        else if (collision.gameObject.CompareTag("Ceiling"))
        {
            // Add a small force to push the ball down
            rb.AddForce(Vector3.down * 10f, ForceMode.Impulse);
        }
    }

    private void Bounce(Vector3 collisionNormal)
    {
        numBounces++;
        float angle = Random.Range(10f, 30f); // Random angle of deflection in degrees
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.Cross(Vector3.up, collisionNormal));
        Vector3 direction = rotation * collisionNormal;

        bounceForce = Mathf.Clamp(bounceForce + speedIncrement, 0f, maxBounceForce);
        float currentSpeed = rb.velocity.magnitude;
        float speedFactor = 1.2f; // Factor to increase speed
        if (currentSpeed > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        rb.AddForce((direction + Vector3.up) * bounceForce * speedFactor, ForceMode.Impulse);
    }

    private void Split()
    {
        // Spawn two duplicates at half the size
        GameObject duplicate1 = Instantiate(gameObject, transform.position, transform.rotation);
        GameObject duplicate2 = Instantiate(gameObject, transform.position, transform.rotation);
        duplicate1.transform.localScale *= 0.5f;
        duplicate2.transform.localScale *= 0.5f;

        // Decrement the numSplitsLeft counter for each duplicate
        duplicate1.GetComponent<PopcornBall>().numSplitsLeft = numSplitsLeft - 1;
        duplicate2.GetComponent<PopcornBall>().numSplitsLeft = numSplitsLeft - 1;

        // Add a sudden burst of speed and bounce force
        float currentSpeed = rb.velocity.magnitude;
        rb.AddForce(rb.velocity.normalized * splitSpeedBoost, ForceMode.Impulse);
        rb.AddForce(Vector3.up * splitBounceForce, ForceMode.Impulse);

        // Destroy the current object
        Destroy(gameObject);
    }
}



