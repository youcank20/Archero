using UnityEngine;

public enum State
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    Hitted = 3,
    Die = 4,
}

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement _instance;

    public static PlayerMovement Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerMovement>();

            return _instance;
        }
    }

    public State PlayerState { get; private set; } = State.Idle;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform rotateTransform;

    private Rigidbody _rigidbody;
    private float _speed = 5f;
    private PlayerAttack _playerAttack;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void FixedUpdate()
    {
        if (Joystick.Instance.NormalizedDirection != Vector3.zero)
        {
            Vector3 joystickDirection = new Vector3(Joystick.Instance.NormalizedDirection.x, 0, Joystick.Instance.NormalizedDirection.y);

            transform.Translate(joystickDirection * _speed * Time.deltaTime, Space.World);
            LookAt(transform.position + joystickDirection);

            ChangeState(State.Move);
        }
        else
        {
            if (_playerAttack.HasTarget)
                ChangeState(State.Attack);
            else
                ChangeState(State.Idle);
        }
    }

    private void ChangeState(State state)
    {
        if (PlayerState == state)
            return;

        PlayerState = state;

        animator.SetInteger("State", (int)PlayerState);
    }

    public void LookAt(Vector3 position)
    {
        rotateTransform.LookAt(position);
    }

    public Quaternion GetRotation()
    {
        return rotateTransform.rotation;
    }
}
