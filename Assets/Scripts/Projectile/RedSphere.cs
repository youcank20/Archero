using UnityEngine;

public class RedSphere : MonoBehaviour
{
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private Transform imageTransform;

    private float _speed = 5f;
    private int _damage;
    private Vector3 _startPoint;
    private Vector3 _endPoint;
    private float _accumulatedTime;

    private void Update()
    {
        _accumulatedTime += Time.deltaTime;

        if (_accumulatedTime <= 1f)
        {
            transform.position = FlyParabola(_startPoint, _endPoint, 4f, _accumulatedTime, 1f);
        }
        else
        {
            transform.position = _endPoint;

            ObjectPoolManager.Instance.Release(gameObject);
        }

        transform.Translate(rotateTransform.forward * Time.deltaTime * _speed, Space.World); // 현재 직선/곡선으로 바꿔야 함

        imageTransform.Rotate(Vector3.back * Time.deltaTime * 360f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Projectile"))
            return;

        if (other.CompareTag("Player"))
            other.GetComponent<Player>().MinusHp(_damage);

        ObjectPoolManager.Instance.Release(gameObject);
    }

    public void SetRotate(Quaternion rotation)
    {
        rotateTransform.rotation = rotation;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void Initialize(Vector3 startPoint)
    {
        _startPoint = startPoint;
        _endPoint = Player.Instance.transform.position;
        _accumulatedTime = 0f;
    }

    private Vector3 FlyParabola(Vector3 startPoint, Vector3 endPoint, float height, float accumulatedTime, float endTime)
    {
        float timeRatio = accumulatedTime / endTime;
        float yPosition = 4f * timeRatio * (1f - timeRatio) * height;

        return Vector3.Lerp(startPoint, endPoint, timeRatio) + Vector3.up * yPosition;
    }
}
