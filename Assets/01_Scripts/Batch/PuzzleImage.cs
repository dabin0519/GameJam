using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EnumList;

public class PuzzleImage : MonoBehaviour
{
    public Puzzle puzzle;
    [HideInInspector]
    public int Cnt
    {
        get { return cnt; }
        set
        {
            cnt = value;
            TextValue(cnt);
        }
    }

    private TextMeshProUGUI amountText;
    private int cnt;

    private void Awake()
    {
        amountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void TextValue(int amount)
    {
        amountText.text = $"X{cnt}";
    }
}
