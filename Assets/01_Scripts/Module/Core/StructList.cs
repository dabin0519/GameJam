using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumList;

namespace StructList
{
    [System.Serializable]
    public struct PuzzleType
    {
        public Puzzle puzzleType;
        public GameObject puzzleObj;
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
