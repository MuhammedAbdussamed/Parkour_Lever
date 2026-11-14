using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionAsset inputs;
    [SerializeField] private float mouseSensitivity;
    public float MouseSensitivity => mouseSensitivity;

    // Action Maps
    private InputActionMap movementMap;

    // Actions
    private InputAction move;
    private InputAction turn;
    private InputAction jump;
    private InputAction interact;
    private InputAction run;

    // Events
    public event Action<Vector2> OnMoveEvent;   // Public event kapsülleme örneğidir
    public event Action<Vector2> OnTurnEvent;   // Diğer scriptler Invoke edemez
    public event Action<bool> OnRunEvent;
    public event Action OnJumpEvent;
    public event Action OnInteractionEvent;

    void OnEnable()
    {
        move.performed += OnMovePerformed;
        move.canceled += OnMoveCanceled;

        turn.performed += ReadMouseInput;
        turn.canceled += StopTurning;

        interact.performed += CheckInteractions;

        run.performed += Run;
        run.canceled += StopRunning;

        jump.performed += OnJumpInput;
    }

    void OnDisable()
    {
        move.performed -= OnMovePerformed;
        move.canceled -= OnMoveCanceled;

        turn.performed -= ReadMouseInput;
        turn.canceled -= StopTurning;

        interact.performed -= CheckInteractions;

        run.performed -= Run;
        run.canceled -= StopRunning;

        jump.performed -= OnJumpInput;
    }

    void Awake()
    {
        AssignInputs();
    }

    #region --- Input Functions ---

    /*---------- Movement ----------*/

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    /*---*/

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(Vector2.zero);
    }

    /*------------ Run --------------*/

    private void Run(InputAction.CallbackContext context)
    {
        OnRunEvent?.Invoke(true);
    }

    /*---*/

    private void StopRunning(InputAction.CallbackContext context)
    {
        OnRunEvent?.Invoke(false);
    }

    /*----------- Mouse & Turn --------------*/

    private void ReadMouseInput(InputAction.CallbackContext context)
    {
        OnTurnEvent?.Invoke(context.ReadValue<Vector2>());
    }

    /*---*/

    private void StopTurning(InputAction.CallbackContext context)
    {
        OnTurnEvent?.Invoke(Vector2.zero);
    }

    /*--------- Interaction ---------*/

    private void CheckInteractions(InputAction.CallbackContext context)
    {
        OnInteractionEvent?.Invoke();
    }

    /*------------ Jump --------------*/

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        OnJumpEvent?.Invoke();
    }

    /*---------- Assigns ------------*/

    private void AssignInputs()
    {

        movementMap = inputs.FindActionMap("Movement");
        move = movementMap.FindAction("Move");
        turn = movementMap.FindAction("Turn");
        jump = movementMap.FindAction("Jump");
        interact = movementMap.FindAction("Interaction");
        run = movementMap.FindAction("Run");

        inputs.Enable();
    }

    #endregion
}
