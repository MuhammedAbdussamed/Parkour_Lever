using UnityEngine;

public class Button : BaseActivatable
{
    public float activationDuration;
    public float defaultDurationValue;

    public void Awake()
    {
        defaultDurationValue = activationDuration;
        activationDuration = 0f;
    }

    public void Update()
    {
        activationDuration -= Time.deltaTime;
        activationDuration = Mathf.Clamp(activationDuration, 0, defaultDurationValue);
        TurnOff();
    }

    #region ----- Functions -----

    public override void TurnOff()
    {
        if (isActive && activationDuration < 0.15f)
        {
            isActive = false;
            InvokePowerEvent();
        }
    }

    /*-----------------------------------*/

    public override void TurnOn()
    {
        base.TurnOn();
        activationDuration = defaultDurationValue;
    }

    #endregion
}
