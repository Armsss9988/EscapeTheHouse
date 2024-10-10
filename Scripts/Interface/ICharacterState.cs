public interface ICharacterState
{
    void EnterState();
    void UpdateState();
    void ExitState();
    void FixedUpdateState();
    void OnAnimatorMoveState();

}
