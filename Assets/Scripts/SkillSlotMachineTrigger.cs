using UnityEngine;

public class SkillSlotMachineTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetPause();

            StartCoroutine(UICanvas.Instance.FadeOutCoroutine(Content.SkillSlotMachine, 0.5f, true));
        }
    }
}
