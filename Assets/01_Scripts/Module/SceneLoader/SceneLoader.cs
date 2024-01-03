using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private Transform _containerTrm;
    [SerializeField] private TipListSO _tipSO;
    [SerializeField] private float _minLoadingDuration;
    [SerializeField] private int _repeatCount;

    private TextMeshProUGUI _tipText;
    private Image[] _loadingImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _tipText = _containerTrm.Find("TipText").GetComponent<TextMeshProUGUI>();
        _loadingImage = _containerTrm.Find("CircleGroup").GetComponentsInChildren<Image>();
    }

    public void LoadScene(string level)
    {
        StartCoroutine(LoadSceneCoroutine(level));
    }

    private IEnumerator LoadSceneCoroutine(string level)
    {

        //float radomTime = Random.Range(_minLoadingDuration - 1f, _minLoadingDuration + 2f);
        float radomTime = _minLoadingDuration;

        for(int i = 0; i < _repeatCount + 2; i++)
        {
            int radomValue = Random.Range(0, _tipSO.tipList.Count);

            _tipText.text = _tipSO.tipList[radomValue];

            foreach(var image in _loadingImage)
            {
                float time = radomTime / 6;
                image.transform.DOScale(1.5f, time);
                yield return new WaitForSeconds(time);
                image.transform.DOScale(0.8f, time);
            }

            if(i == _repeatCount - 1)
                SceneManager.LoadSceneAsync(level);
        }
    }
}
