using UnityEngine;

public class RandomWheelSet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetPause();

            StartCoroutine(UICanvas.Instance.FadeOut(Content.RandomWheel, true));
        }
    }
}
