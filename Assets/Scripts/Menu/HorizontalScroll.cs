using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HorizontalScroll : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Scrollbar horizontalScrollbar;
    [SerializeField] private Scrollbar[] verticalScrollbars;
    [SerializeField] private Slider buttonSlider;
    [SerializeField] private RectTransform[] buttonRectTransforms;
    [SerializeField] private RectTransform[] imageRectTransforms;
    [SerializeField] private GameObject[] texts;
    [SerializeField] private RectTransform[] lineRectTransforms;

    private float[] _panelPosition = new float[MAX_PANEL_COUNT];
    private float _distance;
    private float _targetValue;
    private int _currentIndex;
    private int _targetIndex;
    private bool _isScrolling = false;

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

        if (_isScrolling)
        {
            StopCoroutine(HorizontalScrollCoroutine());
            EndHorizontalScroll();
        }
        StartCoroutine(HorizontalScrollCoroutine());
    }

    private IEnumerator HorizontalScrollCoroutine()
    {
        _isScrolling = true;

        texts[_currentIndex].SetActive(false);
        texts[_targetIndex].SetActive(true);

        while (Mathf.Abs(horizontalScrollbar.value - _targetValue) > 0.0005f)
        {
            SetHorizontalScrollbarAndButtonSlider(Mathf.Lerp(horizontalScrollbar.value, _targetValue, Time.deltaTime * 10f));

            if (_targetIndex != _currentIndex)
            {
                float accValue = Mathf.Abs((buttonSlider.value - _panelPosition[_currentIndex]) / (_panelPosition[_targetIndex] - _panelPosition[_currentIndex]));

                buttonRectTransforms[_currentIndex].sizeDelta = new Vector2(240f - accValue * 120f, 120f);
                buttonRectTransforms[_targetIndex].sizeDelta = new Vector2(120f + accValue * 120f, 120f);

                imageRectTransforms[_currentIndex].anchoredPosition = new Vector2(0f, 30f - accValue * 30f);
                imageRectTransforms[_targetIndex].anchoredPosition = new Vector2(0f, accValue * 30f);

                imageRectTransforms[_currentIndex].localScale = Vector3.one * (0.6f - accValue * 0.15f);
                imageRectTransforms[_targetIndex].localScale = Vector3.one * (0.45f + accValue * 0.15f);

                if (_currentIndex < MAX_PANEL_COUNT - 1)
                    lineRectTransforms[_currentIndex].anchoredPosition = new Vector2(120f - accValue * 60f, 0f);
                if (_targetIndex < MAX_PANEL_COUNT - 1)
                    lineRectTransforms[_targetIndex].anchoredPosition = new Vector2(60 + accValue * 60f, 0f);
            }

            yield return null;
        }

        EndHorizontalScroll();
    }

    private void EndHorizontalScroll()
    {
        if (_targetIndex != _currentIndex)
            InitializeVerticalScrollbar(_currentIndex);

        SetHorizontalScrollbarAndButtonSlider(_targetValue);

        if (_targetIndex != _currentIndex)
        {
            buttonRectTransforms[_currentIndex].sizeDelta = new Vector2(120f, 120f);
            buttonRectTransforms[_targetIndex].sizeDelta = new Vector2(240f, 120f);

            imageRectTransforms[_currentIndex].anchoredPosition = new Vector2(0f, 0f);
            imageRectTransforms[_targetIndex].anchoredPosition = new Vector2(0f, 30f);

            imageRectTransforms[_currentIndex].localScale = Vector3.one * (0.45f);
            imageRectTransforms[_targetIndex].localScale = Vector3.one * (0.6f);

            if (_currentIndex < MAX_PANEL_COUNT - 1)
                lineRectTransforms[_currentIndex].anchoredPosition = new Vector2(60f, 0f);
            if (_targetIndex < MAX_PANEL_COUNT - 1)
                lineRectTransforms[_targetIndex].anchoredPosition = new Vector2(120f, 0f);
        }

        _currentIndex = _targetIndex;

        _isScrolling = false;
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
        if (_isScrolling)
        {
            StopCoroutine(HorizontalScrollCoroutine());
            EndHorizontalScroll();
        }

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
