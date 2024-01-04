using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private Image _sfxImage;
    [SerializeField] private Image _bgmImage;

    [SerializeField] private List<Sprite> _sfxOnOffSprite = new List<Sprite>();
    [SerializeField] private List<Sprite> _bgmOnOffSprite = new List<Sprite>();

    private RectTransform _rt;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        SetPanelOff();
        SetImages();
    }

    public void SetSfxVolume()
    {
        if (AudioManager.Instance.SfxSource.volume < 0.5f)
        {
            AudioManager.Instance.SetSfx(1);
        }
        else
        {
            AudioManager.Instance.SetSfx(0);
        }
        SetImages();
    }

    public void SetBgmVolume()
    {
        if (AudioManager.Instance.BgmSource.volume < 0.5f)
        {
            AudioManager.Instance.SetVolume(1);
        }
        else
        {
            AudioManager.Instance.SetVolume(0);
        }
        SetImages();
    }

    private void SetImages()
    {
        if (AudioManager.Instance.SfxSource.volume < 0.5f)
        {
            _sfxImage.sprite = _sfxOnOffSprite[0];
        }
        else
        {
            _sfxImage.sprite = _sfxOnOffSprite[1];
        }

        if (AudioManager.Instance.BgmSource.volume < 0.5f)
        {
            _bgmImage.sprite = _bgmOnOffSprite[0];
        }
        else
        {
            _bgmImage.sprite = _bgmOnOffSprite[1];
        }

        Debug.Log($"sfx {AudioManager.Instance.SfxSource.volume < 0.5f}");
        Debug.Log($"bgm {AudioManager.Instance.BgmSource.volume < 0.5f}");
    }

    public void SetPanelOn()
    {
        _rt.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutQuad);
    }

    public void SetPanelOff()
    {
        _rt.DOAnchorPosY(Screen.height, 0.2f).SetEase(Ease.OutQuad);
    }
}
