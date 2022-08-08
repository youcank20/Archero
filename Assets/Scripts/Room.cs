using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<GameObject> EnemyList = new List<GameObject>();
    public PlayerAttack Player;
    public GameObject SpawnPoint;
    public BoxCollider TopCenterCollider = new BoxCollider();
    public Transform LeftDoorTransform;
    public Transform RightDoorTransform;
    public bool IsActived = false;
    public bool IsCleared => _isCleared;

    private bool _isCleared = false;
    private bool _isTopCenterPushed = false;
    private bool _isDoorOpened = false;
    private float _doorAngle = 0f;

    private void Update()
    {
        if (IsActived)
        {
            if (EnemyList.Count != 0)
            {
                Player.EnemyList = EnemyList;
            }
            else
            {
                _isCleared = true;
            }

            if (_isCleared)
            {
                if (!_isTopCenterPushed)
                    PushTopCenter();

                if (!_isDoorOpened)
                    OpenDoor();
            }
        }
    }

    private void PushTopCenter()
    {
        TopCenterCollider.center += Vector3.forward * 2.5f;

        _isTopCenterPushed = true;
    }

    private void OpenDoor()
    {
        _doorAngle = Time.deltaTime * 120f;

        if (LeftDoorTransform.rotation.y < 120f)
        {
            LeftDoorTransform.Rotate(Vector3.up * _doorAngle);
            RightDoorTransform.Rotate(Vector3.down * _doorAngle);

            if (LeftDoorTransform.rotation.eulerAngles.y >= 120f)
            {
                LeftDoorTransform.rotation = Quaternion.Euler(LeftDoorTransform.rotation.x, 120f, LeftDoorTransform.rotation.z);
                RightDoorTransform.rotation = Quaternion.Euler(RightDoorTransform.rotation.x, 60f, RightDoorTransform.rotation.z);

                _isDoorOpened = true;
            }
        }
    }
}
