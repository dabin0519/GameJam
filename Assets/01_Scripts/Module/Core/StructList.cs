using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using EnumList;

namespace StructList
{
    

    [System.Serializable]
    public struct PuzzleType
    {
        public Puzzle puzzleType;
        public BatchType puzzleBatchType;
        public GameObject puzzleObj;
        public TileBase puzzleTile;
    }

    [System.Serializable]
    public struct PuzzleData
    {
        public Puzzle puzzleType;
        public int puzzleCnt;
    }

    [System.Serializable]
    public struct StageData
    {
        public int playTime;
        public GameObject map;
        public List<PuzzleData> puzzleDatas;
    }
}
