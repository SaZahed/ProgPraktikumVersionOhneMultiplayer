using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 endPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject endTarget = GameObject.FindGameObjectWithTag("End");
        if (endTarget != null)
        {
            endPosition = endTarget.transform.position;
            agent.SetDestination(endPosition);
        }
    }
}
