using UnityEngine;

public class Lever : BaseActivatable
{
    public override void TurnOff()
    {
        if (isActive)
        {
            isActive = false;
        }
    }
    
}
