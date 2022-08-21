using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Creature
{
    public Room CurrentRoom { get; set; }

    [SerializeField] private GameObject coinPrefab;

    protected override void Die()
    {
        gameObject.GetComponent<Collider>().enabled = false;

        HPBackground.SetActive(false);

        ChangeState(EState.Die);

        isDead = true;

        InvokeRepeating("DropCoin", 0f, 0.1f);
        Invoke("CancelDropCoin", 0.5f);

        CurrentRoom.EnemyList.Remove(gameObject);

        StartCoroutine(DeadEffectCoroutine());
    }

    private void DropCoin()
    {
        GameObject coin = ObjectPoolManager.Instance.Get("Coin", transform.position);

        coin.GetComponent<Coin>().Drop();
    }

    private void CancelDropCoin()
    {
        CancelInvoke("DropCoin");
    }

    private IEnumerator DeadEffectCoroutine()
    {
        GameObject deadEffectObject = ObjectPoolManager.Instance.Get("DeadEffect", HPBackground.transform.parent, false);

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
        image.rectTransform.localRotation = Quaternion.Euler(Vector3.zero);
        image.rectTransform.localScale = Vector3.one;
        image.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void SetModelInactive()
    {
        rotateTransform.gameObject.SetActive(false);
    }
}
