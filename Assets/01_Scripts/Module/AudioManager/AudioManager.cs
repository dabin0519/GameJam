using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;

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
    }

    public void SetBgm()
    {
        float value;
        audioMixer.GetFloat("BGM", out value);

        if (value == 0)
        {
            audioMixer.SetFloat("BGM", -80);
        }
        else
        {
            audioMixer.SetFloat("BGM", 0);
        }
    }

    public void SetSfx()
    {
        float value;
        audioMixer.GetFloat("SFX", out value);

        if (value == 0)
        {
            audioMixer.SetFloat("SFX", -80);
        }
        else
        {
            audioMixer.SetFloat("SFX", 0);
        }
    }

    public void SetVolume(float value)
    {
        _bgmSource.volume = value;
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
