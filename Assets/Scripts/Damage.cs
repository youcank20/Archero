using TMPro;
using UnityEngine;

public enum FontSize
{
    Enlarging = 0,
    Reducing = 1,
    Fixed = 2,
    Transparent = 3,
}

public class Damage : MonoBehaviour
{
    [SerializeField] private RectTransform posTransform;
    [SerializeField] private TextMeshProUGUI text;

    private int _posX;
    private FontSize _fontSize;
    private float _fixedTime;

    public void Initialize(int damage)
    {
        posTransform.anchoredPosition = new Vector2(0f, 2f);
        transform.localScale = Vector3.zero;
        text.text = damage.ToString();
        text.color = new Color(1f, 1f, 1f, 0f);

        _posX = Random.Range(-1, 2);
        _fontSize = FontSize.Enlarging;
        _fixedTime = 0.3f;
    }

    private void Update()
    {
        if (_fontSize == FontSize.Enlarging)
        {
            posTransform.anchoredPosition += new Vector2(1f * _posX, 2f) * Time.deltaTime * 4f;
            transform.localScale += Vector3.one * Time.deltaTime * 5f;
            text.color += new Color(0f, 0f, 0f, Time.deltaTime * 4f);

            if (transform.localScale.x >= 1.25f)
            {
                transform.localScale = Vector3.one * 1.5f;
                _fontSize = FontSize.Reducing;
            }
        }
        else if (_fontSize == FontSize.Reducing)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * 20f;

            if (transform.localScale.x <= 1f)
            {
                transform.localScale = Vector3.one;
                _fontSize = FontSize.Fixed;
            }
        }
        else if (_fontSize == FontSize.Fixed)
        {
            _fixedTime -= Time.deltaTime;

            if (_fixedTime <= 0f)
            {
                _fontSize = FontSize.Transparent;
            }
        }
        else if (_fontSize == FontSize.Transparent)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * 5f;
            text.color -= new Color(0f, 0f, 0f, Time.deltaTime * 5f);

            if (transform.localScale.x <= 0f)
            {
                ObjectPoolManager.Instance.Release(gameObject);
            }
        }
    }
}
