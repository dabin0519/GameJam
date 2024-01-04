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

    //[Header("����Ʈ")]
    //[SerializeField] private List<PuzzleImage> puzzleImages;
    //[SerializeField] private List<StageData> stageData;

    [Header("�ܺ�����")]
    [SerializeField] private ButtonEvent buttonEvent;
    [SerializeField] private HeartPanel heartPanel;
    [SerializeField] private Transform settingPanel;
    [SerializeField] private RectTransform timeImage;
    private Image timeFillImage;
    private bool isShaking = false;

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

        //이벤트
        endFlag = FindObjectOfType<EndFlag>();
        playerHP = FindObjectOfType<PlayerHP>();
        endFlag.EndEvent += GameClear;
        playerHP.DieEvent += GameLose;

        //타이머
        timeFillImage = timeImage.Find("Fill").GetComponent<Image>();
        currentPlayTime = maxPlayTime;

        settingPanel.DOMoveY(0, 0.5f);
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
            StartGame();
        }
    }

    public void StartGame()
    {
        IsPlay = true;
        currentPlayTime = 999;
        //timeText.gameObject.SetActive(false);
        DOTween.Kill(timeImage);
        timeImage.DOAnchorPosY(timeImage.sizeDelta.y, 0.8f).SetEase(Ease.InOutBack);
        isShaking = false;
        settingPanel.DOMoveY(-300, 0.5f);
        OnStartEvt?.Invoke();
    }

    public void GameClear() //����Ŭ����
    {
        playData.clearAmount++;
        buttonEvent.SceneLoad("LoadingScene");
    }

    public void GameLose() //������
    {
        StartCoroutine(GameLoseCol());
    }

    private IEnumerator GameLoseCol()
    {
        heartPanel.HeartUp(playData.heart);
        yield return new WaitForSeconds(1.5f);
        playData.heart--;
        if (playData.heart <= 0)
        {
            Debug.Log("게임종료");
            buttonEvent.SceneLoad("GameoverScene");
        }
        else
        {
            buttonEvent.SceneLoad("LoadingScene");
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
