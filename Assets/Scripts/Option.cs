using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    private static Option _instance;

    public static Option Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Option>();

            return _instance;
        }
    }

    [SerializeField] private Image panelImage;
    [SerializeField] private GameObject homePopup;

    public void OnClickContinue()
    {
        UICanvas.Instance.SetContentSetActive(Content.Option, false);
        UICanvas.Instance.SetAlpha(0f);

        GameManager.Instance.SetContinue();
    }

    public void OnClickSound()
    {
    }

    public void OnClickMusic()
    {
    }

    public void OnClickMenu()
    {
        StartCoroutine(OpenPopupCoroutine());
    }

    public void OnClickCancel()
    {
        StartCoroutine(ClosePopupCoroutine());
    }

    public void OnClickOK()
    {
        homePopup.SetActive(false);

        GameManager.Instance.LoadMenuScene();
    }

    public void OnClickCoin()
    {
    }

    private IEnumerator OpenPopupCoroutine()
    {
        panelImage.gameObject.SetActive(true);

        StartCoroutine(FadeOutCoroutine(0.5f, 2f));

        if (panelImage.color.a < 0.5f)
            yield return null;

        homePopup.SetActive(true);
    }

    private IEnumerator ClosePopupCoroutine()
    {
        homePopup.SetActive(false);

        StartCoroutine(FadeInCoroutine(4f));

        if (panelImage.color.a > 0f)
            yield return null;

        panelImage.gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine(float alpha, float speed = 1f)
    {
        while (panelImage.color.a < alpha)
        {
            panelImage.color += new Color(0f, 0f, 0f, Time.unscaledDeltaTime * speed);

            if (panelImage.color.a > alpha)
                SetAlpha(alpha);

            yield return null;
        }
    }

    private IEnumerator FadeInCoroutine(float speed = 1f)
    {
        while (panelImage.color.a > 0f)
        {
            panelImage.color -= new Color(0f, 0f, 0f, Time.unscaledDeltaTime * speed);

            if (panelImage.color.a < 0f)
                SetAlpha(0f);

            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
    }

    public float GetPanelAlpha()
    {
        return panelImage.color.a;
    }
}
