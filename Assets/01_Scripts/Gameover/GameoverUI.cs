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
        playData = Resources.Load<PlayData>("PlayData");
        scoreText.text = $"Stage Clear\n{playData.clearAmount}";
    }

    private void Start()
    {
        AudioManager.Instance.StartBgm(4);
    }
}
