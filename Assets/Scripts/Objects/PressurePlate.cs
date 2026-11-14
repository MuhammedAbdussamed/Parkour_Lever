using System;
using UnityEngine;

public class PressurePlate : BaseActivatable
{
    [Header("Mass Activate Value")]
    [SerializeField] private int massActivateValue;
    private Collision? currentCol;

    void OnCollisionEnter(Collision col)
    {
        currentCol = col;
    }

    private void OnCollisionStay()
    {
        StayActive();
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

    #region ----- Functions -----


    private void StayActive()
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

    /*------------------------------*/

    public override void TurnOff()
    {
        isActive = false;
        InvokePowerEvent();
    }
    
    #endregion
}
