using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _speed = 15f;

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * _speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (PlayerSkill.Instance.playerAbilities[3] == 0)
                gameObject.SetActive(false);

            other.GetComponent<Enemy>().MinusHp(Player.Instance.Damage);
        }
    }
}
