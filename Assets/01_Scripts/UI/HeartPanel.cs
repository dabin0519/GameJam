using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HeartPanel : MonoBehaviour
{
    [SerializeField] private Image heartImage;

    private Image movingHeart;

    public void HeartUp(int heart)
    {
        if (heart < 0) return;

        for (int i = 0; i < heart; i++)
        {
            Image image = Instantiate(heartImage, transform);
            if (i == heart - 1)
            {
                movingHeart = image;
            }
        }

        Sequence sequence = DOTween.Sequence();
        sequence
            .Append(movingHeart.transform.DOScale(Vector3.zero, 1.1f).SetEase(Ease.InElastic))
            .Append(transform.transform.DOMoveY(-500, 0.4f));
    }
}
