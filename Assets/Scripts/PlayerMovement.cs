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
    private float _accumulatedTime = 0f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Joystick.Instance.NormalizedDirection != Vector3.zero)
        {
            Vector3 joystickDirection = new Vector3(Joystick.Instance.NormalizedDirection.x, 0, Joystick.Instance.NormalizedDirection.y);

            transform.Translate(joystickDirection * Time.fixedDeltaTime * _speed, Space.World);
            Player.Instance.LookAt(transform.position + joystickDirection);

            Player.Instance.ChangeState(EState.Move);

            _accumulatedTime += Time.fixedDeltaTime;

            if (_accumulatedTime >= 0.5f)
            {
                _accumulatedTime = 0f;

                Dust dust = ObjectPoolManager.Instance.Get("Dust").GetComponent<Dust>();
                StartCoroutine(dust.MoveDustCoroutine(Player.Instance.transform.position - joystickDirection * 0.25f));
            }
        }
        else
        {
            if (Player.Instance.HasTarget())
                Player.Instance.ChangeState(EState.Attack);
            else
                Player.Instance.ChangeState(EState.Idle);
        }
    }
}
