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

    public void SetStartButton(bool isStart)
    {
        if (isStart)
            _startButton.DOAnchorPosY(_startButton.sizeDelta.y, 0.2f).SetEase(Ease.OutQuad);
        else
            _startButton.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutQuad); 
    }
}
