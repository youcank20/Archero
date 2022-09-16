using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private List<GameObject> _enemyList = new List<GameObject>();
    private float _speed = 15f;
    private int _enemyBounceCount;
    private int _wallBounceCount;
    private int _targetIndex;

    private void OnEnable()
    {
        _enemyBounceCount = 2;
        _wallBounceCount = 2;
    }

    private void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * _speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (PlayerSkill.Instance.playerAbilities[(int)Ability.Ricochet] != 0)
            {
                if (_enemyBounceCount > 0)
                    ChangeDirection(other.gameObject);
            }

            if (PlayerSkill.Instance.playerAbilities[(int)Ability.Headshot] != 0)
            {
                if (Random.Range(0, 8) < PlayerSkill.Instance.playerAbilities[8])
                {
                    other.GetComponent<Enemy>().MinusHp(0, true);

                    ObjectPoolManager.Instance.Release(gameObject);

                    return;
                }
            }

            other.GetComponent<Enemy>().MinusHp(Player.Instance.Damage);

            if (PlayerSkill.Instance.playerAbilities[(int)Ability.PiercingShot] == 0)
                ObjectPoolManager.Instance.Release(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            if (PlayerSkill.Instance.playerAbilities[(int)Ability.BouncyWall] != 0 && _wallBounceCount > 0)
            {
                --_wallBounceCount;

                transform.forward = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            }
            else
                ObjectPoolManager.Instance.Release(gameObject);
        }
    }

    private bool ChangeDirection(GameObject colliderObject)
    {
        --_enemyBounceCount;

        List<GameObject> enemyList = Player.Instance.GetComponent<PlayerAttack>().EnemyList;

        if (enemyList.Count != 0)
        {
            if (_targetIndex >= enemyList.Count || colliderObject != enemyList[_targetIndex])
            {
                for (int i = 0; i < enemyList.Count; ++i)
                {
                    if (colliderObject == enemyList[i])
                        _targetIndex = i;
                }
            }

            int targetIndex = -1;
            float maxDistance = 10f;
            float targetDistance = maxDistance;

            for (int i = 0; i < enemyList.Count; ++i)
            {
                if (i == _targetIndex)
                    continue;

                float enemyDistance = Vector3.Distance(enemyList[i].transform.position, transform.position);

                if (enemyDistance <= maxDistance)
                {
                    RaycastHit hit;
                    bool isHit = Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), enemyList[i].transform.position - transform.position, out hit, maxDistance);

                    if (isHit && hit.collider.CompareTag("Enemy"))
                    {
                        if (enemyDistance <= targetDistance)
                        {
                            targetDistance = enemyDistance;
                            targetIndex = i;
                        }
                    }
                }
            }

            if (targetIndex != -1)
            {
                _targetIndex = targetIndex;

                transform.LookAt(enemyList[_targetIndex].transform);

                return true;
            }
        }

        return false;
    }

    public void SetEnemyListAndTargetIndex(List<GameObject> enemyList, int targetIndex)
    {
        _enemyList = enemyList;
        _targetIndex = targetIndex;
    }
}
