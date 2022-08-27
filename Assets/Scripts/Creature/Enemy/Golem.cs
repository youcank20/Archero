using UnityEngine;

public class Golem : Enemy
{
    private int _attackCount = 0;

    private void Start()
    {
        maxHp = 1000;
        currentHp = 1000;
        speed = 2f;
        damage = 120;

        State = EState.Idle;
    }

    private void Update()
    {
        if (State != EState.Move)
            return;

        transform.Translate(rotateTransform.forward * Time.deltaTime * speed, Space.World);
    }

    protected override void SetStateIdle()
    {
        if (State == EState.Attack)
        {
            --_attackCount;

            if (_attackCount != 0)
                return;
        }

        base.SetStateIdle();
    }

    protected override void SetStateMove()
    {
        base.SetStateMove();

        float randomX = Random.Range(-1f, 1f);
        float randomZ = 2f;
        while (randomX * randomX + randomZ * randomZ > 1f)
        {
            randomZ = Random.Range(-1f, 1f);
        }

        LookAt(transform.position + new Vector3(randomX, 0f, randomZ));

        speed = 2f;
    }

    protected override void SetStateHitted()
    {
        if (State != EState.Attack)
            base.SetStateHitted();

        speed = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().MinusHp(damage);
        }
    }

    private void NextState()
    {
        EState randomState = (EState)Random.Range(1, 3);

        if (randomState == EState.Move)
        {
            SetStateMove();
        }
        else if (randomState == EState.Attack)
        {
            _attackCount = 2;
            SetStateAttack();
        }
    }

    private void ShootStone()
    {
        if (State != EState.Attack)
            return;

        LookAt(Player.Instance.transform.position);

        MakeStone();
    }

    private void MakeStone()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject stone = ObjectPoolManager.Instance.Get("Stone", transform, false);

            stone.transform.position = transform.position + Vector3.up * 0.5f;
            stone.GetComponent<Stone>().SetRotate(rotateTransform.rotation, Vector3.up * 45f * (i - 1));
            stone.GetComponent<Stone>().SetDamage(100);
        }
    }
}
