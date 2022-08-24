using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }

    public bool IsPaused { get; private set; }

    public void SetPause()
    {
        Time.timeScale = 0f;

        IsPaused = true;

        Joystick.Instance.PointerUp();
    }

    public void SetResume()
    {
        Time.timeScale = 1f;

        IsPaused = false;
    }
}
