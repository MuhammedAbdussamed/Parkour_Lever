using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionAsset inputs;
    [SerializeField] internal float mouseSensitivity;

    // Action Maps
    private InputActionMap movementMap;

    // Actions
    private InputAction move;
    private InputAction turn;
    private InputAction jump;

    // Events
    public Action<Vector2> OnMoveEvent;
    public Action<Vector2> OnTurnEvent;
    public Action OnJumpEvent;

    void OnEnable()
    {
        move.performed += OnMovePerformed;
        move.canceled += OnMoveCanceled;

        turn.performed += ReadMouseInput;
        turn.canceled += StopTurning;

        jump.performed += OnJumpInput;
    }

    void OnDisable()
    {
        move.performed -= OnMovePerformed;
        move.canceled -= OnMoveCanceled;

        turn.performed -= ReadMouseInput;
        turn.canceled -= StopTurning;

        jump.performed -= OnJumpInput;
    }

    void Awake()
    {
        AssignInputs();
    }

    private void AssignInputs()
    {

        movementMap = inputs.FindActionMap("Movement");
        move = movementMap.FindAction("Move");
        turn = movementMap.FindAction("Turn");
        jump = movementMap.FindAction("Jump");

        inputs.Enable();
    }

    /*---------Input Functions---------*/

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    /*---*/

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(Vector2.zero);
    }

    /*--------------------------------*/

    private void ReadMouseInput(InputAction.CallbackContext context)
    {
        OnTurnEvent?.Invoke(context.ReadValue<Vector2>());
    }

    /*---*/

    private void StopTurning(InputAction.CallbackContext context)
    {
        OnTurnEvent?.Invoke(Vector2.zero);
    }

    /*--------------------------------*/
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        OnJumpEvent?.Invoke();
    }
}
