using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;
    public AudioSource BgmSource => _bgmSource;
    public AudioSource SfxSource => _sfxSource;

    [SerializeField] private AudioClip[] _bgmClips;
    [SerializeField] private AudioClip[] _sfxClips;

    [Header("너가 틀고 싶은 음악 번호")]
    [SerializeField] private int _bgmIdx;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform);
        }

        StartBgm(0);
    }

    public void SetBgm(float value)
    {
        _bgmSource.volume = value;
    }

    public void SetSfx(float value)
    {
        _sfxSource.volume = value;
    }

    public void StartBgm(int idx)
    {
        Debug.Log("SNOG");
        _bgmSource.clip = _bgmClips[idx];
        _bgmSource.Play();
    }

    public void PlaySfx(int idx)
    {
        _sfxSource.clip = _sfxClips[idx];
        _sfxSource.Play();
    }
}
