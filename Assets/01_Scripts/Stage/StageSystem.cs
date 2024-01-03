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
    [SerializeField] private List<StageData> stageData;

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

    [SerializeField] private float currentPlayTime;
    private int currentStage;

    private int clearAmount;
    private int heart;

    private int _clearAmount;
    private int _heart;

    private void Awake()
    {
        Instance = this;
        //LoadStage();
        timeFillImage = timeImage.Find("Fill").GetComponent<Image>();
    }

    private void Update()
    {
        if (!IsPlay)
        {
            currentPlayTime -= Time.deltaTime;
            //timeText.text = currentPlayTime.ToString("##");
            timeFillImage.fillAmount = currentPlayTime / stageData[currentStage].playTime;
        }

        if(currentPlayTime / stageData[currentStage].playTime < 0.3f && !isShaking)// added
        {
            isShaking = true;
            timeImage.DOShakePosition((stageData[currentStage].playTime - currentPlayTime) * 0.5f, 10, 15);
            timeFillImage.DOColor(Color.red, stageData[currentStage].playTime - currentPlayTime);
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

    private void LoadStage()
    {
        currentPlayTime = stageData[currentStage].playTime;

        Instantiate(stageData[currentStage].map); //�� ����

        foreach (PuzzleData puzzleData in stageData[currentStage].puzzleDatas) //���� ����
        {
            //PuzzleImage settingPuzzle = puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType);
            PuzzleImage settingPuzzle = Instantiate(puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType), settingPanel);
            settingPuzzle.Cnt = puzzleData.puzzleCnt;
        }

        //timeText = clearPanel.Find("MainPanel").Find("TimeText").GetComponent<TextMeshProUGUI>();//���ʷ�
        //starPanel = clearPanel.Find("MainPanel").Find("StarPanel").GetComponent<RectTransform>();//�� �޾Ƴ��� ��
        //stageBtn = clearPanel.Find("MainPanel").Find("StageBtn").GetComponent<Button>();//�������� ���ư���
        //nextBtn = clearPanel.Find("MainPanel").Find("NextBtn").GetComponent<Button>();//���� ��������
    }

    public void GameClear() //����Ŭ����
    {
        //_clearAmount++; // �̰� �ƴ϶� ClearAmount++; ��
        //gameData.clearAmount++;
        SceneManager.LoadScene("LoadingScene");
    }

    public void GameLose() //������
    {
        //_heart--;
        //TextAsset textAsset;
        //textAsset.
        //gameData.heart--;
        SceneManager.LoadScene("LoadingScene");
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
