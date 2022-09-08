using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour, IPointerDownHandler
{
    private Image _panelImage;

    private void Start()
    {
        _panelImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        StartCoroutine(FadeOutCoroutine(1f, 1f));

        if (_panelImage.color.a < 1f)
            yield return null;

        GameManager.Instance.LoadMenuScene(false);
    }

    private IEnumerator FadeOutCoroutine(float alpha, float speed = 1f)
    {
        while (_panelImage.color.a < alpha)
        {
            _panelImage.color += new Color(0f, 0f, 0f, Time.unscaledDeltaTime * speed);

            if (_panelImage.color.a > alpha)
                SetAlpha(alpha);

            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        _panelImage.color = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, alpha);
    }
}
