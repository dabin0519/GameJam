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

    public event Action OnStartEvt; //실행시킬때
    public event Action OnStopEvt; //일시정지할때
    public event Action OnClearEvt; //게임 클리어시
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

        Instantiate(stageData[currentStage].map); //맵 생성

        foreach (PuzzleData puzzleData in stageData[currentStage].puzzleDatas) //퍼즐 생성
        {
            //PuzzleImage settingPuzzle = puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType);
            PuzzleImage settingPuzzle = Instantiate(puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType), settingPanel);
            settingPuzzle.Cnt = puzzleData.puzzleCnt;
        }

        playBtn.onClick.AddListener(PlayBtn);//실행버튼 함수넣고
        OnClearEvt += GameClear;//클리어시 창 뜨게

        timeText = clearPanel.Find("TimeText").GetComponent<TextMeshProUGUI>();//분초로
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

    private void PlayBtn() //버튼 누룰때
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

    private void GameClear() //OnClearEvt도착지에서 ?.Invoke() 해주면 완료
    {
        OnStopEvt?.Invoke(); //플레이어 멈추고
        //창뜨기(걸린시간, 별의 양, 다음스테이지 버튼, 스테이지 선택 버튼)
    }
}
