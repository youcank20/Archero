using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCenterCollider : MonoBehaviour
{
    public StageManager StageManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (StageManager.IsCurrentRoomCleared())
            StageManager.MoveToNextStage();
    }
}
