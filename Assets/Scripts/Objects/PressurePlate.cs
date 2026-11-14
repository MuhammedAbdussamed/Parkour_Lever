using System;
using UnityEngine;

public class PressurePlate : Activateable_Objects
{
    [Header("Mass Activate Value")]
    [SerializeField] private int massActivateValue;
    private Collision? currentCol;
    private event Action OnActiveEvent;

    void OnCollisionEnter(Collision col)
    {
        currentCol = col;
    }

    private void OnCollisionStay()
    {
        if (currentCol == null) 
            return; 

        Rigidbody colRb = currentCol.gameObject.GetComponent<Rigidbody>();

        if (colRb.mass > massActivateValue)
        {
            TurnOn();
            Debug.Log("Basınç Plakası Aktif !");
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col == currentCol)
        {
            currentCol = null;
            TurnOff();
            Debug.Log("Basınç plakası deaktif !");
        }
    }

    public override void TurnOff()
    {
        isActive = false;
        InvokePowerEvent();
    }
}
