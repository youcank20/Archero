using UnityEngine;

public class Enemy : Creature
{
    public State EnemyState { get; protected set; } = State.Idle;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject coinPrefab;

    public void MinusHp(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
            Die();
    }

    protected void Die()
    {
        ChangeState(State.Die);

        DropCoin();
    }

    protected void ChangeState(State state)
    {
        if (EnemyState == state)
            return;

        EnemyState = state;

        animator.SetInteger("State", (int)EnemyState);
    }

    private void DropCoin()
    {
        int coinCount = 5;

        for (int i = 0; i < coinCount; ++i)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

            coin.GetComponent<Coin>().Drop();
        }
    }
}
