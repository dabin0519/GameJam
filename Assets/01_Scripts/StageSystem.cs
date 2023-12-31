using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StructList;
using System.Linq;

public class StageSystem : MonoBehaviour
{
    [SerializeField] private List<PuzzleImage> puzzleImages;
    [SerializeField] private List<StageData> stageData;

    [SerializeField] private GameObject settingPanel;

    int currentStage;

    private void Awake()
    {
        Instantiate(stageData[currentStage].map); //�� ����

        foreach (PuzzleData puzzleData in stageData[currentStage].puzzleDatas) //���� ����
        {
            PuzzleImage settingPuzzle = puzzleImages.Find(p => p.puzzle == puzzleData.puzzleType);
            settingPuzzle.cnt = puzzleData.puzzleCnt;
            Instantiate(settingPuzzle, settingPanel.transform);
        }
    }
}
