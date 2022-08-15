using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void Drop()
    {
        StartCoroutine(DropCoroutine());
    }

    IEnumerator DropCoroutine()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = transform.position + new Vector3(Random.value, 0f, Random.value);
        float height = 3f;
        float accumulatedTime = 0f;
        float endTime = 1f;

        while (true)
        {
            accumulatedTime += Time.deltaTime;

            if (accumulatedTime < endTime)
            {
                transform.position = FlyParabola(startPoint, endPoint, height, accumulatedTime, endTime);

                yield return null;
            }
            else
            {
                transform.position = endPoint;

                break;
            }
        }
    }

    private Vector3 FlyParabola(Vector3 startPoint, Vector3 endPoint, float height, float accumulatedTime, float endTime)
    {
        float timeRatio = accumulatedTime / endTime;
        float yPosition = 4f * timeRatio * (1f - timeRatio) * height;

        return Vector3.Lerp(startPoint, endPoint, timeRatio) + Vector3.up * yPosition;
    }
}
