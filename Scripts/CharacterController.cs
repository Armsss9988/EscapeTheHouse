using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    private AudioSource m_AudioSource;
    [SerializeField] private float soundSpeed;
    [SerializeField] private float maxSoundSpeed;
    [SerializeField] private float turnSpeed = 200f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] protected float accelerationTime;
    [SerializeField] private CharacterState currentState;

    public Animator Animator { get { return m_Animator; } }
    public Rigidbody Rigidbody { get { return m_Rigidbody; } }
    public AudioSource AudioSource { get { return m_AudioSource; } }
    public float Speed => speed;
    public float MaxSpeed => maxSpeed;
    public float SoundSpeed => soundSpeed;

    public float MaxSoundSpeed => maxSoundSpeed;
    public Transform CameraTransform { get { return cameraTransform; } }
    public float AccelerationTime => accelerationTime;
    public float TurnSpeed => turnSpeed;



    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator.applyRootMotion = true;
        m_AudioSource = GetComponent<AudioSource>();
        ChangeState(new Idle(this));
    }

    public void ChangeState(CharacterState newState)
    {
        if (currentState != null) currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    void Update()
    {
        currentState.UpdateState();
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }
    private void OnAnimatorMove()
    {
        currentState.OnAnimatorMoveState();
    }




    public void StopAudio()
    {
        m_AudioSource.Stop();
    }
}
