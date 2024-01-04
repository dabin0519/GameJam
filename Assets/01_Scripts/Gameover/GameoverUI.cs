using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayData playData;

    private void Awake()
    {
        playData = PlayDataReset.Instance.playData;
        scoreText.text = $"Stage Clear\n{playData.clearAmount}";

        playData.clearAmount = 0;
        playData.heart = 3;
    }

    private void Start()
    {
        AudioManager.Instance.StartBgm(4);
    }
}
