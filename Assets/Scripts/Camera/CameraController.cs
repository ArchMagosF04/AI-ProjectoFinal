using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private InputHandler input;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private float xRotation;
    private float yRotation;

    private float speedMultiplier;

    private void Awake()
    {
        input = GetComponent<InputHandler>();
    }

    private void Start()
    {
        speedMultiplier = 1f;

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    private void Update()
    {
        MoveCamera();
        CameraRotation();
        ResetCameraPosition();
    }

    private void ResetCameraPosition()
    {
        if (input.ResetPosition)
        {
            input.UseResetPositionInput();
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            xRotation = transform.eulerAngles.x;
            yRotation = transform.eulerAngles.y;
        }
    }

    private void MoveCamera() //Moves the Camera
    {
        if (input.SpeedInput) speedMultiplier = 2.5f;
        else speedMultiplier = 1f;

        Vector3 movement = new Vector3(input.MoveInput.x, input.ElevationInput, input.MoveInput.y);
        transform.Translate(movement * speed * speedMultiplier * Time.unscaledDeltaTime);
    }

    private void CameraRotation() //Rotates the camera.
    {
        ToggleCursor(!input.ActivateLook);

        if (input.ActivateLook)
        {
            xRotation += input.LookInput.y;
            yRotation += input.LookInput.x;

            Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);

            transform.rotation = rotation;
        }
    }

    private void ToggleCursor(bool value)
    {
        Cursor.visible = value;

        if (value)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
