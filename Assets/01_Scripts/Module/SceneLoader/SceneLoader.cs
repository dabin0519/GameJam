using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    [SerializeField] private TextMeshProUGUI _loadingTxt;

    public void LoadScene(string level)
    {
        StartCoroutine(LoadSceneAsync(level));
    }

    private IEnumerator LoadSceneAsync(string level)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(level);

        while(!loadOperation.isDone)
        {
            float value = Mathf.Clamp01(loadOperation.progress / 0.9f);

            switch((loadOperation.progress / 0.9f) % 3)
            {
                case 0:
                    _loadingTxt.text = "Loading.";
                    break;
                case 1:
                    _loadingTxt.text = "Loading..";
                    break;
                case 2:
                    _loadingTxt.text = "Loading...";
                    break;
            }
            //_loadingBar.value = value;
            yield return null;
        }
    }
}
