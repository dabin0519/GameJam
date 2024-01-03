using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FootImageController : MonoBehaviour
{
    [SerializeField] private GameObject _image;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        SetFootImage();
        //Debug.Log(_playerController.Count);
    }

    public void SetFootImage()
    {
        int amount = (_playerController.MaxCount - _playerController.Count) - transform.childCount;
        if(amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject go = Instantiate(_image, transform);
                Sequence seq = DOTween.Sequence();

                RectTransform rt = go.transform.GetChild(0).GetComponent<RectTransform>();
                SpriteRenderer sr = go.transform.GetChild(0).GetComponent<SpriteRenderer>();
                seq.Append(rt.DOAnchorPosY(0, 0.2f))
                    .SetEase(Ease.OutQuad);
                seq.Join(sr.DOFade(1, 0.2f))
                    .SetEase(Ease.OutQuad);
            }
        }
        else if(amount < 0)
        {
            for (int i = 0; i < Mathf.Abs(amount); i++)
            {
                if (transform.childCount - 1 < 0) break;
                GameObject go = transform.GetChild(transform.childCount - 1).gameObject;
                Sequence seq = DOTween.Sequence();

                RectTransform rt = go.transform.GetChild(0).GetComponent<RectTransform>();
                Image sr = go.transform.GetChild(0).GetComponent<Image>();
                seq.Append(rt.DOAnchorPosY(-15, 0.2f))
                    .SetEase(Ease.OutQuad);
                seq.Join(sr.DOFade(0, 0.2f))
                    .SetEase(Ease.OutQuad);
                seq.OnComplete(() => Destroy(go));
            }
        }
    }
}
