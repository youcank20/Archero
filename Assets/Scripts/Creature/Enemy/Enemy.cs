using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Creature
{
    public Room CurrentRoom { get; set; }

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Image deadEffect;

    protected override void Die()
    {
        gameObject.GetComponent<Collider>().enabled = false;

        HPBackground.SetActive(false);

        ChangeState(EState.Die);

        isDead = true;

        DropCoin();

        CurrentRoom.EnemyList.Remove(gameObject);

        StartCoroutine(DeadEffectCoroutine());
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

    IEnumerator DeadEffectCoroutine()
    {
        while (deadEffect.color.a > 0f)
        {
            if (deadEffect.rectTransform.rect.height < 100f)
            {
                deadEffect.rectTransform.sizeDelta += Vector2.one * Time.deltaTime * 100f;
            }
            else
            {
                deadEffect.color -= new Color(0f, 0f, 0f, Time.deltaTime * 0.5f);

                if (deadEffect.color.a <= 0f)
                    gameObject.SetActive(false);
            }

            deadEffect.rectTransform.anchoredPosition += Vector2.up * Time.deltaTime * 100f;

            yield return null;
        }
    }

    private void SetModelInactive()
    {
        rotateTransform.gameObject.SetActive(false);
    }
}
