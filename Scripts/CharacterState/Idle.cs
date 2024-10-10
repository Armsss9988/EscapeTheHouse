public class Idle : MovementState
{

    public Idle(CharacterController characterController) : base(characterController)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        m_Animator.SetBool("isWalking", false);
        m_Animator.SetBool("isRunning", false);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        if (IsMoving())
        {
            characterController.ChangeState(new Walk(characterController));
        }

    }
}
