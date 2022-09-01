using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : Room
{
    private bool _isInitialized = false;
    private bool _isSpreadCompleted = false;

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

        coroutine = StartCoroutine(SpreadDustCoroutine());

        while (!_isSpreadCompleted)
            yield return null;

        StopCoroutine(coroutine);

        GameManager.Instance.SetContinue();
    }

    public IEnumerator SpreadDustCoroutine()
    {
        List<Dust> dusts = new List<Dust>();

        for (int i = 0; i < 16; ++i)
        {
            Dust dust = ObjectPoolManager.Instance.Get("Dust").GetComponent<Dust>();
            Coroutine coroutine = StartCoroutine(dust.DropDustCoroutine(Player.Instance.transform.position));

            dusts.Add(dust);
        }

        while (dusts.Count > 0)
        {
            for (int i = dusts.Count - 1; i >= 0; --i)
            {
                if (dusts[i].IsReleased)
                    dusts.RemoveAt(i);
            }

            yield return null;
        }

        _isSpreadCompleted = true;
    }
}
