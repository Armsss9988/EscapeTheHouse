using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "ChaseState", menuName = "ChaseState")]
public class ChaseState : AIState
{

    [SerializeField] protected float speed = 1f;


    public override void EnterState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.ResetPath();
        agent.isStopped = false;
    }

    public override void ExitState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.ResetPath();
        agent.isStopped = true;
    }

    public override void UpdateState(AIController aiController)
    {
    }
}
