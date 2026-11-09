using System;
using UnityEngine;
using System.Collections;

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
    private bool isShiftPressed;
    private bool isResting;

    void OnEnable()
    {
        inputManager.OnMoveEvent += ReadMoveInputValue;
        inputManager.OnTurnEvent += ReadMouseTurnValue;
        inputManager.OnJumpEvent += Jump;
        inputManager.OnRunEvent += ShiftPressedControl;
    }

    void OnDisable()
    {
        inputManager.OnMoveEvent -= ReadMoveInputValue;
        inputManager.OnTurnEvent -= ReadMouseTurnValue;
        inputManager.OnJumpEvent -= Jump;
        inputManager.OnRunEvent -= ShiftPressedControl;
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
        Run();
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

    /*-------------- Run --------------*/

    private void ShiftPressedControl(bool isShiftPressed)
    {
        this.isShiftPressed = isShiftPressed;
    }

    /*---*/

    private void Run()
    {
        if (!isShiftPressed || playerStats.stamina < 0.1f)
        {
            playerStats.characterSpeed = Mathf.Lerp(
                playerStats.characterSpeed,
                playerStats.defaultCharacterSpeed,
                1f
                );

            if (!isResting)
            { StartCoroutine(IncreaseStamina(1)); }
        }

        else
        {
            playerStats.characterSpeed = Mathf.Lerp(
                playerStats.characterSpeed,
                playerStats.defaultCharacterSpeed * 1.5f,
                1f
            );

            DecreaseStamina();
        }
    }

    /*--------------- Stamina ---------------*/

    IEnumerator IncreaseStamina(int valueSign)      // +1 = Stamina artar. -1 = Stamina azalır
    {
        isResting = true;
        yield return new WaitForSeconds(3f);

        while (playerStats.stamina < 100f && !isShiftPressed)
        {
            playerStats.stamina += Time.deltaTime * playerStats.staminaIncreasingValue;
            yield return null;
        }

        isResting = false;
    }

    /*---*/

    private void DecreaseStamina()
    {
        playerStats.stamina -= Time.deltaTime * playerStats.staminaIncreasingValue;
    }

    #endregion
}




