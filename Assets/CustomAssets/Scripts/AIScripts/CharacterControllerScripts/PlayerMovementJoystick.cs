using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerMovementJoystick : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSensitivity = 0.5f;
    [SerializeField] private float _crouchSpeed = 0.5f;
    [SerializeField] private float _crouchDuration = 1.0f;
    [SerializeField] private float _uncrouchDuration = 1.0f;

    private bool _isCrouching = false;
    private float _crouchStartTime = 0.0f;
    public AudioClip movementSound;

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 joystickMovement = new Vector3(_joystick.Horizontal * _turnSensitivity, 0f, _joystick.Vertical) * _moveSpeed;
        Vector3 wasdMovement = new Vector3(horizontal * _turnSensitivity, 0f, vertical) * _moveSpeed;

        Vector3 movement = joystickMovement + wasdMovement;

        if (movement.magnitude > 0.01f)
        {
            if (!_isCrouching && _rigidbody.velocity.magnitude > 0f && !GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = movementSound;
                GetComponent<AudioSource>().Play();

                Debug.Log("Playing movement sound");
            }
            // Invert the movement vector if joystick or W key is moving downwards
            if (_joystick.Vertical < 0f || vertical < 0f)
            {
                Crouch();
                return;
            }

            // If crouching, move slower
            float speed = (_isCrouching) ? _crouchSpeed : _moveSpeed;

            // Transform the movement vector from local space to world space
            Vector3 worldMovement = transform.TransformDirection(movement.normalized * speed);

            // Cast a ray in front of the player to check for obstacles
            RaycastHit hit;
            if (Physics.Raycast(transform.position, worldMovement, out hit, worldMovement.magnitude * Time.fixedDeltaTime))
            {
                // Stop the player from moving forward if there is an obstacle
                worldMovement = hit.distance * hit.normal / Time.fixedDeltaTime;
            }

            // Apply the movement to the Rigidbody
            _rigidbody.MovePosition(transform.position + worldMovement * Time.fixedDeltaTime);

            // Rotate the character to face the movement direction
            transform.rotation = Quaternion.LookRotation(worldMovement.normalized, Vector3.up);
        }
        else
        {
            // If not moving, stop crouching
            StopCrouch();
        }
    }

    private void Crouch()
    {
        // Only crouch if not already crouching
        if (!_isCrouching)
        {
            _isCrouching = true;
            _crouchStartTime = Time.time;

            // Reduce the height of the character's collider
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x, collider.size.y / 2f, collider.size.z);

            // Play crouch animation or do other crouch-related stuff
        }
    }

    private void StopCrouch()
    {
        // Only uncrouch if currently crouching
        if (_isCrouching)
        {
            // If crouch duration has elapsed, uncrouch
            if (Time.time - _crouchStartTime >= _crouchDuration)
            {
                _isCrouching = false;

                // Increase the height of the character's collider
                BoxCollider collider = GetComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, collider.size.y * 2f, collider.size.z);

                // Play uncrouch animation or do other uncrouch-related stuff
            }
        }
    }
}













