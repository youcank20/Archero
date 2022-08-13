using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Serializable]
    struct RoomBundle
    {
        public List<Room> Rooms;

        public RoomBundle(List<Room> rooms)
        {
            Rooms = rooms;
        }
    }

    public PlayerMovement Player;
    public Room _currentRoom;

    [SerializeField] private List<RoomBundle> roomBundles = new List<RoomBundle>();

    private int _currentStage = 0;

    public bool IsCurrentRoomCleared()
    {
        return _currentRoom.IsCleared;
    }

    public void MoveToNextStage()
    {
        int bundleIndex = _currentStage / 10;
        int randomIndex = UnityEngine.Random.Range(0, roomBundles[bundleIndex].Rooms.Count);

        _currentRoom = roomBundles[bundleIndex].Rooms[randomIndex];
        roomBundles[bundleIndex].Rooms.RemoveAt(randomIndex);

        Vector3 spawnPosition = _currentRoom.SpawnPoint.transform.position;
        Player.transform.position = spawnPosition;

        Transform mainCamera = Camera.main.transform;
        mainCamera.position = new Vector3(spawnPosition.x, mainCamera.position.y, spawnPosition.z - 5f);

        _currentRoom.IsActived = true;
    }
}
