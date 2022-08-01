using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement _instance;

    public static PlayerMovement Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PlayerMovement>();

            return _instance;
        }
    }

    private Rigidbody _rigidbody;
    private float _speed = 5f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Joystick.Instance.NormalizedDirection != Vector3.zero)
        {
            Vector3 joystickDirection = new Vector3(Joystick.Instance.NormalizedDirection.x, 0, Joystick.Instance.NormalizedDirection.y);

            _rigidbody.velocity = joystickDirection * _speed;
            _rigidbody.rotation = Quaternion.LookRotation(joystickDirection);
        }
        else
            _rigidbody.velocity = Vector3.zero;
    }
}
