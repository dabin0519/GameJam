using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumList;

[System.Serializable]
public struct PuzzleData
{
    public Puzzle puzzleType;
    public GameObject puzzleObj;
}

public class BatchManager : MonoBehaviour
{
    public static BatchManager Instance;//모노싱글톤에 리소스 비어있어서 일단은...

    [SerializeField] private List<PuzzleData> puzzleList = new List<PuzzleData>();

    private Dictionary<Puzzle, GameObject> puzzleDictionary = new Dictionary<Puzzle, GameObject>();
    private GameObject dragPuzzle;

    private void Awake()
    {
        Instance = this;

        foreach (PuzzleData puzzleData in puzzleList)
        {
            puzzleDictionary.Add(puzzleData.puzzleType, puzzleData.puzzleObj);
        }
    }

    public void PuzzleCreate(Vector2 batchPos, Puzzle puzzleEnum)
    {
        dragPuzzle = Instantiate(puzzleDictionary[puzzleEnum]);
        PuzzleMove(batchPos);
    }

    public void PuzzleMove(Vector2 batchPos)
    {
        if (dragPuzzle == null) return;

        dragPuzzle.transform.position = BatchTile.Instance.Vector2IntPos(batchPos);
    }

    public void PuzzleBatch()
    {
        if (dragPuzzle == null) return;

        if (BatchCheck.batchble)
        {
            dragPuzzle.GetComponent<BatchCheck>().enabled = false;
            dragPuzzle.GetComponent<Collider2D>().enabled = true;

            //이팩트 처리
        }
        else
        {
            Destroy(dragPuzzle);
        }
    }
}
