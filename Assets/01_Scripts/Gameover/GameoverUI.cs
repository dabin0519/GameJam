using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayData playData;

    //데이타에 이름 저장해야한다

    private void Awake()
    {
        playData = Resources.Load<PlayData>("PlayData");
        scoreText.text = $"Stage Clear\n{playData.clearAmount}";

        FirebaseManager.Instance.AddScore(playData.userName, playData.clearAmount);
    }
}
