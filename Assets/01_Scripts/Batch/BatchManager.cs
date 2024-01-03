using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumList;
using StructList;

public class BatchManager : MonoBehaviour
{
    public static BatchManager Instance;//모노싱글톤에 리소스 비어있어서 일단은...

    [SerializeField] private List<PuzzleType> puzzleList = new List<PuzzleType>();
    
    private GameObject dragPuzzle;
    private PuzzleImage dragImage;
    private PuzzleType puzzleType;

    private void Awake()
    {
        Instance = this;
    }

    public void PuzzleCreate(Vector2 batchPos, Puzzle puzzleEnum, PuzzleImage puzzleImage)
    {
        puzzleType = puzzleList.Find(p => p.puzzleType == puzzleEnum);
        dragPuzzle = Instantiate(puzzleType.puzzleObj);
        dragImage = puzzleImage;
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
            if (puzzleType.puzzleBatchType == BatchType.Obj)
            {
                BatchCheck batchCheck = dragPuzzle.GetComponent<BatchCheck>();
                batchCheck.BatchClear();
            }
            else
            {
                BatchTile.Instance.PlaseNewTileObj(dragPuzzle.transform.position, puzzleType.puzzleTile);
                Destroy(dragPuzzle);
            }

            dragImage.Cnt--;

            dragPuzzle = null;
            dragImage = null;

            //이팩트 처리
        }
        else
        {
            Destroy(dragPuzzle);
        }
    }
}
