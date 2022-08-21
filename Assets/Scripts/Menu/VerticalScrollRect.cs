using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VerticalScrollRect : ScrollRect
{
    private HorizontalScroll _horizontalScroll;
    private ScrollRect _horizontalScrollRect;
    private bool _isXLonger;

    protected override void Start()
    {
        _horizontalScroll = GameObject.FindWithTag("HorizontalScroll").GetComponent<HorizontalScroll>();
        _horizontalScrollRect = _horizontalScroll.GetComponent<ScrollRect>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        _isXLonger = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y);

        if (_isXLonger)
        {
            _horizontalScroll.OnBeginDrag(eventData);
            _horizontalScrollRect.OnBeginDrag(eventData);
        }
        else
            base.OnBeginDrag(eventData);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (_isXLonger)
        {
            _horizontalScroll.OnDrag(eventData);
            _horizontalScrollRect.OnDrag(eventData);
        }
        else
            base.OnDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        if (_isXLonger)
        {
            _horizontalScroll.OnEndDrag(eventData);
            _horizontalScrollRect.OnEndDrag(eventData);
        }
        else
            base.OnEndDrag(eventData);
    }
}
