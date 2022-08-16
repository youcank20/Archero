using UnityEngine;

public class TopCenterCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (StageManager.Instance.IsCurrentRoomCleared())
            StageManager.Instance.MoveToNextStage();
    }
}
