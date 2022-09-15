using System.Collections;
using UnityEngine;

public enum EState
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    Hitted = 3,
    Die = 4,
}

public class Creature : MonoBehaviour
{
    public EState State { get; protected set; } = EState.Idle;

    protected int maxHp;
    protected int currentHp;
    protected float speed;
    protected int damage;
    protected bool isDead = false;

    [SerializeField] private Animator animator;
    [SerializeField] protected Transform rotateTransform;
    [SerializeField] protected GameObject HPBackground;
    [SerializeField] private RectTransform HPReduceTransform;
    [SerializeField] protected RectTransform HPTransform;

    private const float MAX_HP_WIDTH = 92f;

    protected void ChangeState(EState state)
    {
        if (State == state)
            return;

        State = state;

        animator.SetInteger("State", (int)State);
    }

    protected void LookAt(Vector3 worldPosition)
    {
        rotateTransform.LookAt(worldPosition);
    }

    public virtual void MinusHp(int damage, bool headshot = false)
    {
        if (headshot)
            damage = currentHp;

        currentHp -= damage;

        GameObject damageText = ObjectPoolManager.Instance.Get("Damage", transform.position);
        damageText.GetComponent<Damage>().Initialize(-damage);

        HPTransform.sizeDelta = new Vector2(MAX_HP_WIDTH * currentHp / maxHp, HPTransform.rect.height);

        StartCoroutine(MinusHPCoroutine());

        if (currentHp <= 0)
            Die();
        else
            SetStateHitted();
    }

    private IEnumerator MinusHPCoroutine()
    {
        while (State != EState.Die && HPReduceTransform.rect.width > HPTransform.rect.width)
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

    protected virtual void SetStateIdle()
    {
        ChangeState(EState.Idle);
    }

    protected virtual void SetStateMove()
    {
        ChangeState(EState.Move);
    }

    protected virtual void SetStateAttack()
    {
        ChangeState(EState.Attack);
    }

    protected virtual void SetStateHitted()
    {
        ChangeState(EState.Hitted);
    }

    protected virtual void Die()
    {
    }
}
