using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private float _speed = 5f;

    private const float CAMERA_Z_GAP = -10f;

    private void Update()
    {
        float cameraZ = Mathf.Lerp(transform.position.z, playerTransform.position.z + CAMERA_Z_GAP, Time.deltaTime * _speed);

        transform.position = new Vector3(transform.position.x, transform.position.y, cameraZ);
    }
}
