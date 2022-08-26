using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private Transform rotateTransform;
    [SerializeField] private Transform imageTransform;

    private float _speed = 5f;
    private int _damage;

    private void Update()
    {
        transform.Translate(rotateTransform.forward * Time.deltaTime * _speed, Space.World);

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

    public void SetRotate(Quaternion rotation, Vector3 eulers)
    {
        rotateTransform.rotation = rotation;
        rotateTransform.Rotate(eulers);
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
