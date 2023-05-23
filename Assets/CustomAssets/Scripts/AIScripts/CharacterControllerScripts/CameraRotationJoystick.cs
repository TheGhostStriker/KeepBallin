using UnityEngine;
using UnityEngine.UI;

public class CameraRotationJoystick : MonoBehaviour
{
    public Joystick joystick;
    public float rotationSpeed = 1f;
    public float maxRotationX = 10f;
    public float maxRotationY = 10f;

    private Transform cameraTransform;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    private void Start()
    {
        // Assuming the player has a camera as a child object, you can find it using GetComponentInChildren
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        // Get the joystick input values
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Calculate the rotation angles based on the input values
        float rotationX = vertical * rotationSpeed;
        float rotationY = horizontal * rotationSpeed;

        // Accumulate the rotation angles
        currentRotationX += rotationX;
        currentRotationY += rotationY;

        // Clamp the rotation angles to the desired range
        currentRotationX = Mathf.Clamp(currentRotationX, -maxRotationX, maxRotationX);
        currentRotationY = Mathf.Clamp(currentRotationY, -maxRotationY, maxRotationY);

        // Apply the rotation to the camera
        cameraTransform.localRotation = Quaternion.Euler(currentRotationX, currentRotationY, 0f);
    }
}



