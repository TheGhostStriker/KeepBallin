using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioClip bounceClip;


    
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
        if (speed <= 2f)
        {
            Invoke("DestroyBalls", 1f); 
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // Calculate the bounce direction based on the collision normal
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity.normalized, other.contacts[0].normal).normalized;

            // Apply the bounce velocity to the ball
            rb.velocity = bounceDirection * speed;

            // Increase the ball's speed by a small amount
            speed += speedIncrement;

            // Cap the ball's speed at a maximum value
            speed = Mathf.Min(speed, maxSpeed);

            // Set the flag to indicate that the maximum speed has been reached
            if (speed == maxSpeed)
            {
                isMaxSpeedReached = true;
            }

            GetComponent<AudioSource>().clip = bounceClip;
            GetComponent<AudioSource>().Play();

            Debug.Log("DoingAllTheBallThings");
        }
    }





    public void DestroyBalls()
    {
        Destroy(gameObject);
    }
}


















