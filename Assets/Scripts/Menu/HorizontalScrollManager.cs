using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private static HorizontalScrollManager _instance;

    public static HorizontalScrollManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<HorizontalScrollManager>();

            return _instance;
        }
    }

    [SerializeField] private Scrollbar horizontalScrollbar;

    private float[] _panelPosition = new float[MAX_PANEL_COUNT];
    private float _distance;
    private float _targetValue;

    private const int MAX_PANEL_COUNT = 5;

    private void Start()
    {
        _distance = 1f / (2 * MAX_PANEL_COUNT + 6);

        for (int i = 0; i < MAX_PANEL_COUNT; ++i)
        {
            _panelPosition[i] = (2f + 3f * i) * _distance;
        }

        horizontalScrollbar.value = _panelPosition[2];
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < MAX_PANEL_COUNT; ++i)
        {
            if (horizontalScrollbar.value <= _panelPosition[0] + _distance * 1.5f)
                _targetValue = _panelPosition[0];
            else if (horizontalScrollbar.value > _panelPosition[MAX_PANEL_COUNT - 1] - _distance * 1.5f)
                _targetValue = _panelPosition[MAX_PANEL_COUNT - 1];
            else if (horizontalScrollbar.value > _panelPosition[i] - _distance * 1.5f && horizontalScrollbar.value <= _panelPosition[i] + _distance * 1.5f)
                _targetValue = _panelPosition[i];
        }

        StartCoroutine(HorizontalScrollCoroutine());
    }

    IEnumerator HorizontalScrollCoroutine()
    {
        while (Mathf.Abs(horizontalScrollbar.value - _targetValue) > 0.0005f)
        {
            horizontalScrollbar.value = Mathf.Lerp(horizontalScrollbar.value, _targetValue, Time.deltaTime * 10f);

            yield return null;
        }

        horizontalScrollbar.value = _targetValue;
    }
}
