using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
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
    public event Action OnJumpEvent;
    public event Action OnInteractionEvent;
    public event Action OnRunEvent;



    void OnEnable()
    {
        move.performed += OnMovePerformed;
        move.canceled += OnMoveCanceled;

        turn.performed += ReadMouseInput;
        turn.canceled += StopTurning;

        interact.performed += CheckInteractions;

        run.performed += Run;

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
        interact = movementMap.FindAction("Interaction");
        run = movementMap.FindAction("Run");

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

    private void Run(InputAction.CallbackContext context)
    {
        Debug.Log("Koşuyorum");
        OnRunEvent?.Invoke();
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

    private void CheckInteractions(InputAction.CallbackContext context)
    {
        OnInteractionEvent?.Invoke();
    }

    /*--------------------------------*/
    private void OnJumpInput(InputAction.CallbackContext context)
    {
        OnJumpEvent?.Invoke();
    }
}
