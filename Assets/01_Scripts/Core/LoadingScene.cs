using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    private void Start()
    {
        StageManager.Instance.LoadStage();
    }
}
