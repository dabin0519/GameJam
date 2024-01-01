using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private RectTransform _startButton;
    [SerializeField] private RectTransform _reStartButton;

    public void SetStartButton(bool isStartButtonDown)
    {
        RectTransform inside, outside;
        if (isStartButtonDown)
        {
            inside = _reStartButton;
            outside = _startButton;
        }
        else
        {
            inside = _startButton;
            outside = _reStartButton;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(outside.DOAnchorPosY(outside.sizeDelta.y, 0.2f));
        seq.Append(inside.DOAnchorPosY(0, 0.2f));
    }
}
