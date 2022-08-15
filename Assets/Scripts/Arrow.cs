using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _speed = 15f;
    private int _damage = 5;

    private void Update()
    {
        transform.Translate(transform.forward * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (PlayerSkill.Instance.playerAbilities[3] == 0)
                gameObject.SetActive(false);

            other.GetComponent<Enemy>().MinusHp(_damage);
        }
    }
}
