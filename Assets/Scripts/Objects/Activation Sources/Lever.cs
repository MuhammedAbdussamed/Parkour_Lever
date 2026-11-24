using UnityEngine;

public class Lever : BaseActivationSource
{
    private Animator leverAnim;

    private void Awake()
    {
        leverAnim = GetComponent<Animator>();
    }

    public override void TurnOn()
    {
        base.TurnOn();
        SetHandleAnimation(true);
    }

    public override void TurnOff()
    {
        if (isActive)
        {
            isActive = false;
            InvokePowerEvent();
            SetHandleAnimation(false);
        }
    }

    private void SetHandleAnimation(bool isOpened)
    {
        leverAnim.SetBool("isActive", isOpened);
    }
    
}
