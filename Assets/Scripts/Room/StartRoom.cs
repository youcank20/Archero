using System.Collections;
using UnityEngine;

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
        Coroutine coroutine = StartCoroutine(UICanvas.Instance.FadeInCoroutine(true));

        while (UICanvas.Instance.GetPanelAlpha() > 0f)
            yield return null;

        StopCoroutine(coroutine);

        coroutine = StartCoroutine(Player.Instance.AppearCoroutine());

        while (Player.Instance.transform.position.y > 0f)
            yield return null;

        StopCoroutine(coroutine);

        GameManager.Instance.SetResume();
    }
}
