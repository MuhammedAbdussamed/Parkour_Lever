using UnityEngine;

public class Button : Activateable_Objects
{
    public float activationDuration;
    public float defaultDurationValue;

    public void Awake()
    {
        defaultDurationValue = activationDuration;
        activationDuration = 0f;
    }

    public override void TurnOn()
    {
        base.TurnOn();
        activationDuration = defaultDurationValue;
    }

    public void Update()
    {
        activationDuration -= Time.deltaTime;
        activationDuration = Mathf.Clamp(activationDuration, 0, defaultDurationValue);
        TurnOff();
    }

    public override void TurnOff()
    {
        if (isActive && activationDuration < 0.15f)
        {
            isActive = false;
            InvokePowerEvent();
            Debug.Log("Kapandı");
            
        }
    }
}
