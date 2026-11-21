using UnityEngine;

public class Piston : BaseActivationReceivers
{
    [Header("Piston Parts")]
    [SerializeField] private Transform pistonPlate;
    [SerializeField] private Transform channel;
    [SerializeField] private Transform body;

    [Header("Piston Stats")]
    [SerializeField] private float slideDistance;
    [SerializeField] private float slideSpeed;

    // Variables
    private Vector3 closePos;
    private Vector3 openPos;
    private Vector3 defaultScale;
    private bool isOpenedOnce;

    private void Awake()
    {
        closePos = pistonPlate.position;
        openPos = pistonPlate.position + transform.forward * slideDistance;

        defaultScale = channel.localScale;
    }

    protected override void OpenClose()
    {
        base.OpenClose();
        isOpenedOnce = true;
    }

    private void FixedUpdate()
    {
        SlidePistonPlate();
    }

    private void SlidePistonPlate()
    {
        if (!isOpenedOnce) { return; }

        Rigidbody plateRb = pistonPlate.GetComponent<Rigidbody>();

        Vector3 targetPos = isActive ? openPos : closePos;

        Vector3 newPos = Vector3.MoveTowards(
            plateRb.position,
            targetPos,
            slideSpeed * Time.fixedDeltaTime
        );

        plateRb.MovePosition(newPos);

        ResizeChannel(isActive);
    }

    private void ResizeChannel(bool isActive)
    {
        float targetScale = isActive ? defaultScale.z + slideDistance : defaultScale.z;

        Vector3 currentScale = channel.localScale;

        currentScale.z = Mathf.MoveTowards(
            currentScale.z,
            targetScale,
            slideSpeed * Time.fixedDeltaTime
            );

        channel.localScale = currentScale;

        RePositionChannel();
    }

    private void RePositionChannel()
    {
        float targetPosZ = (body.localPosition.z + pistonPlate.localPosition.z) / 2;

        float newChannelZValue = Mathf.MoveTowards(
            channel.localPosition.z,
            targetPosZ,
            slideSpeed * Time.fixedDeltaTime
            );

        Vector3 newChannelPos = channel.transform.localPosition;
        newChannelPos.z = newChannelZValue;
        channel.localPosition = newChannelPos;
    }
}

