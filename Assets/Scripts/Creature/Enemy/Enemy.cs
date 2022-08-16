using System.Collections;
using UnityEngine;

public class Enemy : Creature
{
    public State EnemyState { get; protected set; } = State.Idle;
    public Room CurrentRoom { get; set; }

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject HPBackground;
    [SerializeField] private RectTransform HPReduceTransform;
    [SerializeField] private RectTransform HPTransform;

    private const float MAX_HP_WIDTH = 92f;

    public void MinusHp(int damage)
    {
        Hp -= damage;

        HPTransform.sizeDelta = new Vector2(MAX_HP_WIDTH * Hp / MaxHp, HPTransform.rect.height);

        StartCoroutine(MinusHPCoroutine());

        if (Hp <= 0)
            Die();
    }

    IEnumerator MinusHPCoroutine()
    {
        while (EnemyState != State.Die && HPReduceTransform.rect.width > HPTransform.rect.width)
        {
            HPReduceTransform.sizeDelta -= new Vector2(Time.deltaTime * 25f, 0f);

            if (HPReduceTransform.rect.width > HPTransform.rect.width)
                yield return null;
            else
            {
                HPReduceTransform.sizeDelta = HPTransform.sizeDelta;

                break;
            }
        }
    }

    protected void Die()
    {
        HPBackground.SetActive(false);

        ChangeState(State.Die);

        DropCoin();

        CurrentRoom.EnemyList.Remove(gameObject);
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
