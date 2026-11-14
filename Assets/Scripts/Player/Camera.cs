using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private InputManager inputManager;
    

    // Variables
    private float turnDirection;
    private float xRotation;
    private Vector2 mouseTurnValue;
    public static Vector3 cameraPosition;


    void OnEnable()
    {
        inputManager.OnTurnEvent += ReadMouseValue;
    }

    void OnDisable()
    {
        inputManager.OnTurnEvent -= ReadMouseValue;
    }

    void Update()
    {
        TurnUpDown();
        cameraPosition = transform.position + new Vector3(0, 0, 0);
        
    }

    private void ReadMouseValue(Vector2 mouseTurnInput)
    {
        mouseTurnValue = mouseTurnInput;
    }

    void TurnUpDown()
    {
        xRotation -= mouseTurnValue.y * inputManager.MouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
