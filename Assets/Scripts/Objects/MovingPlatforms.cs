using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float speed;
    private int patrolIndex;

    void Update()
    {
        Patrol(patrolPoints);
    }

    #region ----- Functions -----

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

    #endregion
}
