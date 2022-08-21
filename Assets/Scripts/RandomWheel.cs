using System.Collections;
using UnityEngine;

public class RandomWheel : MonoBehaviour
{
    public void Turn()
    {
        StartCoroutine(TurnCoroutine());
    }

    private IEnumerator TurnCoroutine()
    {
        float zAngle = -30f + UnityEngine.Random.Range(1f, 6f);

        while (zAngle < -0.01f)
        {
            zAngle = Mathf.Lerp(zAngle, 0f, Time.unscaledDeltaTime);
            transform.Rotate(0f, 0f, zAngle);

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        UICanvas.Instance.SetContentSetActive(Content.RandomWheel, false);
        UICanvas.Instance.SetAlpha(0f);

        GameManager.Instance.SetResume();
    }
}
