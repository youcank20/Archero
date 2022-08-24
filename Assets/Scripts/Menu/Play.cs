using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    [SerializeField] private Image panelImage;

    public void LoadGameScene()
    {
        StartCoroutine(LoadGameSceneCoroutine());
    }

    private IEnumerator LoadGameSceneCoroutine()
    {
        StartCoroutine(FadeOutCoroutine());

        while (panelImage.color.a < 1f)
            yield return null;

        SceneManager.LoadScene("Game");
    }

    private IEnumerator FadeOutCoroutine()
    {
        while (panelImage.color.a < 1f)
        {
            panelImage.color += new Color(0f, 0f, 0f, Time.deltaTime);

            if (panelImage.color.a > 1f)
                panelImage.color = Color.black;

            yield return null;
        }
    }
}
