using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _speed = 15f;
    private int _bounceCount;

    private void OnEnable()
    {
        _bounceCount = 3;
    }

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * _speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (PlayerSkill.Instance.playerAbilities[3] == 0)
                ObjectPoolManager.Instance.Release(gameObject);

            if (PlayerSkill.Instance.playerAbilities[8] != 0)
            {
                if (Random.Range(0, 8) < PlayerSkill.Instance.playerAbilities[8])
                {
                    other.GetComponent<Enemy>().MinusHp(0, true);

                    return;
                }
            }

            other.GetComponent<Enemy>().MinusHp(Player.Instance.Damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            if (PlayerSkill.Instance.playerAbilities[4] != 0 && _bounceCount > 0)
            {
                --_bounceCount;
            }
            else
            {
                ObjectPoolManager.Instance.Release(gameObject);
            }
        }
    }
}
