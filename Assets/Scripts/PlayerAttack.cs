using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public bool HasTarget => _hasTarget;

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowTransform;
    [SerializeField] private PlayerSkill playerSkill;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private RectTransform experienceTransform;
    [SerializeField] private TextMeshProUGUI levelUpText;

    private int _targetIndex = -1;
    private float _targetDistance;
    private int _closestIndex;
    private float _closestDistance;
    private bool _hasTarget = false;
    private Rigidbody _rigidbody;
    private PlayerMovement _playerMovement;
    private int _coins = 0;
    private int _level = 1;
    private int _experiencePoint = 0;
    private int _maxExperiencePoint = 12;

    private const float MAX_DISTANCE = 10f;
    private const float MAX_EXPERIENCE_WIDTH = 490f;

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
                UIManager.Instance.Get("TargetMarker").GetComponent<TargetMarker>().SetTarget(EnemyList[_targetIndex]);

                if (Player.Instance.State == EState.Move)
                    return;

                _hasTarget = true;

                Player.Instance.LookAt(EnemyList[_targetIndex].transform.position);
            }
        }
        else
            _hasTarget = false;

        if (!_hasTarget)
            UIManager.Instance.Release("TargetMarker");
    }

    private void ShootArrow()
    {
        if (Player.Instance.State != EState.Attack)
            return;

        MakeArrow(playerSkill.playerAbilities[2] + 1);

        if (playerSkill.playerAbilities[0] != 0)
        {
            StartCoroutine(Multishot(arrowTransform.position, Player.Instance.GetRotation()));
        }
    }

    private void MakeArrow(int count = 1)
    {
        for (int i = 0; i < count; ++i)
        {
            GameObject arrow = ObjectPoolManager.Instance.Get("Arrow", transform, false);

            arrow.transform.position = arrowTransform.position + arrow.transform.right * 0.2f * (1 - count + i * 2);
            arrow.transform.rotation = Player.Instance.GetRotation();
        }
    }

    private IEnumerator Multishot(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(0.2f);

        GameObject arrow = ObjectPoolManager.Instance.Get("Arrow", transform, false);
        arrow.transform.SetPositionAndRotation(position, rotation);
    }

    public void GetCoin()
    {
        ++_coins;
        coinText.text = _coins.ToString();

        ++_experiencePoint;
        experienceTransform.sizeDelta = new Vector2(MAX_EXPERIENCE_WIDTH * _experiencePoint / _maxExperiencePoint, experienceTransform.rect.height);

        if (_experiencePoint >= _maxExperiencePoint)
        {
            ++_level;
            levelText.text = "Lv." + _level.ToString();
            StartCoroutine(LevelUpCoroutine());

            _experiencePoint = 0;
            experienceTransform.sizeDelta = new Vector2(0f, experienceTransform.rect.height);
        }
    }

    private IEnumerator LevelUpCoroutine()
    {
        while (levelUpText.rectTransform.anchoredPosition.y < 100f)
        {
            if (levelUpText.rectTransform.anchoredPosition.y < -50f && levelUpText.color.a < 1f)
                levelUpText.color += new Color(0f, 0f, 0f, Time.deltaTime * 5f);

            levelUpText.rectTransform.anchoredPosition += new Vector2(0f, Time.deltaTime * 200f);

            if (levelUpText.rectTransform.anchoredPosition.y >= 50f && levelUpText.color.a > 0f)
                levelUpText.color -= new Color(0f, 0f, 0f, Time.deltaTime * 5f);

            yield return null;
        }

        levelUpText.rectTransform.anchoredPosition = new Vector2(0f, -100f);
    }
}
