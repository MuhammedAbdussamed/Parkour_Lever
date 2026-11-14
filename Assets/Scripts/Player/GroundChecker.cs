using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private LayerMask raycastLayers;
    private float raycastDistance;
    private bool onGround;
    public bool OnGround => onGround;

    void FixedUpdate()
    {
        GroundCheck();
    }
        
    private void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit ,raycastDistance,~raycastLayers)) 
        {
            onGround = true;
            Debug.DrawLine(
            transform.position,
            transform.position - transform.up * raycastDistance,
            Color.green
            );
        }
        else
        {
            onGround = false;
            Debug.DrawLine(
            transform.position,
            transform.position - transform.up * raycastDistance,
            Color.red
            );
        }
    }
}
