using UnityEngine;

public abstract class Activateable_Objects : MonoBehaviour
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
