using UnityEngine;
using UnityEngine.AI;

public class VisionController : SenseController
{
    public override void OnDetectedTarget(Transform target)
    {
        aiController.Target = target;
        aiController.ChangeState(AIController.StateEnum.Chase);
    }

    public override void OnUndetectedTarget()
    {
        NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();

        aiController.ChangeState(AIController.StateEnum.Tracking);
        aiController.Target = null;
    }


}
