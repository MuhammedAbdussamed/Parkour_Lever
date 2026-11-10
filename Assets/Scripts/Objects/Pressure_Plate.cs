using UnityEngine;

public class Pressure_Plate : Activateable_Objects
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
        if (currentCol == null) 
            return; 

        Rigidbody colRb = currentCol.gameObject.GetComponent<Rigidbody>();

        if (colRb.mass > massActivateValue)
            TurnOn();
    }

    void OnCollisionExit(Collision col)
    {
        if (col == currentCol)
        {
            currentCol = null;
            TurnOff();
        }
    }
}
