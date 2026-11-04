using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [Header("Raycast Layers")]
    [SerializeField] private LayerMask layers;
    [SerializeField] private Input_Manager inputManager;

    // Events
    public event Action<bool> InteractTextEvent;

    // Variables
    private Collider interactingObject;
    private bool wasInteracting;

    void OnEnable()
    {
        inputManager.OnInteractionEvent += InteractionWithObject;
    }

    void OnDisable()
    {
        inputManager.OnInteractionEvent -= InteractionWithObject;
    }

    void Update()
    {
        interactingObject = SendRaycastToMouse();
    }

    private void InteractionWithObject()
    {
        if (interactingObject == null)
        { return; }

        if (!interactingObject.TryGetComponent<Activateable_Objects>(out var objectScript))
        { return; }                 // Null Check

        if (objectScript.IsActive)
        {
            objectScript.TurnOff();
        }
        else
        {
            objectScript.TurnOn();
        }
    }

    private Collider SendRaycastToMouse()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,10f,layers))
        {
            if (!wasInteracting)
            {
                wasInteracting = true;
                InteractTextEvent?.Invoke(true);
                return hit.collider;                    // Çarptı ve etkileşimde değildi. 
            }
            return hit.collider;                        // Çarptı ve etkileşimde 
        }
        else
        {
            if (wasInteracting)
            {
                wasInteracting = false;
                InteractTextEvent?.Invoke(false);
                return null;                            // Çarpmadı ve etkileşimdeydi
            }
            return null;                                // Çarpmadı ve etkileşimde değildi. ( Default durum )
        }
    }
}
