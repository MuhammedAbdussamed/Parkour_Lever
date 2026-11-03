using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [Header("Raycast Layers")]
    [SerializeField] private LayerMask layers;

    // Events
    public event Action<bool> InteractTextEvent;

    void Update()
    {
        SendRaycastToMouse();
    }

    void SendRaycastToMouse()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,10f,layers))
        {
            Debug.Log("Çarptı!");               // Ok
            InteractTextEvent?.Invoke(true);
        }
        else
        {
            InteractTextEvent?.Invoke(false);
        }
    }
}
