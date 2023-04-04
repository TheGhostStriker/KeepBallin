using System.Collections;
using UnityEngine;

public class AnimeBall : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float bounceForce = 5f;
    public float gravityScale = 0.17f;
    public AudioClip bounceSound;

    private Rigidbody rb;
    private bool isStopped = false;

    private float nextStopTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        nextStopTime = Time.time + Random.Range(10f, 25f);
    }

    private void FixedUpdate()
    {
        if (!isStopped)
        {
            rb.AddForce(Vector3.down * Physics.gravity.magnitude * gravityScale, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        if (transform.position.y > 30f)
        {
            rb.AddForce(Vector3.down * Physics.gravity.magnitude * gravityScale * 10f, ForceMode.Acceleration);
        }
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Bounce(collision.contacts[0].normal);
        }
    }

    private void Bounce(Vector3 collisionNormal)
    {
        float angle = Random.Range(10f, 30f); // Random angle of deflection in degrees
        float slope = Random.Range(0.1f, 0.3f); // Random slope factor
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.Cross(Vector3.up, collisionNormal));
        Vector3 direction = rotation * (collisionNormal + new Vector3(slope, 0f, 0f));

        bounceForce = Mathf.Clamp(bounceForce + 1f, 0f, 50f); // Increase the bounce force
        rb.AddForce((direction + Vector3.up) * bounceForce, ForceMode.Impulse);
    }

    IEnumerator StopAndPlayAudio()
    {
        isStopped = true;
        rb.velocity = Vector3.zero;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(bounceSound);
        yield return new WaitForSeconds(bounceSound.length);
        isStopped = false;
    }

    private void Update()
    {
        if (Time.time > nextStopTime) // Check if it's time to stop the ball and play the audio
        {
            StartCoroutine(StopAndPlayAudio());
            nextStopTime = Time.time + Random.Range(10f, 25f); // Set the next stop time to a random value between 10 and 25 seconds from now
        }
    }
}








