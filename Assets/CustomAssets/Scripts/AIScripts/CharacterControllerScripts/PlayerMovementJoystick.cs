using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    

    [SerializeField] private float _movementSmoothing = 0.1f;
    private Vector3 _originalJoystickMovement;
    private Vector3 _smoothedJoystickMovement;

    private const float _movementThreshold = 0.01f;

    private bool _isSpeedBoostActive = false;
    [SerializeField] private float _speedBoostMultiplier = 2.0f;
    [SerializeField] private float _speedBoostDuration = 2.0f;
    [SerializeField] private float _speedBoostCooldown = 5.0f;
    private float _speedBoostEndTime = 0.0f;
    private float _speedBoostCooldownEndTime = 0.0f;

    [SerializeField] private Button _speedBoostButton;
    [SerializeField] private Button _stopButton;

    private bool _isPlayerStopped = false;
    private bool _isMovementAllowed = true;

    

    private void Start()
    {
        _originalJoystickMovement = Vector3.zero;
        _speedBoostButton.onClick.AddListener(ActivateSpeedBoost);
        _stopButton.onClick.AddListener(StopPlayer);
    }

    private void FixedUpdate()
    {

        // Check if the player is stopped or movement is not allowed
        if (_isPlayerStopped || !_isMovementAllowed)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 joystickMovement = new Vector3(_joystick.Horizontal * _turnSensitivity, 0f, _joystick.Vertical) * _moveSpeed;
        Vector3 wasdMovement = new Vector3(horizontal * _turnSensitivity, 0f, vertical) * _moveSpeed;

        Vector3 movement = joystickMovement + wasdMovement;

        // Apply joystick movement smoothing
        _smoothedJoystickMovement = Vector3.Lerp(_smoothedJoystickMovement, movement, _movementSmoothing);

        // Check if joystick movement is below the threshold
        if (_smoothedJoystickMovement.magnitude <= _movementThreshold)
        {
            // Stop the player from moving
            _smoothedJoystickMovement = Vector3.zero;
        }

        if (_smoothedJoystickMovement.magnitude > 0.01f)
        {
            if (!_isCrouching && _rigidbody.velocity.magnitude > 0f && !GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = movementSound;
                GetComponent<AudioSource>().Play();

                Debug.Log("Playing movement sound");
            }

            if (_joystick.Vertical < 0f || vertical < 0f)
            {
                Crouch();
                return;
            }

            float speed = (_isCrouching) ? _crouchSpeed : _moveSpeed;

            // Apply speed boost if active
            if (_isSpeedBoostActive)
            {
                speed *= _speedBoostMultiplier;
                
            }

            Vector3 worldMovement = transform.TransformDirection(_smoothedJoystickMovement.normalized * speed);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, worldMovement, out hit, worldMovement.magnitude * Time.fixedDeltaTime))
            {
                worldMovement = hit.distance * hit.normal / Time.fixedDeltaTime;
            }

            // Apply movement with interpolation
            Vector3 targetPosition = transform.position + worldMovement * Time.fixedDeltaTime;
            _rigidbody.MovePosition(Vector3.Lerp(transform.position, targetPosition, _movementSmoothing));

            // Apply rotation with interpolation
            Quaternion targetRotation = Quaternion.LookRotation(worldMovement.normalized, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _movementSmoothing);
        }
        else
        {
            StopCrouch();
        }
    }

    private void Crouch()
    {
        if (!_isCrouching)
        {
            _isCrouching = true;
            _crouchStartTime = Time.time;

            BoxCollider collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(collider.size.x, collider.size.y / 2f, collider.size.z);
        }
    }

    private void StopCrouch()
    {
        if (_isCrouching)
        {
            if (Time.time - _crouchStartTime >= _crouchDuration)
            {
                _isCrouching = false;

                BoxCollider collider = GetComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, collider.size.y * 2f, collider.size.z);
            }
        }
    }

    private void ActivateSpeedBoost()
    {
        if (!_isSpeedBoostActive && Time.time >= _speedBoostCooldownEndTime)
        {
            _isSpeedBoostActive = true;
            _speedBoostEndTime = Time.time + _speedBoostDuration;
            _speedBoostCooldownEndTime = Time.time + _speedBoostCooldown;

            // Perform any UI or gameplay logic for speed boost activation
            Debug.Log("Speed boost activated!");

            StartCoroutine(DeactivateSpeedBoost());
        }
    }

    private IEnumerator DeactivateSpeedBoost()
    {
        yield return new WaitForSeconds(_speedBoostDuration);

        _isSpeedBoostActive = false;

        // Perform any UI or gameplay logic for speed boost deactivation
        Debug.Log("Speed boost deactivated!");
    }

    private void StopPlayer()
    {
        if (!_isPlayerStopped)
        {
            // Store the current smoothed joystick movement as the original joystick movement
            _originalJoystickMovement = _smoothedJoystickMovement;

            // Stop the player's movement
            _smoothedJoystickMovement = Vector3.zero;

            _isPlayerStopped = true;

            // Start a coroutine to automatically re-enable movement after a delay
            StartCoroutine(EnableMovementAfterDelay(1f));
        }
    }

    private IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Restore the original joystick movement
        _smoothedJoystickMovement = _originalJoystickMovement;

        // Reset the original joystick movement
        _originalJoystickMovement = Vector3.zero;

        _isPlayerStopped = false;
    }

    
}

















