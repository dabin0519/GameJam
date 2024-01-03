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
            .Append(movingHeart.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.7f).SetEase(Ease.InElastic))
            .Join(movingHeart.transform.DORotate(new Vector3(-90f, 0, 0), 0.6f).OnComplete(() =>
            {
                transform.transform.DOMoveY(-500, 0.4f);
            }));
    }
}
