using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private RectTransform _startButton;
    [SerializeField] private RectTransform _restartButton;

    [SerializeField] private RectTransform _pausePanel;
    [SerializeField] private GameObject _otherButtonControlPanel;

    public bool testIsPause = false;

    private void Start()
    {
        _pausePanel.transform.Find("StageName").GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name; 
    }

    public void SetStartButton(bool isStartButtonDown)
    {
        RectTransform inside, outside;
        if (isStartButtonDown)
        {
            inside = _restartButton;
            outside = _startButton;
        }
        else
        {
            inside = _startButton;
            outside = _restartButton;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(outside.DOAnchorPosY(outside.sizeDelta.y, 0.2f));
        seq.Append(inside.DOAnchorPosY(0, 0.2f));
    }

    public void PauseToggleButton()
    {
        testIsPause = !testIsPause;
        SetPausePanel(testIsPause);
    }

    public void PauseFalseButton()
    {
        testIsPause = false;
        SetPausePanel(testIsPause);
    }

    public void SetPausePanel(bool isPause)
    {
        float targetY;

        DOTween.Kill(_pausePanel);

        _otherButtonControlPanel.SetActive(isPause);
        if (isPause)
        {
            targetY = 0;
        }
        else
        {
            targetY = Screen.height * 1.2f;
        }

        _pausePanel.DOAnchorPosY(targetY, isPause ? 0.85f : 0.5f).SetEase(Ease.OutBounce);
    }
}
