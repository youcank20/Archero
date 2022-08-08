using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public bool HasTarget => _hasTarget;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowTransform;

    private int _targetIndex;
    private float _targetDistance;
    private int _closestIndex;
    private float _closestDistance;
    private bool _hasTarget = false;
    private Rigidbody _rigidbody;
    private PlayerMovement _playerMovement;

    private const float MAX_DISTANCE = 10f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (EnemyList.Count != 0)
        {
            _targetIndex = -1;
            _targetDistance = MAX_DISTANCE;
            _closestIndex = -1;
            _closestDistance = MAX_DISTANCE;

            for (int i = 0; i < EnemyList.Count; ++i)
            {
                float enemyDistance = Vector3.Distance(EnemyList[i].transform.position, transform.position);

                if (enemyDistance <= MAX_DISTANCE)
                {
                    RaycastHit hit;
                    bool isHit = Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), EnemyList[i].transform.position - transform.position, out hit, MAX_DISTANCE);

                    if (isHit && hit.collider.CompareTag("Enemy"))
                    {
                        if (enemyDistance <= _targetDistance)
                        {
                            _targetDistance = enemyDistance;
                            _targetIndex = i;
                        }
                    }
                    else
                    {
                        if (enemyDistance <= _closestDistance)
                        {
                            _closestDistance = enemyDistance;
                            _closestIndex = i;
                        }
                    }
                }
            }

            if (_targetIndex == -1)
            {
                if (_closestIndex == -1)
                    _hasTarget = false;
                else
                    _targetIndex = _closestIndex;
            }

            if (_targetIndex != -1)
            {
                _hasTarget = true;

                _rigidbody.rotation = Quaternion.LookRotation(EnemyList[_targetIndex].transform.position - transform.position);
            }
        }
        else
            _hasTarget = false;
    }

    private void ShootAnArrow()
    {
        if (_playerMovement.PlayerState != State.Attack)
            return;

        GameObject arrow = Instantiate(arrowPrefab, arrowTransform.position, Quaternion.identity);

        arrow.transform.rotation = transform.rotation;
    }
}
