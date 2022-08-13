using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnTableSet : MonoBehaviour
{
    [SerializeField] private Image canvasPanelImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetPause();

            StartCoroutine(FadeOut(0.5f));
        }
    }

    IEnumerator FadeOut(float alpha)
    {
        while (canvasPanelImage.color.a < alpha)
        {
            canvasPanelImage.color += new Color(0f, 0f, 0f, Time.unscaledDeltaTime);

            if (canvasPanelImage.color.a > alpha)
                canvasPanelImage.color = new Color(canvasPanelImage.color.r, canvasPanelImage.color.g, canvasPanelImage.color.b, alpha);

            yield return null;
        }
    }
}
