using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dust : MonoBehaviour
{
    public bool IsReleased { get; private set; }

    [SerializeField] private Image image;
    [SerializeField] private Transform direction;

    private RectTransform _imageTransform;

    private void OnEnable()
    {
        if (_imageTransform == null)
            _imageTransform = image.GetComponent<RectTransform>();

        float zAngle = Random.Range(0f, 360f);
        _imageTransform.Rotate(0f, 0f, zAngle);

        transform.localScale = Vector3.one;

        IsReleased = false;
    }

    public IEnumerator DropDustCoroutine(Vector3 startPosition)
    {
        transform.position = startPosition;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        float yAngle = Random.Range(0f, 360f);
        direction.Rotate(0f, yAngle, 0f);

        while (Vector3.Distance(startPosition, transform.position) < 2.5f)
        {
            transform.Translate(direction.forward * Time.unscaledDeltaTime * 5f);

            yield return null;
        }

        while (image.color.a > 0f)
        {
            image.color -= new Color(0f, 0f, 0f, Time.unscaledDeltaTime * 2.5f);
            transform.localScale -= Vector3.one * Time.unscaledDeltaTime * 2f;

            yield return null;
        }

        ObjectPoolManager.Instance.Release(gameObject);
        IsReleased = true;
    }

    public IEnumerator MoveDustCoroutine(Vector3 position)
    {
        transform.position = position;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

        while (image.color.a > 0f)
        {
            image.color -= new Color(0f, 0f, 0f, Time.deltaTime * 2f);
            transform.localScale -= Vector3.one * Time.deltaTime;

            yield return null;
        }

        ObjectPoolManager.Instance.Release(gameObject);
        IsReleased = true;
    }
}
