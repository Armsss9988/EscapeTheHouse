
using UnityEngine.AI;

public abstract class StateRegular : IAIStateRegular
{
    protected NavMeshAgent agent;

    public StateRegular(AIController controller)
    {
        agent = controller.GetNavMeshAgent();

    }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}