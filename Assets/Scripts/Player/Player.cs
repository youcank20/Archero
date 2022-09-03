using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Creature
{
    private static Player _instance;

    public static Player Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<Player>();

            return _instance;
        }
    }

    public int Damage => damage;

    [SerializeField] private TextMeshProUGUI HpText;

    private List<GameObject> _HPLineList = new List<GameObject>();

    private void Start()
    {
        maxHp = 1150;
        currentHp = 1150;
        speed = 5f;
        damage = 100;

        for (int i = 1; i < maxHp / 200f; ++i)
        {
            GameObject newHPLine = ObjectPoolManager.Instance.Get("HPLine", HPBackground.transform, false);

            newHPLine.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(4f + (92f * 200f * i) / maxHp, 0f);

            _HPLineList.Add(newHPLine);
        }

        transform.position = Vector3.up * 25f;

        RefreshHpText();
    }

    public IEnumerator AppearCoroutine()
    {
        while (transform.position.y > 0f)
        {
            transform.position -= Vector3.up * Time.unscaledDeltaTime * 25f;

            if (transform.position.y < 0f)
                transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

            yield return null;
        }
    }

    public new void ChangeState(EState state)
    {
        base.ChangeState(state);
    }

    public new void LookAt(Vector3 worldPosition)
    {
        base.LookAt(worldPosition);
    }

    public Quaternion GetRotation()
    {
        return rotateTransform.rotation;
    }

    public override void MinusHp(int damage)
    {
        base.MinusHp(damage);

        for (int i = 0; i < _HPLineList.Count; ++i)
        {
            if (_HPLineList[i].GetComponent<RectTransform>().anchoredPosition.x < HPTransform.anchoredPosition.x + HPTransform.rect.width)
                _HPLineList[i].SetActive(true);
            else
                _HPLineList[i].SetActive(false);
        }

        RefreshHpText();
    }

    private void RefreshHpText()
    {
        HpText.text = currentHp.ToString();
    }

    public bool HasTarget()
    {
        return GetComponent<PlayerAttack>().HasTarget;
    }
}
