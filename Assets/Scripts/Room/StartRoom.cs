using System.Collections;

public class StartRoom : Room
{
    private bool _isInitialized = false;

    protected override void Start()
    {
        base.Start();

        IsActived = true;
    }

    protected override void Update()
    {
        if (!_isInitialized)
        {
            StartCoroutine(StartGameSceneCoroutine());

            _isInitialized = true;
        }
        else
            base.Update();
    }

    private IEnumerator StartGameSceneCoroutine()
    {
        StartCoroutine(UICanvas.Instance.FadeInCoroutine());

        while (UICanvas.Instance.GetPanelAlpha() > 0f)
            yield return null;

        StartCoroutine(Player.Instance.AppearCoroutine());

        while (Player.Instance.transform.position.y > 0f)
            yield return null;

        GameManager.Instance.SetResume();
    }
}
