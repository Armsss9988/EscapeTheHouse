using UnityEngine;

public class Interact : CharacterState
{
    public Interact(CharacterController characterController) : base(characterController)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        m_Animator.SetTrigger("isInteracting");
    }

    public override void ExitState()
    {

        base.ExitState();

    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (IsInteractionAnimationFinished())
        {

            characterController.ChangeState(new Idle(characterController));
        }

    }
    private bool IsInteractionAnimationFinished()
    {
        AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsTag("Interact"))
        {

            if (stateInfo.normalizedTime >= 1.0f)
            {
                return true;
            }
        }
        return false;
    }
}
