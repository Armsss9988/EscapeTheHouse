using UnityEngine;

public class Walk : MovementState
{
    protected AudioSource m_AudioSource;
    protected float currentSoundSpeed;
    public Walk(CharacterController characterController) : base(characterController)
    {
        m_AudioSource = characterController.AudioSource;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_AudioSource.Play();
        m_Animator.SetBool("isWalking", true);
        currentSpeed = characterController.Speed;
        m_AudioSource.pitch = characterController.SoundSpeed;
        m_Animator.SetFloat("speed", currentSpeed);
    }

    public override void ExitState()
    {
        base.ExitState();
        m_AudioSource.Stop();
        m_Animator.SetBool("isWalking", false);
    }

    public override void UpdateState()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            characterController.ChangeState(new Run(characterController));
        }
        if (!IsMoving())
        {
            characterController.ChangeState(new Idle(characterController));
        }
    }
}
