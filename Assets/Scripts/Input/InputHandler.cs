using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public float ElevationInput { get; private set; }
    public bool ResetPosition { get; private set; }
    public bool SpeedInput { get; private set; }
    public bool ActivateLook { get; private set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnElevationInput(InputAction.CallbackContext context)
    {
        ElevationInput = context.ReadValue<float>();
    }

    public void OnResetPositionInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ResetPosition = true;
        }
        if (context.canceled)
        {
            ResetPosition = false;
        }
    }

    public void UseResetPositionInput() => ResetPosition = false;

    public void OnSpeedInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SpeedInput = true;
        }
        if (context.canceled)
        {
            SpeedInput = false;
        }
    }

    public void OnActivateLookInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ActivateLook = true;
        }
        if (context.canceled)
        {
            ActivateLook = false;
        }
    }

    public void UseActivateLookInput() => ActivateLook = false;


    public void OnExitInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
}
