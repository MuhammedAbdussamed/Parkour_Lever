using UnityEngine;

public class Piston : BaseActivationReceivers
{
    [SerializeField] private Transform pistonPlate;
    [SerializeField] private float slideDistance;
    private float slideSpeed = 10f;

    protected override void OpenClose()
    {
        base.OpenClose();
       
        SlidePistonPlate();
    }

    private void SlidePistonPlate()
    {
        Rigidbody plateRb = pistonPlate.GetComponent<Rigidbody>();

        float distance = isActive ? slideDistance : -slideDistance;

        Vector3 targetPos = plateRb.position + transform.forward * distance;

        plateRb.MovePosition(targetPos);
    }
}
