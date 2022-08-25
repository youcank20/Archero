using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Content
{
    Pause = 0,
    NextStage = 1,
    RandomWheel = 2,
    SkillSlotMachine = 3,
}

public class UICanvas : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private static UICanvas _instance;

    public static UICanvas Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UICanvas>();

            return _instance;
        }
    }

    [SerializeField] private Joystick joystick;
    [SerializeField] private Image panelImage;
    [SerializeField] private GameObject randomWheelUISet;
    [SerializeField] private GameObject skillSlotMachineUISet;

    private void Start()
    {
        panelImage.color = Color.black;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!GameManager.Instance.IsPaused)
            joystick.Drag();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!GameManager.Instance.IsPaused)
            joystick.PointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!GameManager.Instance.IsPaused)
            joystick.PointerUp();
    }

    public IEnumerator FadeOutCoroutine(Content content, float alpha, bool scaled, float speed = 1f, bool value = true)
    {
        while (panelImage.color.a < alpha)
        {
            if (scaled)
                panelImage.color += new Color(0f, 0f, 0f, Time.deltaTime * speed);
            else
                panelImage.color += new Color(0f, 0f, 0f, Time.unscaledDeltaTime * speed);

            if (panelImage.color.a > alpha)
                SetAlpha(alpha);

            yield return null;
        }

        SetContentSetActive(content, value);
    }

    public IEnumerator FadeInCoroutine(bool scaled, float speed = 1f)
    {
        while (panelImage.color.a > 0f)
        {
            if (scaled)
                panelImage.color -= new Color(0f, 0f, 0f, Time.deltaTime * speed);
            else
                panelImage.color -= new Color(0f, 0f, 0f, Time.unscaledDeltaTime * speed);

            if (panelImage.color.a < 0f)
                SetAlpha(0f);

            yield return null;
        }
    }

    public void SetAlpha(float alpha)
    {
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
    }

    public void SetContentSetActive(Content content, bool value)
    {
        switch (content)
        {
            case Content.Pause:
                break;
            case Content.NextStage:
                break;
            case Content.RandomWheel:
                randomWheelUISet.SetActive(value);
                break;
            case Content.SkillSlotMachine:
                skillSlotMachineUISet.SetActive(value);
                break;
        }
    }

    public float GetPanelAlpha()
    {
        return panelImage.color.a;
    }
}
