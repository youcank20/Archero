using UnityEngine;

public class TargetMarker : MonoBehaviour
{
    private GameObject _target;
    private bool _isGoingUp = false;
    private float _yPosition = 4f;

    public void SetTarget(GameObject targetObject)
    {
        _target = targetObject;

        if (_isGoingUp)
        {
            _yPosition += Time.deltaTime;

            transform.position = _target.transform.position + Vector3.up * _yPosition;

            if (_yPosition > 4f)
                _isGoingUp = false;
        }
        else
        {
            _yPosition -= Time.deltaTime;

            transform.position = _target.transform.position + Vector3.up * _yPosition;

            if (_yPosition < 3.5f)
                _isGoingUp = true;
        }
    }

    private void OnDisable()
    {
        _isGoingUp = false;
        _yPosition = 4f;
    }
}
