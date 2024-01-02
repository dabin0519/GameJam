using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructList;
using UnityEngine.UI;
using System;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StageSystem : MonoBehaviour, ISaveManager
{
    public static StageSystem Instance;

    public event Action OnStartEvt; //�����ų��
    public event Action OnStopEvt; //�Ͻ������Ҷ�
    public event Action OnClearEvt; //���� Ŭ�����
    [HideInInspector] public bool IsPlay { get; set; }

    [Header("����Ʈ")]
    [SerializeField] private List<PuzzleImage> puzzleImages;
    [SerializeField] private List<StageData> stageData;

    [Header("�ܺ�����")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Transform settingPanel;
    [SerializeField] private Transform clearPanel;

    [Header("������")]
    [SerializeField] private Image starImage;

    private GameData gameData;

    private TextMeshProUGUI timeText;
    private RectTransform starPanel;
    private Button stageBtn;
    private Button nextBtn;

    private int currentStage;
    private float currentPlayTime = 0f;
    private int currentStar;

    private void Awake()
    {
        Instance = this;
        LoadStage();
    }

    private void Update()
    {
        if (IsPlay)
        {
            currentPlayTime += Time.deltaTime;
        }
    }

    private void LoadStage()
    {
        Instantiate(stageData[currentStage].map); //�� ����

        foreach (PuzzleData puzzleData in stageData[currentStage].puzzleDatas) //���� ����
        {
            //PuzzleImage settingPuzzle = puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType);
            PuzzleImage settingPuzzle = Instantiate(puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType), settingPanel);
            settingPuzzle.Cnt = puzzleData.puzzleCnt;
        }

        playBtn.onClick.AddListener(PlayBtn);//�����ư �Լ��ְ�
        OnClearEvt += GameClear;//Ŭ����� â �߰�

        timeText = clearPanel.Find("MainPanel").Find("TimeText").GetComponent<TextMeshProUGUI>();//���ʷ�
        starPanel = clearPanel.Find("MainPanel").Find("StarPanel").GetComponent<RectTransform>();//�� �޾Ƴ��� ��
        stageBtn = clearPanel.Find("MainPanel").Find("StageBtn").GetComponent<Button>();//�������� ���ư���
        nextBtn = clearPanel.Find("MainPanel").Find("NextBtn").GetComponent<Button>();//���� ��������
    }

    public void PlusStar()
    {
        currentStar++;
        Instantiate(starImage, starPanel);
    }

    private void PlayBtn() //��ư ���궧
    {
        IsPlay = !IsPlay;

        if (IsPlay)
        {
            OnStartEvt?.Invoke();
        }
        else
        {
            OnStopEvt?.Invoke();
        }
    }

    private void GameClear() //OnClearEvt���������� ?.Invoke() ���ָ� �Ϸ�
    {
        OnStopEvt?.Invoke(); //�÷��̾� ���߰�

        string second = TimeSpan.FromSeconds(currentPlayTime).ToString("mm\\:ss");
        var time = second.Split(":");
        timeText.text = $"{time[0]}m {time[1]}s";

        //â�߱�(�ɸ��ð�, ���� ��, ������������ ��ư, �������� ���� ��ư)
        clearPanel.DOMoveY(0, 0.5f).SetEase(Ease.InOutBounce)
            .OnComplete(() => {
                stageBtn.onClick.AddListener(() => { GameOut(); });
                nextBtn.onClick.AddListener(() => { NextGame(-1);/*���ڷ� ���� �������� + 1 �ֱ�*/});
            });
    }

    private void NextGame(int nextStage) //�������� ���� �� �ް躯��X
    {
        SaveData(ref gameData);
        SceneManager.LoadScene("");//���̵�
    }

    private void GameOut()
    {
        SaveData(ref gameData);
        SceneManager.LoadScene("");//���̵�
    }

    public void LoadData(GameData data) //�������� �ҷ�����(Start���� ȣ��)
    {
        gameData = data;
        currentStage = data.stage;
        LoadStage();
    }

    public void SaveData(ref GameData data) //���� ��������, �� ����
    {
        //���� ������������ ���� ������ �����ϰ�
        if (currentStar > data.star[currentStage])
        {
            data.star[currentStage] = currentStar;
        }
        //���� ���������� nextStage�� ����
        data.stage = currentStage + 1;
    }
}
