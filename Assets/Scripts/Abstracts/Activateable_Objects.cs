using System;
using UnityEngine;

public abstract class Activateable_Objects : MonoBehaviour
{
    public bool isActive;

    public bool IsActive => isActive;

    public event Action OnPowerEvent;

    public virtual void TurnOn()
    {
        isActive = true;
        OnPowerEvent?.Invoke();
        Debug.Log("Açıldı");
    }

    public virtual void TurnOff() { }

    
}
