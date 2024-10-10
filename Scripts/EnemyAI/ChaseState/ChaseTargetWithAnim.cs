using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "ChaseToTargetWithAnim", menuName = "ChaseState/ChaseToTargetWithAnim")]
public class ChaseTargetWithAnim : ChaseToTarget
{
    public override void EnterState(AIController aiController)
    {
        base.EnterState(aiController);
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.updateRotation = true;
        aiController.animator.SetBool("isRunning", true);
    }

    public override void ExitState(AIController aiController)
    {
        base.ExitState(aiController);
        aiController.animator.SetBool("isRunning", false);
    }
    public override void OnAnimatorMoveState(AIController aiController)
    {
        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        Animator animator = aiController.animator;
        Vector3 position = animator.rootPosition;
        position.y = agent.nextPosition.y;
        aiController.transform.position = position;
        agent.nextPosition = aiController.transform.position;
    }

}
