using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Input_Manager inputManager;

    [Header("Stats")]
    [SerializeField] private float characterSpeed;

    // Components
    private Rigidbody rb;

    // Variables
    private Vector2 movementDirection;
    private Vector2 mouseTurnValue;

    void OnEnable()
    {
        inputManager.OnMoveEvent += ReadMoveInputValue;
        inputManager.OnTurnEvent += ReadMouseTurnValue;
    }

    void OnDisable()
    {
        inputManager.OnMoveEvent -= ReadMoveInputValue;
        inputManager.OnTurnEvent -= ReadMouseTurnValue;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
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
        mouseTurnValue = turnValue;
    }

    /*---------- Movement ----------*/
    private void Move()
    {
        Vector3 direction = new Vector3(movementDirection.x, rb.linearVelocity.y, movementDirection.y);

        rb.linearVelocity = new Vector3(
            direction.x * characterSpeed,
            rb.linearVelocity.y,
            direction.z * characterSpeed
        );
    }

    /*---*/

    private void Turn()
    {
        transform.Rotate(-mouseTurnValue.y, mouseTurnValue.x, 0);
    }

    /*---*/

    #endregion
}
