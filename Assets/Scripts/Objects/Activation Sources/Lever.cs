using UnityEngine;

public class Lever : BaseActivationSource
{
    public override void TurnOff()
    {
        if (isActive)
        {
            isActive = false;
            InvokePowerEvent();
        }
    }
    
}
