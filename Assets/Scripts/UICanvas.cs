using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Content
{
    Pause = 0,
    RandomWheel = 1,
    SkillSlotMachine = 2,
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

    public IEnumerator FadeOut(Content content, bool value)
    {
        while (panelImage.color.a < 0.5f)
        {
            panelImage.color += new Color(0f, 0f, 0f, Time.unscaledDeltaTime);

            if (panelImage.color.a > 0.5f)
                SetAlpha(0.5f);

            yield return null;
        }

        SetContentSetActive(content, value);
    }

    public IEnumerator FadeIn()
    {
        while (panelImage.color.a > 0f)
        {
            panelImage.color -= new Color(0f, 0f, 0f, Time.unscaledDeltaTime);

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
            case Content.RandomWheel:
                randomWheelUISet.SetActive(value);
                break;
            case Content.SkillSlotMachine:
                skillSlotMachineUISet.SetActive(value);
                break;
        }
    }
}
