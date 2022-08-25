using UnityEngine;

public class Bat : Enemy
{
    private int _idleCount = 0;
    private bool _isAttack { get; set; } = false;

    private void Start()
    {
        maxHp = 400;
        currentHp = 400;
        speed = 1f;
        damage = 120;

        State = EState.Attack;
    }

    private void Update()
    {
        if (State == EState.Die)
            return;

        if (!_isAttack)
            LookAt(Player.Instance.transform.position);

        transform.Translate(rotateTransform.forward * Time.deltaTime * speed, Space.World);
    }

    private void SetAttackTrue()
    {
        _isAttack = true;

        speed = 5f;
    }

    private void SetStateIdle()
    {
        _idleCount = 2;
        _isAttack = false;

        ChangeState(EState.Idle);

        speed = 1f;
    }

    private void SetStateAttack()
    {
        --_idleCount;

        if (_idleCount == 0)
        {
            ChangeState(EState.Attack);

            speed = -1f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().MinusHp(damage);
        }
    }
}
