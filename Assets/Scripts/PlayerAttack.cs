using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public bool HasTarget { get; private set; } = false;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowTransform;

    private int _targetIndex;
    private float _targetDistance;
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

            for (int i = 0; i < EnemyList.Count; ++i)
            {
                float enemyDistance = Vector3.Distance(EnemyList[i].transform.position, transform.position);

                if (enemyDistance <= _targetDistance)
                {
                    _targetDistance = enemyDistance;
                    _targetIndex = i;
                }
            }

            if (_targetIndex != -1)
            {
                HasTarget = true;

                _rigidbody.rotation = Quaternion.LookRotation(EnemyList[_targetIndex].transform.position - transform.position);
            }
            else
                HasTarget = false;
        }
        else
            HasTarget = false;
    }

    private void ShootAnArrow()
    {
        if (_playerMovement.PlayerState != State.Attack)
            return;

        GameObject arrow = Instantiate(arrowPrefab, arrowTransform.position, Quaternion.identity);

        arrow.transform.rotation = transform.rotation;
    }
}
