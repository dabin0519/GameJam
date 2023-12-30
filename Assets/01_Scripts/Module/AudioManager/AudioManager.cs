using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip[] _bgmClips;
    [SerializeField] private AudioClip[] _sfxClips;
    
    private void Awake()
    {
        
    }

    public void SetVolume(float value)
    {
        _bgmSource.volume = value;
        _sfxSource.volume = value;
    }

    // ���� �ʿ��� ��� ����� �����ϱ� ������
}
