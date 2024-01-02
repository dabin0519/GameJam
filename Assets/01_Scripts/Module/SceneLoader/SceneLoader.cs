using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoSingleton<SceneLoader>
{
    [SerializeField] private TextMeshProUGUI _tipText;
    [SerializeField] private TipListSO _tipSO;
    [SerializeField] private float _minLoadingDuration;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene("MainScene");
        }
    }

    public void LoadScene(string level)
    {
        StartCoroutine(LoadSceneAsync(level));
    }

    private IEnumerator LoadSceneAsync(string level)
    {
        int radomValue = Random.Range(0, _tipSO.tipList.Count);
        _tipText.text = _tipSO.tipList[radomValue];

        yield return new WaitForSeconds(_minLoadingDuration);

        SceneManager.LoadScene(level);
    }
}
