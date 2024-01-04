using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataReset : MonoBehaviour
{
    private PlayData playData;

    void Start()
    {
        playData = Resources.Load<PlayData>("PlayData");
        playData.clearAmount = 0;
        playData.heart = 3;
    }
}
