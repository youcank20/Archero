using UnityEngine;

public class RandomWheelSet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetPause();

            StartCoroutine(UICanvas.Instance.FadeOutCoroutine(Content.RandomWheel, 0.5f, false, 1f, true));
        }
    }
}
