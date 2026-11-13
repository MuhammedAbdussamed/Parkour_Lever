using UnityEngine;

public class Moving_Platforms : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed;
    private int patrolIndex;

    void Update()
    {
        Patrol(patrolPoints);
    }

    void Patrol(Transform[] patrolPoints)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            patrolPoints[patrolIndex].position,
            speed * Time.deltaTime
            );

        float pointDistance = Vector3.Distance(transform.position, patrolPoints[patrolIndex].position);

        if (pointDistance <= 0.15f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
        
    }

    // 4 patrol pointli dizi olsun
    // index 0 => 0 / 4 => 0 OK
    // index 1 => 1 / 4 => 1 Ok
    // index 2 => 2 / 4 => 2 Ok
    // index 3 => 3 / 4 => 3 Ok
    // index 4 => 4 / 4 => 0 Ok 
   
}
