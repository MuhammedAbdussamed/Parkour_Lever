using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private Input_Manager inputManager;
    private float xRotation;
    private Vector2 mouseTurnValue;

    // Variables
    private float turnDirection;

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
