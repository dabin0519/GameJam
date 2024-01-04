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
    [Header("--ÃÑ ·Îµù½Ã°£ = _scaleDuration x 3 x _repeatCount--")]
    [SerializeField] private float _scaleDuration;
    [SerializeField] private int _repeatCount;
    [SerializeField] private bool _isOneTip;

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

        for (int i = 0; i < _repeatCount + 2; i++)
        {
            int radomValue = Random.Range(0, _tipSO.tipList.Count);

            if(!_isOneTip)
                _tipText.text = _tipSO.tipList[radomValue];
            else if(_isOneTip && i == 0)
                _tipText.text = _tipSO.tipList[radomValue];

            foreach(var image in _loadingImage)
            {
                image.transform.DOScale(1.5f, _scaleDuration);
                yield return new WaitForSeconds(_scaleDuration);
                image.transform.DOScale(0.8f, _scaleDuration);
            }

            if(i == _repeatCount - 1)
                SceneManager.LoadSceneAsync(level);
        }
    }
}
