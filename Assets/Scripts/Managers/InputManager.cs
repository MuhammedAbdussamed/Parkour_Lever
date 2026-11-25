using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionAsset inputs;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float enableInputDelay;
    public float MouseSensitivity => mouseSensitivity;

    // Action Maps
    private InputActionMap movementMap;

    // Actions
    private InputAction move;
    private InputAction turn;
    private InputAction jump;
    private InputAction interact;
    private InputAction run;
    private InputAction turnObject;

    // Events
    public event Action<Vector2> OnMoveEvent;   // Public event kapsülleme örneğidir
    public event Action<Vector2> OnTurnEvent;   // Diğer scriptler Invoke edemez
    public event Action<bool> OnRunEvent;
    public event Action OnJumpEvent;
    public static event Action<bool> OnTurnObjectEvent;
    public static event Action OnInteractionEvent;

    void OnEnable()
    {
        AssignInputToEvents(true);
    }

    void OnDisable()
    {
        AssignInputToEvents(false);
    }

    void Awake()
    {
        AssignInputs();
        inputs.Disable();
    }

    void Start()
    {
        Invoke("EnableInputs", enableInputDelay);
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

    /*---*/

    private void TurnObject(InputAction.CallbackContext context)
    {
        OnTurnObjectEvent?.Invoke(true);
    }

    private void StopTurnObject(InputAction.CallbackContext context)
    {
        OnTurnObjectEvent?.Invoke(false);
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
        turnObject = movementMap.FindAction("TurnObject");
    }

    /*---*/

    private void AssignInputToEvents(bool assign)
    {
        if (assign)
        {
            move.performed += OnMovePerformed;
            move.canceled += OnMoveCanceled;

            turn.performed += ReadMouseInput;
            turn.canceled += StopTurning;

            interact.performed += CheckInteractions;

            run.performed += Run;
            run.canceled += StopRunning;

            jump.performed += OnJumpInput;

            turnObject.started += TurnObject;
            turnObject.canceled += StopTurnObject;
        }

        else
        {
            move.performed -= OnMovePerformed;
            move.canceled -= OnMoveCanceled;

            turn.performed -= ReadMouseInput;
            turn.canceled -= StopTurning;

            interact.performed -= CheckInteractions;

            run.performed -= Run;
            run.canceled -= StopRunning;

            jump.performed -= OnJumpInput;

            turnObject.performed -= TurnObject;
            turnObject.canceled -= StopTurnObject;
        }
    }

    /*---*/

    private void EnableInputs()
    {
        inputs.Enable();
    }

    #endregion
}
