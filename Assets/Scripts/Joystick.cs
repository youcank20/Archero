using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private static Joystick _instance;

    public static Joystick Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Joystick>();

            return _instance;
        }
    }

    public Vector3 NormalizedDirection;

    [SerializeField] private Transform background;
    [SerializeField] private Transform touch;
    [SerializeField] private Transform center;

    private Vector3 _backgroundOriginalPosition;
    private Vector3 _pointerDownPosition;
    private float _maxDistance;

    private void Start()
    {
        _backgroundOriginalPosition = background.position;
        _maxDistance = background.GetComponent<RectTransform>().rect.height * 0.3f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        background.position = Input.mousePosition;
        _pointerDownPosition = Input.mousePosition;
        center.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        background.position = _backgroundOriginalPosition;
        touch.position = _backgroundOriginalPosition;
        center.rotation = Quaternion.Euler(Vector3.zero);

        NormalizedDirection = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        NormalizedDirection = (Input.mousePosition - _pointerDownPosition).normalized;

        float pointerDistance = Vector3.Distance(Input.mousePosition, _pointerDownPosition);

        if (pointerDistance > _maxDistance)
            touch.position = _pointerDownPosition + NormalizedDirection * _maxDistance;
        else
            touch.position = Input.mousePosition;

        float directionAngle = Mathf.Atan2(NormalizedDirection.y, NormalizedDirection.x) * Mathf.Rad2Deg;
        center.rotation = Quaternion.Euler(new Vector3(0f, 0f, directionAngle - 90f));
    }
}
