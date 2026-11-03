using UnityEngine;

public class Ground_Checker : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask raycastLayers;
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
