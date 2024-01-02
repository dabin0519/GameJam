using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private TextMeshProUGUI _tipText;
    [SerializeField] private TipListSO _tipSO;
    [SerializeField] private float _minLoadingDuration;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

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

        _tipText.text = $"tip : {_tipSO.tipList[radomValue]}";

        yield return new WaitForSeconds(_minLoadingDuration);

        SceneManager.LoadScene(level);
    }
}
