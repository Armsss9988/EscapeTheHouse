
public class StateMachine
{
    private AIState currentState;
    private AIController controller;
    public StateMachine(AIController aiController)
    {
        this.controller = aiController;
    }
    public void ChangeState(AIState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(controller);
        }

        currentState = newState;
        currentState.EnterState(controller);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(controller);
        }
    }
    public void OnAnimatorMove()
    {
        if (currentState != null)
        {
            currentState.OnAnimatorMoveState(controller);
        }
    }
    public AIState GetState()
    {
        return currentState;
    }
}