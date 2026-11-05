using System;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Input_Manager inputManager;
    private Player_Stats playerStats;
    private Ground_Checker groundChecker;

    // Components
    private Rigidbody rb;

    // Variables
    private Vector2 movementDirection;
    private Vector2 mouseTurnValue;

    void OnEnable()
    {
        inputManager.OnMoveEvent += ReadMoveInputValue;
        inputManager.OnTurnEvent += ReadMouseTurnValue;
        inputManager.OnJumpEvent += Jump;
        inputManager.OnRunEvent += Run;
    }

    void OnDisable()
    {
        inputManager.OnMoveEvent -= ReadMoveInputValue;
        inputManager.OnTurnEvent -= ReadMouseTurnValue;
        inputManager.OnJumpEvent -= Jump;
        inputManager.OnRunEvent -= Run;
    }

    void Awake()
    {
        groundChecker = GetComponent<Ground_Checker>();
        playerStats = GetComponent<Player_Stats>();

        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Turn();
        DetectPlayerStamina();
        StopRunning();
    }

    private void FixedUpdate()
    {
        Move();
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
            direction.x * playerStats.characterSpeed,
            rb.linearVelocity.y,
            direction.z * playerStats.characterSpeed
        );
    }

    /*---*/

    private void Turn()
    {
        rb.MoveRotation(
            rb.rotation * Quaternion.Euler(0f, mouseTurnValue.x * inputManager.MouseSensitivity, 0f)
        );

        mouseTurnValue.x = 0f;
    }

    /*---*/
    private void Jump()
    {
        Debug.Log("Zıplıyor");
        if (groundChecker.OnGround)
        {
            rb.AddForce(Vector3.up * playerStats.jumpPower * 1000, ForceMode.Force);
        }
    }

    /*---*/

    private void Run()
    {
        if (!playerStats.isTired)
        {
            playerStats.characterSpeed = Mathf.Lerp(
                playerStats.characterSpeed,
                playerStats.defaultCharacterSpeed * 1.5f,
                1f
                );

            playerStats.stamina -= Time.deltaTime * playerStats.staminaDecreasingValue;
        }
    }

    /*---*/

    private void DetectPlayerStamina()
    {
        if(playerStats.stamina < 0.05)
        {
            playerStats.isTired = true;
        }
        else
        {
            playerStats.isTired = false;
        }
    }

    private void StopRunning()
    {
        if (playerStats.isTired)
        {
            playerStats.characterSpeed = Mathf.Lerp(
                playerStats.characterSpeed,
                playerStats.defaultCharacterSpeed,
                1f
                );

            playerStats.stamina += Time.deltaTime * playerStats.staminaIncreasingValue;
        }
    }

    private void StopRunningHandle()
    {

    }

    /*---*/
}



    #endregion
