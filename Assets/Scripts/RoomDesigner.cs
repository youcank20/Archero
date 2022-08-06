using UnityEngine;

public class RoomDesigner : MonoBehaviour
{
    [SerializeField] private GameObject parentRoom;
    [SerializeField] private string roomName;
    [SerializeField] private Vector3 position;
    [SerializeField] private int length;
    [SerializeField] private GameObject roomObjectPrefab;

    public void MakeRoom()
    {
        GameObject room = new GameObject(roomName);

        room.transform.parent = parentRoom.transform;
        room.transform.position = position;

        MakeRoomSpace(room.transform);
        MakeRoomCollider(room.transform);
    }

    private void MakeRoomSpace(Transform roomTransform)
    {
        GameObject space = MakeGameObject("Space", roomTransform, Vector3.zero);

        int min = -(length / 2);

        for (int i = 0; i < length; ++i)
        {
            for (int j = 0; j < 11; ++j)
            {
                MakeObject(space.transform, new Vector3(j - 5, -0.5f, min + i));
            }

            MakeObject(space.transform, new Vector3(-6f, 0.5f, min + i));
            MakeObject(space.transform, new Vector3(6f, 0.5f, min + i));
        }
    }

    private void MakeObject(Transform parentTransform, Vector3 localPosition)
    {
        GameObject roomObject = Instantiate(roomObjectPrefab);

        roomObject.transform.parent = parentTransform;
        roomObject.transform.localPosition = localPosition;
    }

    private void MakeRoomCollider(Transform roomTransform)
    {
        GameObject collider = MakeGameObject("Collider", roomTransform, Vector3.zero);

        MakeCollider("Left", collider.transform, new Vector3(-6f, 0.5f, 0f), new Vector3(1f, 1f, length));
        MakeCollider("Center", collider.transform, new Vector3(0f, -0.5f, 0f), new Vector3(11f, 1f, length));
        MakeCollider("Right", collider.transform, new Vector3(6f, 0.5f, 0f), new Vector3(1f, 1f, length));
    }

    private void MakeCollider(string name, Transform parentTransform, Vector3 localPosition, Vector3 size)
    {
        GameObject colliderObject = MakeGameObject(name, parentTransform, localPosition);

        BoxCollider boxCollider = colliderObject.AddComponent<BoxCollider>();

        boxCollider.size = size;
    }

    private GameObject MakeGameObject(string name, Transform parentTransform, Vector3 localPosition)
    {
        GameObject gameObject = new GameObject(name);

        gameObject.transform.parent = parentTransform;
        gameObject.transform.localPosition = localPosition;

        return gameObject;
    }
}
