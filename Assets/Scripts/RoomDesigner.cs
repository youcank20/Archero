using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomDesigner : MonoBehaviour
{
    [Serializable]
    struct Obstacle
    {
        public GameObject objectPrefab;
        public List<Vector3> positions;
    }

    [SerializeField] private GameObject parentRoom;
    [SerializeField] private string roomName;
    [SerializeField] private Vector3 position;
    [SerializeField] private int length;
    [SerializeField] private GameObject roomObjectPrefab;
    [SerializeField] private GameObject stairsPrefab;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private GameObject halfStonePrefab;
    [SerializeField] private List<Obstacle> obstacles;

    public void MakeRoom()
    {
        GameObject room = new GameObject(roomName);

        room.transform.parent = parentRoom.transform;
        room.transform.position = position;

        MakeTop(room.transform);
        MakeMiddle(room.transform);
        MakeBottom(room.transform);
    }

    private void MakeTop(Transform parentTransform)
    {
        GameObject top = MakeGameObject("Top", parentTransform, new Vector3(0f, 0f, length / 2 + 1));

        MakeTopCollider(top.transform);
        MakeTopSpace(top.transform);
    }

    private void MakeMiddle(Transform parentTransform)
    {
        GameObject middle = MakeGameObject("Middle", parentTransform, Vector3.zero);

        MakeMiddleCollider(middle.transform);
        MakeMiddleSpace(middle.transform);
    }

    private void MakeBottom(Transform parentTransform)
    {
        GameObject bottom = MakeGameObject("Bottom", parentTransform, new Vector3(0f, 0f, -(length / 2) - 1));

        GameObject collider = MakeGameObject("Collider", bottom.transform, Vector3.zero);

        MakeCollider("Bottom", collider.transform, Vector3.up, new Vector3(13f, 2f, 1f));

        GameObject space = MakeGameObject("Space", bottom.transform, Vector3.zero);

        for (int i = 0; i < 11; ++i)
        {
            for (int j = 0; j < 13; ++j)
            {
                MakeRoomObject(space.transform, new Vector3(j - 6, 0.5f, -i));
            }
        }
    }

    private void MakeTopCollider(Transform parentTransform)
    {
        GameObject collider = MakeGameObject("Collider", parentTransform, Vector3.zero);

        MakeCollider("Left", collider.transform, new Vector3(-4f, 1f, 2f), new Vector3(5f, 2f, 5f));
        MakeCollider("Center", collider.transform, new Vector3(0f, 1f, 2f), new Vector3(3f, 2f, 5f));
        MakeCollider("Right", collider.transform, new Vector3(4f, 1f, 2f), new Vector3(5f, 2f, 5f));
    }

    private void MakeTopSpace(Transform parentTransform)
    {
        GameObject space = MakeGameObject("Space", parentTransform, Vector3.zero);

        for (int i = 0; i < 13; ++i)
        {
            for (int j = 0; j < 13; ++j)
            {
                if (i > 1 || j < 5 || j > 7)
                    MakeRoomObject(space.transform, new Vector3(j - 6, 0.5f, i));
            }
        }

        for (int i = 0; i < 3; ++i)
        {
            MakeObject(space.transform, new Vector3(i - 1, 0f, 0.5f), new Vector3(0f, 180f, 0f), Vector3.one, stairsPrefab);
        }

        MakeObject(space.transform, new Vector3(-1.05f, 1f, 2f), Vector3.zero, Vector3.one, doorPrefab);
        MakeObject(space.transform, new Vector3(1.05f, 1f, 2f), new Vector3(0f, 180f, 0f), Vector3.one, doorPrefab);

        MakeObject(space.transform, new Vector3(-2.6f, 1.5f, 2.35f), Vector3.zero, Vector3.one, stonePrefab);
        MakeObject(space.transform, new Vector3(-1.6f, 1.5f, 2.35f), Vector3.zero, Vector3.one, stonePrefab);
        MakeObject(space.transform, new Vector3(1.6f, 1.5f, 2.35f), Vector3.zero, Vector3.one, stonePrefab);
        MakeObject(space.transform, new Vector3(2.6f, 1.5f, 2.35f), Vector3.zero, Vector3.one, stonePrefab);
        MakeObject(space.transform, new Vector3(-1.6f, 2.5f, 2.35f), Vector3.zero, Vector3.one, stonePrefab);
        MakeObject(space.transform, new Vector3(1.6f, 2.5f, 2.35f), Vector3.zero, Vector3.one, stonePrefab);
        MakeObject(space.transform, new Vector3(-0.6f, 3f, 2.35f), Vector3.zero, new Vector3(1.2f, 1f, 1f), halfStonePrefab);
        MakeObject(space.transform, new Vector3(0.6f, 3f, 2.35f), Vector3.zero, new Vector3(1.2f, 1f, 1f), halfStonePrefab);
    }

    private void MakeMiddleCollider(Transform parentTransform)
    {
        GameObject collider = MakeGameObject("Collider", parentTransform, Vector3.zero);

        MakeCollider("Left", collider.transform, new Vector3(-6f, 1f, 0f), new Vector3(1f, 2f, length));
        MakeCollider("Center", collider.transform, new Vector3(0f, -0.5f, 0f), new Vector3(11f, 1f, length));
        MakeCollider("Right", collider.transform, new Vector3(6f, 1f, 0f), new Vector3(1f, 2f, length));
    }

    private void MakeMiddleSpace(Transform parentTransform)
    {
        GameObject space = MakeGameObject("Space", parentTransform, Vector3.zero);

        int min = -(length / 2);

        for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j < 11; ++j)
            {
                MakeRoomObject(space.transform, new Vector3(j - 5, -0.5f, min + i));
            }

            MakeRoomObject(space.transform, new Vector3(-6f, 0.5f, min + i));
            MakeRoomObject(space.transform, new Vector3(6f, 0.5f, min + i));
        }

        MakeObstacles(space.transform);
    }

    private void MakeObstacles(Transform parentTransform)
    {
        for (int i = 0; i < obstacles.Count; ++i)
        {
            for (int j = 0; j < obstacles[i].positions.Count; ++j)
            {
                MakeObject(parentTransform, obstacles[i].positions[j], Vector3.zero, Vector3.one, obstacles[i].objectPrefab);
            }
        }
    }

    private void MakeCollider(string name, Transform parentTransform, Vector3 localPosition, Vector3 size)
    {
        GameObject colliderObject = MakeGameObject(name, parentTransform, localPosition);

        BoxCollider boxCollider = colliderObject.AddComponent<BoxCollider>();

        boxCollider.size = size;
    }

    private void MakeRoomObject(Transform parentTransform, Vector3 localPosition)
    {
        GameObject roomObject = Instantiate(roomObjectPrefab);

        roomObject.transform.parent = parentTransform;
        roomObject.transform.localPosition = localPosition;
    }

    private void MakeObject(Transform parentTransform, Vector3 localPosition, Vector3 localRotation, Vector3 localScale, GameObject objectPrefab)
    {
        GameObject roomObject = Instantiate(objectPrefab);

        roomObject.transform.parent = parentTransform;
        roomObject.transform.localPosition = localPosition;
        roomObject.transform.localRotation = Quaternion.Euler(localRotation);
        roomObject.transform.localScale = localScale;
    }

    private GameObject MakeGameObject(string name, Transform parentTransform, Vector3 localPosition)
    {
        GameObject gameObject = new GameObject(name);

        gameObject.transform.parent = parentTransform;
        gameObject.transform.localPosition = localPosition;

        return gameObject;
    }
}
