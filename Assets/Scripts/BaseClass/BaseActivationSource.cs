using System;
using UnityEngine;

public abstract class BaseActivationSource : MonoBehaviour
{
    public bool isActive;

    public bool IsActive => isActive;

    public event Action OnPowerEvent;

    public event Action OnAnimationEvent;

    #region ----- Functions -----

    public virtual void TurnOn()
    {
        isActive = true;
        OnPowerEvent?.Invoke();
        OnAnimationEvent?.Invoke();
    }

    /*-----------------------------------*/

    public virtual void TurnOff() {}

    /*-----------------------------------*/

    protected void PlayStateChangeSound() {}

    /*-----------------------------------*/
    protected void InvokePowerEvent()
    {
        OnPowerEvent?.Invoke();
    }

    #endregion
}
