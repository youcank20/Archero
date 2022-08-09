using UnityEngine;

public enum State
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    Hit = 3,
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

            _rigidbody.velocity = joystickDirection * _speed;
            _rigidbody.rotation = Quaternion.LookRotation(joystickDirection);

            ChangeState(State.Move);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;

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
}
