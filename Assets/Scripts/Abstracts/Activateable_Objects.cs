using System;
using UnityEngine;

public abstract class Activateable_Objects : MonoBehaviour
{
    public bool isActive;

    public bool IsActive => isActive;

    public event Action OnPowerEvent;

    public event Action OnAnimationEvent;

    public virtual void TurnOn()
    {
        isActive = true;
        OnPowerEvent?.Invoke();
        OnAnimationEvent?.Invoke();
    }

    public virtual void TurnOff() { }

    protected void InvokePowerEvent()
    {
        OnPowerEvent?.Invoke();
    }
}
