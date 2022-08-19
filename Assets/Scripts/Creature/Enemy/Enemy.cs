using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Creature
{
    public Room CurrentRoom { get; set; }

    [SerializeField] private GameObject coinPrefab;
    //[SerializeField] private Image deadEffect;

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
            GameObject coin = ObjectPoolManager.Instance.Get("Coin", transform.position);

            coin.GetComponent<Coin>().Drop();
        }
    }

    IEnumerator DeadEffectCoroutine()
    {
        GameObject deadEffectObject = ObjectPoolManager.Instance.Get("DeadEffect");
        deadEffectObject.transform.SetParent(HPBackground.transform.parent, false);

        Image deadEffect = deadEffectObject.GetComponent<Image>();
        InitializeDeadEffectImage(deadEffect);

        while (deadEffect.color.a > 0f)
        {
            deadEffect.rectTransform.anchoredPosition += Vector2.up * Time.deltaTime * 100f;

            if (deadEffect.rectTransform.rect.height < 100f)
            {
                deadEffect.rectTransform.sizeDelta += Vector2.one * Time.deltaTime * 100f;
            }
            else
            {
                deadEffect.color -= new Color(0f, 0f, 0f, Time.deltaTime * 0.5f);

                if (deadEffect.color.a <= 0f)
                {
                    ObjectPoolManager.Instance.Release(deadEffectObject);
                    gameObject.SetActive(false);
                }
            }

            yield return null;
        }
    }

    private void InitializeDeadEffectImage(Image image)
    {
        image.rectTransform.anchoredPosition3D = new Vector3(0f, -50f, 0f);
        image.rectTransform.sizeDelta = Vector2.zero;
        image.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void SetModelInactive()
    {
        rotateTransform.gameObject.SetActive(false);
    }
}
