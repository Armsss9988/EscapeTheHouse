using UnityEngine;

public abstract class MovementState : CharacterState
{
    protected Vector3 m_Movement;

    protected float turnSpeed;
    protected Quaternion m_Rotation = Quaternion.identity;
    protected Transform cameraTransform;
    protected Rigidbody m_Rigidbody;

    protected float currentSpeed;

    protected MovementState(CharacterController characterController) : base(characterController)
    {

        cameraTransform = characterController.CameraTransform;
        m_Rigidbody = characterController.Rigidbody;
        turnSpeed = characterController.TurnSpeed;

    }

    public override void EnterState()
    {
        base.EnterState();

    }

    public override void ExitState()
    {
        base.ExitState();

    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0;
        Vector3 movementDirection = cameraRight * horizontal + cameraForward * vertical;
        m_Movement.Set(movementDirection.x, 0, movementDirection.z);
        m_Movement.Normalize();
        Vector3 desiredForward = Vector3.RotateTowards(characterController.transform.forward, m_Movement, turnSpeed * Time.fixedDeltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

    }

    public override void OnAnimatorMoveState()
    {
        base.OnAnimatorMoveState();
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Animator.deltaPosition);
        if (m_Rotation != Quaternion.identity) { m_Rigidbody.MoveRotation(m_Rotation); }

    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    public bool IsMoving() => Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0;

}
