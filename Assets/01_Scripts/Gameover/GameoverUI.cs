using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameoverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayData playData;

    //����Ÿ�� �̸� �����ؾ��Ѵ�

    private void Awake()
    {
        playData = Resources.Load<PlayData>("PlayData");
        scoreText.text = $"Stage Clear\n{playData.clearAmount}";

        FirebaseManager.Instance.AddScore(playData.userName, playData.clearAmount);
    }
}
