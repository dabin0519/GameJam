using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataReset : MonoBehaviour
{
    public static PlayDataReset Instance;

    public PlayData playData;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform);
        }

        playData = Resources.Load<PlayData>("PlayData");
        playData.clearAmount = 0;
        playData.heart = 3;
    }
}
