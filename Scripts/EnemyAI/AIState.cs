using UnityEngine;

public abstract class AIState : ScriptableObject, IAIState
{
    public abstract void EnterState(AIController aiController);
    public abstract void ExitState(AIController aiController);

    public virtual void OnAnimatorMoveState(AIController aiController)
    {
    }

    public abstract void UpdateState(AIController aiController);

}