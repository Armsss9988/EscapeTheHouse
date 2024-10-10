using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "PartrolWithAnim", menuName = "PatrolState/PartrolWithAnim")]
public class PatrolWithAnim : PatrolState
{
    public override void EnterState(AIController aiController)
    {
        base.EnterState(aiController);
        aiController.animator.SetBool("isWalking", true);

        NavMeshAgent agent = aiController.GetCachedComponent<NavMeshAgent>();
        agent.updatePosition = false;

    }
    public override void ExitState(AIController aiController)
    {
        base.ExitState(aiController);
        aiController.animator.SetBool("isWalking", false);
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
