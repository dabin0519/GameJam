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

    public event Action OnStartEvt; //실행시킬때
    public event Action OnStopEvt; //일시정지할때
    public event Action OnClearEvt; //게임 클리어시
    [HideInInspector] public bool IsPlay { get; set; }

    [Header("리스트")]
    [SerializeField] private List<PuzzleImage> puzzleImages;
    [SerializeField] private List<StageData> stageData;

    [Header("외부참조")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Transform settingPanel;
    [SerializeField] private Transform clearPanel;

    [Header("프리팹")]
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
        Instantiate(stageData[currentStage].map); //맵 생성

        foreach (PuzzleData puzzleData in stageData[currentStage].puzzleDatas) //퍼즐 생성
        {
            //PuzzleImage settingPuzzle = puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType);
            PuzzleImage settingPuzzle = Instantiate(puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType), settingPanel);
            settingPuzzle.Cnt = puzzleData.puzzleCnt;
        }

        playBtn.onClick.AddListener(PlayBtn);//실행버튼 함수넣고
        OnClearEvt += GameClear;//클리어시 창 뜨게

        timeText = clearPanel.Find("MainPanel").Find("TimeText").GetComponent<TextMeshProUGUI>();//분초로
        starPanel = clearPanel.Find("MainPanel").Find("StarPanel").GetComponent<RectTransform>();//별 달아놓는 곳
        stageBtn = clearPanel.Find("MainPanel").Find("StageBtn").GetComponent<Button>();//스테이지 돌아가기
        nextBtn = clearPanel.Find("MainPanel").Find("NextBtn").GetComponent<Button>();//다음 스테이지
    }

    public void PlusStar()
    {
        currentStar++;
        Instantiate(starImage, starPanel);
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

        string second = TimeSpan.FromSeconds(currentPlayTime).ToString("mm\\:ss");
        var time = second.Split(":");
        timeText.text = $"{time[0]}m {time[1]}s";

        //창뜨기(걸린시간, 별의 양, 다음스테이지 버튼, 스테이지 선택 버튼)
        clearPanel.DOMoveY(0, 0.5f).SetEase(Ease.InOutBounce)
            .OnComplete(() => {
                stageBtn.onClick.AddListener(() => { GameOut(); });
                nextBtn.onClick.AddListener(() => { NextGame(-1);/*인자로 현재 스테이지 + 1 넣기*/});
            });
    }

    private void NextGame(int nextStage) //스테이지 선택 시 메계변수X
    {
        SaveData(ref gameData);
        SceneManager.LoadScene("");//씬이동
    }

    private void GameOut()
    {
        SaveData(ref gameData);
        SceneManager.LoadScene("");//씬이동
    }

    public void LoadData(GameData data) //스테이지 불러오기(Start에서 호출)
    {
        gameData = data;
        currentStage = data.stage;
        LoadStage();
    }

    public void SaveData(ref GameData data) //다음 스테이지, 별 저장
    {
        //현재 스테이지에서 먹은 별개수 저장하고
        if (currentStar > data.star[currentStage])
        {
            data.star[currentStage] = currentStar;
        }
        //현재 스테이지를 nextStage로 설정
        data.stage = currentStage + 1;
    }
}
