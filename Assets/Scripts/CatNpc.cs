using UnityEngine;
using UnityEngine.AI;

public class CatNpc : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private NavMeshAgent agent;
    private Transform currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        currentTarget = pointB;
        agent.SetDestination(currentTarget.position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            if (currentTarget == pointA)
            {
                currentTarget = pointB;
            }
            else
            {
                currentTarget = pointA;
            }

            agent.SetDestination(currentTarget.position);
        }
    }
}


