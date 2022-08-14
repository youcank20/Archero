using UnityEngine;

public class Joystick : MonoBehaviour
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

    public Vector3 NormalizedDirection { get; private set; }

    [SerializeField] private Transform background;
    [SerializeField] private Transform touch;
    [SerializeField] private Transform center;

    private Vector3 _backgroundOriginalPosition;
    private float _maxDistance;
    private Vector3 _pointerDownPosition;

    private void Start()
    {
        _backgroundOriginalPosition = background.position;
        _maxDistance = background.GetComponent<RectTransform>().rect.height * 0.3f;
    }

    public void Drag()
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

    public void PointerDown()
    {
        background.position = Input.mousePosition;
        _pointerDownPosition = Input.mousePosition;
        center.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
    }

    public void PointerUp()
    {
        background.position = _backgroundOriginalPosition;
        touch.position = _backgroundOriginalPosition;
        center.rotation = Quaternion.Euler(Vector3.zero);

        NormalizedDirection = Vector3.zero;
    }
}
