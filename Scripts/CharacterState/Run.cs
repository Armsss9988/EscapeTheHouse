using System.Collections;
using UnityEngine;

public class Run : MovementState
{
    private Coroutine accelerationCoroutine;
    protected AudioSource m_AudioSource;
    protected float currentSoundSpeed;
    public Run(CharacterController characterController) : base(characterController)
    {
        m_AudioSource = characterController.AudioSource;
    }

    public override void EnterState()
    {
        base.EnterState();
        m_AudioSource.Play();
        m_Animator.SetBool("isRunning", true);
        accelerationCoroutine = characterController.StartCoroutine(Accelerate());
    }

    public override void ExitState()
    {
        base.ExitState();
        m_AudioSource.Stop();
        m_AudioSource.pitch = characterController.Speed;
        m_Animator.SetBool("isRunning", false);
        characterController.StopCoroutine(accelerationCoroutine);
        accelerationCoroutine = null;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        m_AudioSource.pitch = currentSoundSpeed;
        if (!IsMoving())
        {
            characterController.ChangeState(new Idle(characterController));
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            characterController.ChangeState(new Walk(characterController));
        }
        m_Animator.SetFloat("speed", currentSpeed);

    }
    private IEnumerator Accelerate()
    {
        float elapsedTime = 0f;
        while (elapsedTime < characterController.AccelerationTime)
        {
            currentSpeed = Mathf.Lerp(characterController.Speed, characterController.MaxSpeed, elapsedTime / characterController.AccelerationTime);
            currentSoundSpeed = Mathf.Lerp(characterController.SoundSpeed, characterController.MaxSoundSpeed, elapsedTime / characterController.AccelerationTime);
            m_AudioSource.pitch = currentSoundSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentSpeed = characterController.MaxSpeed;
        currentSoundSpeed = characterController.MaxSoundSpeed;
    }

}
