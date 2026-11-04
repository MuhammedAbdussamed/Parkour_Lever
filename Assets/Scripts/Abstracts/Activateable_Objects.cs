using UnityEngine;

public class Activateable_Objects : Interactable_Objects
{
    public bool isActive;

    public bool IsActive => isActive;

    public virtual void TurnOn()
    {
        isActive = true;
        Debug.Log("Açıldı");
    }

    public virtual void TurnOff() { }

    
}
