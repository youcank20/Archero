using System.Collections;
using TMPro;
using UnityEngine;

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

    private void Start()
    {
        maxHp = 600;
        currentHp = 600;
        speed = 5f;
        damage = 100;

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
