using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructList;
using UnityEngine.UI;
using System;
using TMPro;

public class StageSystem : MonoBehaviour
{
    public static StageSystem Instance;

    public event Action OnStartEvt; //�����ų��
    public event Action OnStopEvt; //�Ͻ������Ҷ�
    public event Action OnClearEvt; //���� Ŭ�����
    [HideInInspector] public bool IsPlay { get; set; }

    [SerializeField] private List<PuzzleImage> puzzleImages;
    [SerializeField] private List<StageData> stageData;

    [SerializeField] private Button playBtn;
    [SerializeField] private Transform settingPanel;
    [SerializeField] private Transform clearPanel;

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

        Instantiate(stageData[currentStage].map); //�� ����

        foreach (PuzzleData puzzleData in stageData[currentStage].puzzleDatas) //���� ����
        {
            //PuzzleImage settingPuzzle = puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType);
            PuzzleImage settingPuzzle = Instantiate(puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType), settingPanel);
            settingPuzzle.Cnt = puzzleData.puzzleCnt;
        }

        playBtn.onClick.AddListener(PlayBtn);//�����ư �Լ��ְ�
        OnClearEvt += GameClear;//Ŭ����� â �߰�

        timeText = clearPanel.Find("TimeText").GetComponent<TextMeshProUGUI>();//���ʷ�
        starPanel = clearPanel.Find("StarPanel").GetComponent<RectTransform>();
        stageBtn = clearPanel.Find("StageBtn").GetComponent<Button>();
        nextBtn = clearPanel.Find("NextBtn").GetComponent<Button>();
    }

    private void Update()
    {
        if (IsPlay)
        {
            currentPlayTime += Time.deltaTime;
        }
    }

    public void PlusStar()
    {
        currentStar++;
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
        //â�߱�(�ɸ��ð�, ���� ��, ������������ ��ư, �������� ���� ��ư)
    }
}
