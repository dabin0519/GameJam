using DG.Tweening;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    [Header("LeaderBoard")]
    [SerializeField] private GameObject _rankCell;
    [SerializeField] private Transform _contentTrm;
    [SerializeField] private List<Sprite> _medal;

    private void Start()
    {
        FirebaseManager.Instance.OnLeaderboardSetting += SetLeaderboardUI;
    }

    private void OnDisable()
    {
        FirebaseManager.Instance.OnLeaderboardSetting -= SetLeaderboardUI;
    }

    public void LeaderBoardOn()
    {
        FirebaseManager.Instance.SetLeaderboard();
        RectTransform rt = transform.GetComponent<RectTransform>();
        rt.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutQuad);
    }

    public void LeaderBoardOff()
    {
        RectTransform rt = transform.GetComponent<RectTransform>();
        rt.DOAnchorPosY(Screen.height, 0.3f).SetEase(Ease.OutQuad);
    }

    private void SetLeaderboardUI(List<DataSnapshot> childrenList)
    {
        for (int i = childrenList.Count - 1; i >= 0; i--)
        {
            string userId = childrenList[i].Key;
            int score = int.Parse(childrenList[i].Value.ToString());

            //Debug.Log($"UserID: {userId}, Score: {score}");

            Transform content = Instantiate(_rankCell, _contentTrm).transform;
            if (childrenList.Count - i <= 3)
                content.Find("RankingImage").GetComponent<Image>().sprite = _medal[childrenList.Count - i - 1];
            else
                content.Find("RankingImage").GetComponent<Image>().color = Color.clear;
            content.Find("RankingImage/RankingText").GetComponent<TextMeshProUGUI>().text
                = (childrenList.Count - i).ToString();
            content.Find("UserName").GetComponent<TextMeshProUGUI>().text = userId;
            content.Find("Score").GetComponent<TextMeshProUGUI>().text = score.ToString();

            if(userId == Resources.Load<PlayData>("PlayData").userName)
            {
                content.GetComponent<Image>().color = new Color(0.5f, 1, 0.5f);
                Debug.Log(userId);
            }
        }
    }
}
