using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SetContinue()
    {
        Time.timeScale = 1f;

        IsPaused = false;
    }

    public void LoadMenuScene(bool value = true)
    {
        if (value)
            StartCoroutine(LoadMenuSceneCoroutine());
        else
        {
            SetContinue();

            SceneManager.LoadScene("Menu");
        }
    }

    private IEnumerator LoadMenuSceneCoroutine()
    {
        StartCoroutine(Option.Instance.FadeOutCoroutine(1f));

        while (Option.Instance.GetPanelAlpha() < 1f)
            yield return null;

        SetContinue();

        SceneManager.LoadScene("Menu");
    }
}
