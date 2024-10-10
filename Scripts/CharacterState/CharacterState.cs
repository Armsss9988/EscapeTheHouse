using UnityEngine;

public abstract class CharacterState : ICharacterState
{
    protected CharacterController characterController;
    protected Animator m_Animator;
    public CharacterState(CharacterController characterController)
    {
        this.characterController = characterController;
        m_Animator = characterController.Animator;

    }
    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void FixedUpdateState()
    {

    }

    public virtual void OnAnimatorMoveState()
    {

    }

    public virtual void UpdateState()
    {
    }
}
