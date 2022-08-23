using UnityEngine;

public class TargetMarker : MonoBehaviour
{
    private bool _isGoingUp = false;

    private void Update()
    {
        if (_isGoingUp)
        {
            transform.position += Vector3.up * Time.deltaTime;

            if (transform.position.y > 4f)
                _isGoingUp = false;
        }
        else
        {
            transform.position -= Vector3.up * Time.deltaTime;

            if (transform.position.y < 3.5f)
                _isGoingUp = true;
        }
    }

    private void OnDisable()
    {
        _isGoingUp = false;
    }

    public void SetPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition + Vector3.up * 4f;
    }
}
