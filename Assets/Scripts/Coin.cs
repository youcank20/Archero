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
        Vector3 startPoint = transform.position + Vector3.up * 0.25f;
        float randomX = Random.Range(-1f, 1f);
        float randomZ = 2f;
        while (randomX * randomX + randomZ * randomZ > 1f)
        {
            randomZ = Random.Range(-1f, 1f);
        }
        Vector3 endPoint = startPoint + new Vector3(randomX, 0f, randomZ);

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

                StageManager.Instance._currentRoom.AddCoin(this);

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

    public void MoveToPlayer(PlayerAttack player)
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 0.5f)
        {
            player.GetCoin();

            StageManager.Instance._currentRoom.RemoveCoin(this);

            gameObject.SetActive(false);
        }
        else
        {
            Vector3 normalizedDirection = (player.transform.position - transform.position).normalized;
            transform.position += normalizedDirection * Time.deltaTime * 10f;
        }
    }
}
