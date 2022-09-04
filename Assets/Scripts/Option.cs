using UnityEngine;

public class Option : MonoBehaviour
{
    public void OnClickContinue()
    {
        UICanvas.Instance.SetContentSetActive(Content.Option, false);
        UICanvas.Instance.SetAlpha(0f);

        GameManager.Instance.SetContinue();
    }

    public void OnClickSound()
    {
    }

    public void OnClickMusic()
    {
    }

    public void OnClickMenu()
    {
        GameManager.Instance.LoadMenuScene();
    }

    public void OnClickCoin()
    {
    }
}
