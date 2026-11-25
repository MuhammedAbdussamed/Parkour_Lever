using Newtonsoft.Json.Bson;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    [Header("Raycast Layers")]
    [SerializeField] private LayerMask layers;

    [Header("Interactions Stats")]
    [SerializeField] private float objectTurnSpeed;
    private float objectTurnAngle;

    // Events
    public event Action<bool> InteractTextEvent;

    // Variables
    private Collider interactingObject;
    private bool wasInteracting;
    private bool isObjTurning;

    void OnEnable()
    {
        InputManager.OnInteractionEvent += DetectObjectType;
        InputManager.OnTurnObjectEvent += CheckObjTurning;
    }

    void OnDisable()
    {
        InputManager.OnInteractionEvent -= DetectObjectType;
        InputManager.OnTurnObjectEvent -= CheckObjTurning;
    }

    void Awake()
    {
        objectTurnAngle = 35f;
    }

    void Update()
    {
        interactingObject = SendRaycastToMouse();
        TurnObject();
    }

    #region ----- Functions -----

    private void DetectObjectType()
    {
        if (interactingObject == null)
        { return; }

        if (interactingObject.TryGetComponent<BaseActivationSource>(out var activetableScript))
        {
            InteractActivetableObjects(activetableScript);
        }

        else if (interactingObject.TryGetComponent<BaseInteractable>(out BaseInteractable script))
        {
            script.InteractFunction();
        }
    }

    /*------------------------------------------*/

    private void InteractActivetableObjects(BaseActivationSource objectScript)
    {
        if (objectScript.IsActive)
        {
            objectScript.TurnOff();
        }

        else
        {
            objectScript.TurnOn();
        }
    }

    /*------------------------------------------*/

    private void TurnObject()
    {
        if (wasInteracting && isObjTurning)
        {
            Vector3 currentRotation = interactingObject.transform.eulerAngles;

            currentRotation.y = Mathf.MoveTowards(
                currentRotation.y,
                currentRotation.y + objectTurnAngle,
                objectTurnSpeed
                );

            interactingObject.transform.eulerAngles = currentRotation;
        }
    }

    /*------------------------------------------*/

    private void CheckObjTurning(bool isTurning)
    {
        isObjTurning = isTurning;
    }

    /*------------------------------------------*/

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

    #endregion
}
