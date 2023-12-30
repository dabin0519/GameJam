using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    [SerializeField] private Slider _loadingBar;

    public void Test()
    {
        Debug.Log("Test");
    }

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
            _loadingBar.value = value;
            yield return null;
        }
    }
}
