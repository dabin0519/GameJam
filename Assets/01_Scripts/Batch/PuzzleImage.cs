using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumList;

public class PuzzleImage : MonoBehaviour
{
    public Puzzle puzzle;
    public Image image { get; set; }
    public int cnt { get; set; }

    public PuzzleImage(Puzzle puzzle, Image image, int cnt)
    {
        this.puzzle = puzzle;
        this.image = image;
        this.cnt = cnt;
    }
}
