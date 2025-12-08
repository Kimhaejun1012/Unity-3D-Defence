using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Game";

    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI loadingText;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress);

            if (progressBar != null)
                progressBar.fillAmount = progress;

            if (loadingText != null)
                loadingText.text = $"Loading... {(progress * 100f):0}%";

            yield return null;
        }
    }
}
