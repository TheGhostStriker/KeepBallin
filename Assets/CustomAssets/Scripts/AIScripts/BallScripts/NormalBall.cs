using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviour
{
    public float speed = 60f;
    public float maxSpeed = 180f;
    public float minSpeed = 5f;
    public float speedIncrement = 2f;
    public float gravity = 9.81f;
    public float drag = 0.5f;

    private Rigidbody rb;
    private bool isMaxSpeedReached = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(speed, 0, 0);
    }

    void FixedUpdate()
    {
        // Apply Earth gravity to the ball
        rb.AddForce(new Vector3(0, -gravity, 0), ForceMode.Acceleration);

        // Apply drag to the ball to slow it down when it's not hitting anything
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity -= rb.velocity.normalized * drag * Time.deltaTime;
        }

        // Slow down the ball once the maximum speed is reached
        if (isMaxSpeedReached)
        {
            speed -= speedIncrement * Time.deltaTime * 10;
            speed = Mathf.Max(speed, minSpeed);
            rb.velocity = rb.velocity.normalized * speed;
        }

        // Destroy the ball if speed reaches 0
        if (speed == 5f)
        {
            Invoke("DestroyBalls", 10f); ;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Reflect the ball's velocity when it collides with a surface
        Vector3 reflection = Vector3.Reflect(rb.velocity, other.contacts[0].normal);

        // Add a slight curvature to the reflection
        Vector3 curve = Vector3.Cross(Vector3.up, reflection).normalized * 0.1f;
        reflection += curve;

        // Set the ball's velocity to the reflected velocity
        rb.velocity = reflection;

        // Increase the ball's speed by a small amount
        speed += speedIncrement;

        // Cap the ball's speed at a maximum value
        speed = Mathf.Min(speed, maxSpeed);

        // Set the flag to indicate that the maximum speed has been reached
        if (speed == maxSpeed)
        {
            isMaxSpeedReached = true;
        }

        // Bounce the ball in the direction opposite to the collision
        Vector3 bounceDirection = -other.contacts[0].normal;
        rb.velocity = Vector3.Reflect(rb.velocity, bounceDirection);

        // Apply the new speed to the ball's velocity
        rb.velocity = rb.velocity.normalized * speed;
    }

    public void DestroyBalls()
    {
        Destroy(gameObject);
    }
}


















