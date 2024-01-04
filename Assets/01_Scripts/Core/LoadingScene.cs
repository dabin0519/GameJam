using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private int _bgmIdx;
    [SerializeField] private bool _d;

    private void Start()
    {
        if(_d)
            StageManager.Instance.LoadStage();
        else
            AudioManager.Instance.StartBgm(_bgmIdx);
    }
}
