using System;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Input_Manager inputManager;
    private Ground_Checker groundChecker;

    [Header("Stats")]
    [SerializeField] private float characterSpeed;
    [SerializeField] private float jumpPower;

    // Components
    private Rigidbody rb;

    // Variables
    private Vector2 movementDirection;
    private Vector2 mouseTurnValue;
    private float cameraSmoothing;

    void OnEnable()
    {
        inputManager.OnMoveEvent += ReadMoveInputValue;
        inputManager.OnTurnEvent += ReadMouseTurnValue;
        inputManager.OnJumpEvent += Jump;
    }

    void OnDisable()
    {
        inputManager.OnMoveEvent -= ReadMoveInputValue;
        inputManager.OnTurnEvent -= ReadMouseTurnValue;
        inputManager.OnJumpEvent += Jump;
    }

    void Awake()
    {
        groundChecker = GetComponent<Ground_Checker>();
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Turn();
    }

    private void FixedUpdate()
    {
        Move();

        rb.angularVelocity = Vector3.zero;
    }

    #region -------Functions-------

    /*-------- Read Values --------*/
    private void ReadMoveInputValue(Vector2 moveInputValue)
    {
        movementDirection = moveInputValue;
    }

    /*---*/

    private void ReadMouseTurnValue(Vector2 turnValue)
    {
        mouseTurnValue.x = turnValue.x;
    }

    /*---------- Movement ----------*/
    private void Move()
    {

        Vector3 lookDirection = transform.forward;
        Vector3 rightDirection = transform.right;

        Vector3 direction =
            lookDirection * movementDirection.y +
            rightDirection * movementDirection.x;

        rb.linearVelocity = new Vector3(
            direction.x * characterSpeed,
            rb.linearVelocity.y,
            direction.z * characterSpeed
        );
    }

    /*---*/

    private void Turn()
    {
        rb.MoveRotation(
            rb.rotation * Quaternion.Euler(0f, mouseTurnValue.x * inputManager.mouseSensitivity, 0f)
        );

        mouseTurnValue.x = 0f;
    }

    /*---*/
    private void Jump()
    {
        Debug.Log("Zıplıyor");
        if (groundChecker.onGround)
        {
            rb.AddForce(Vector3.up * jumpPower * 1000, ForceMode.Force);
        }
    }
}

    

    #endregion
