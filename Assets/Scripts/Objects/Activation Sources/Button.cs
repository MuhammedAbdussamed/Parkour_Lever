using UnityEngine;

public class Button : BaseActivationSource
{
    public float activationDuration;
    public float defaultDurationValue;
    private float turnOffThreshold;

    public void Awake()
    {
        defaultDurationValue = activationDuration;
        activationDuration = 0f;

        turnOffThreshold = 0.15f;
    }

    public void Update()
    {
        activationDuration -= Time.deltaTime;
        activationDuration = Mathf.Clamp(activationDuration, 0, defaultDurationValue);
        DurationTurnOff();
    }

    #region ----- Functions -----

    private void DurationTurnOff()
    {
        if (!isActive)
        { return; }

        if (activationDuration < turnOffThreshold)
        {
            isActive = false;
            InvokePowerEvent();
        }
    }

    /*-----------------------------------*/

    public override void TurnOff()
    {
        isActive = false;
        activationDuration = 0f;
        InvokePowerEvent();
    }

    /*-----------------------------------*/

    public override void TurnOn()
    {
        base.TurnOn();
        activationDuration = defaultDurationValue;
    }

    #endregion
}
