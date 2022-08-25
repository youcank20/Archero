using UnityEngine;

public class Ghost : Enemy
{
    private void Start()
    {
        maxHp = 400;
        currentHp = 400;
        speed = 1f;
        damage = 120;

        State = EState.Idle;
    }

    private void Update()
    {
        if (State != EState.Attack)
            return;

        transform.Translate(rotateTransform.forward * Time.deltaTime * speed, Space.World);
    }

    private void SetStateIdle()
    {
        ChangeState(EState.Idle);

        speed = 1f;
    }

    private void SetStateAttack()
    {
        ChangeState(EState.Attack);

        speed = Random.Range(5f, 10f);

        float randomX = Random.Range(-1f, 1f);
        float randomZ = 2f;
        while (randomX * randomX + randomZ * randomZ > 1f)
        {
            randomZ = Random.Range(-1f, 1f);
        }

        LookAt(transform.position + new Vector3(randomX, 0f, randomZ));
    }

    private void SetSpeedZero()
    {
        speed = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().MinusHp(damage);
        }
        else
            SetSpeedZero();
    }
}
