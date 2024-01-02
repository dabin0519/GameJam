using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameEndPanel : MonoBehaviour
{
    [SerializeField] List<RectTransform> stars;

    public void SetStars()
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < stars.Count; i++)
        {
            seq.Append(stars[i].DOSizeDelta(Vector2.one, 0.3f));
            seq.Join(stars[i].DORotate())
        }
    }
}
