using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructList;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageSystem : MonoBehaviour
{
    public static StageSystem Instance;

    public event Action OnStartEvt; //�����ų��

    [HideInInspector] public bool IsPlay { get; set; }

    [Header("����Ʈ")]
    [SerializeField] private List<PuzzleImage> puzzleImages;
    //[SerializeField] private List<StageData> stageData;

    [Header("�ܺ�����")]
    [SerializeField] private Transform settingPanel;
    //[SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private RectTransform timeImage;
    private Image timeFillImage;
    private bool isShaking = false;

    //private TextMeshProUGUI timeText;
    //private RectTransform starPanel;
    //private Button stageBtn;
    //private Button nextBtn;

    [SerializeField] private float maxPlayTime;
    private float currentPlayTime;
    private int currentStage;

    private PlayData playData;

    private EndFlag endFlag;
    private PlayerHP playerHP;

    //private int clearAmount;
    //private int heart;

    private void Awake()
    {
        Instance = this;
        playData = Resources.Load<PlayData>("PlayData");

        endFlag = FindObjectOfType<EndFlag>();
        playerHP = FindObjectOfType<PlayerHP>();
        endFlag.EndEvent += GameClear;
        playerHP.DieEvent += GameLose;

        timeFillImage = timeImage.Find("Fill").GetComponent<Image>();
        currentPlayTime = maxPlayTime;
    }

    private void Update()
    {
        if (!IsPlay)
        {
            currentPlayTime -= Time.deltaTime;
            //timeText.text = currentPlayTime.ToString("##");
            timeFillImage.fillAmount = currentPlayTime / maxPlayTime;
        }

        if(timeFillImage.fillAmount < 0.3f && !isShaking)// added
        {
            isShaking = true;
            timeImage.DOShakePosition((maxPlayTime - currentPlayTime) * 0.5f, 10, 15);
            timeFillImage.DOColor(Color.red, maxPlayTime - currentPlayTime);
        }

        if (currentPlayTime <= 0)
        {
            IsPlay = true;
            currentPlayTime = 999;
            //timeText.gameObject.SetActive(false);
            DOTween.Kill(timeImage);
            timeImage.DOAnchorPosY(timeImage.sizeDelta.y, 0.8f).SetEase(Ease.InOutBack);
            isShaking=false;
            OnStartEvt?.Invoke();
        }
    }

    public void GameClear() //����Ŭ����
    {
        //_clearAmount++; // �̰� �ƴ϶� ClearAmount++; ��
        //gameData.clearAmount++;
        playData.clearAmount++;
        SceneManager.LoadScene("LoadingScene");
    }

    public void GameLose() //������
    {
        //_heart--;
        //TextAsset textAsset;
        //textAsset.
        //gameData.heart--;
        playData.heart--;
        if (playData.heart <= 0)
        {
            Debug.Log("게임종료");
            SceneManager.LoadScene("GameoverScene");
        }
        else
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }

    //private void GameClear() //OnClearEvt���������� ?.Invoke() ���ָ� �Ϸ�
    //{
    //    OnStopEvt?.Invoke(); //�÷��̾� ���߰�

    //    string second = TimeSpan.FromSeconds(currentPlayTime).ToString("mm\\:ss");
    //    var time = second.Split(":");
    //    timeText.text = $"{time[0]}m {time[1]}s";

    //    //â�߱�(�ɸ��ð�, ���� ��, ������������ ��ư, �������� ���� ��ư)
    //    clearPanel.DOMoveY(0, 0.5f).SetEase(Ease.InOutBounce)
    //        .OnComplete(() => {
    //            stageBtn.onClick.AddListener(() => { GameOut(); });
    //            nextBtn.onClick.AddListener(() => { NextGame(-1);/*���ڷ� ���� �������� + 1 �ֱ�*/});
    //        });
    //}
}
