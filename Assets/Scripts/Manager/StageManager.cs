using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private static StageManager _instance;

    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<StageManager>();

            return _instance;
        }
    }

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

    private void Start()
    {
        for (int i = 0; i < roomBundles.Count; ++i)
        {
            for (int j = 0; j < roomBundles[i].Rooms.Count; ++j)
            {
                roomBundles[i].Rooms[j].gameObject.SetActive(false);
            }
        }
    }

    public bool IsCurrentRoomCleared()
    {
        return _currentRoom.IsCleared;
    }

    public void MoveToNextStage()
    {
        StartCoroutine(MoveToNextStageCoroutine());
    }

    private IEnumerator MoveToNextStageCoroutine()
    {
        Coroutine coroutine = StartCoroutine(UICanvas.Instance.FadeOutCoroutine(Content.NextStage, 1f, true, 5f));

        while (UICanvas.Instance.GetPanelAlpha() < 1f)
            yield return null;

        StopCoroutine(coroutine);

        int bundleIndex = _currentStage / 10;
        int randomIndex = UnityEngine.Random.Range(0, roomBundles[bundleIndex].Rooms.Count);

        if (roomBundles[bundleIndex].Rooms[randomIndex] != null)
            _currentRoom.gameObject.SetActive(false);

        _currentRoom = roomBundles[bundleIndex].Rooms[randomIndex];
        roomBundles[bundleIndex].Rooms.RemoveAt(randomIndex);

        Vector3 spawnPosition = _currentRoom.SpawnPoint.transform.position;
        Player.transform.position = spawnPosition;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Transform mainCamera = Camera.main.transform;
        mainCamera.position = new Vector3(spawnPosition.x, mainCamera.position.y, spawnPosition.z - 5f);

        _currentRoom.gameObject.SetActive(true);

        coroutine = StartCoroutine(UICanvas.Instance.FadeInCoroutine(true, 10f));

        while (UICanvas.Instance.GetPanelAlpha() > 0f)
            yield return null;

        StopCoroutine(coroutine);

        _currentRoom.IsActived = true;
    }
}
