using UnityEngine;

public class Lever : Activateable_Objects
{
    public override void TurnOff()
    {
        if (isActive)
        {
            isActive = false;
            Debug.Log("Kapandı");
        }
    }
    
}
