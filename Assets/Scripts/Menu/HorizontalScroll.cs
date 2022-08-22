using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Scrollbar horizontalScrollbar;
    [SerializeField] private Scrollbar[] verticalScrollbars = new Scrollbar[3];
    [SerializeField] private Slider buttonSlider;
    [SerializeField] private RectTransform[] buttonRectTransforms = new RectTransform[5];

    private float[] _panelPosition = new float[MAX_PANEL_COUNT];
    private float _distance;
    private float _targetValue;
    private int _currentIndex;
    private int _targetIndex;

    private const int MAX_PANEL_COUNT = 5;

    private void Start()
    {
        _distance = 1f / (2 * MAX_PANEL_COUNT + 6);

        for (int i = 0; i < MAX_PANEL_COUNT; ++i)
        {
            _panelPosition[i] = (2f + 3f * i) * _distance;
        }

        _currentIndex = 2;

        SetHorizontalScrollbarAndButtonSlider(_panelPosition[_currentIndex]);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        buttonSlider.value = horizontalScrollbar.value;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (horizontalScrollbar.value <= _panelPosition[0] + _distance * 1.5f)
            _targetIndex = 0;
        else if (horizontalScrollbar.value > _panelPosition[MAX_PANEL_COUNT - 1] - _distance * 1.5f)
            _targetIndex = MAX_PANEL_COUNT - 1;
        else
        {
            _targetIndex = _currentIndex;

            for (int i = 0; i < MAX_PANEL_COUNT; ++i)
            {
                if (horizontalScrollbar.value > _panelPosition[i] - _distance * 1.5f &&
                    horizontalScrollbar.value <= _panelPosition[i] + _distance * 1.5f)
                    _targetIndex = i;
            }
        }

        if (_targetIndex == _currentIndex)
        {
            if (eventData.delta.x < -10 && _targetIndex < MAX_PANEL_COUNT - 1)
                ++_targetIndex;
            else if (eventData.delta.x > 10 && _targetIndex > 0)
                --_targetIndex;
        }

        _targetValue = _panelPosition[_targetIndex];

        StartCoroutine(HorizontalScrollCoroutine());
    }

    private IEnumerator HorizontalScrollCoroutine()
    {
        while (Mathf.Abs(horizontalScrollbar.value - _targetValue) > 0.0005f)
        {
            SetHorizontalScrollbarAndButtonSlider(Mathf.Lerp(horizontalScrollbar.value, _targetValue, Time.deltaTime * 10f));

            yield return null;
        }

        if (_targetIndex != _currentIndex)
            InitializeVerticalScrollbar(_currentIndex);

        SetHorizontalScrollbarAndButtonSlider(_targetValue);

        buttonRectTransforms[_currentIndex].sizeDelta = new Vector2(120f, 120f);
        buttonRectTransforms[_targetIndex].sizeDelta = new Vector2(240f, 120f);
        _currentIndex = _targetIndex;
    }

    private void InitializeVerticalScrollbar(int currentIndex)
    {
        if (currentIndex < 2)
            verticalScrollbars[currentIndex].value = 1f;
        else if (currentIndex == MAX_PANEL_COUNT - 1)
            verticalScrollbars[2].value = 1f;
    }

    public void OnClickButton(int index)
    {
        _targetIndex = index;
        _targetValue = _panelPosition[_targetIndex];

        StartCoroutine(HorizontalScrollCoroutine());
    }

    private void SetHorizontalScrollbarAndButtonSlider(float value)
    {
        horizontalScrollbar.value = value;
        buttonSlider.value = value;
    }
}
