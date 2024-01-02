using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int gold;
    public int exp;
    public int stage;
    public int clearAmount;
    public int heart;

    public SerializableDictionary<string, int> skillTree;
    public SerializableDictionary<string, int> inventory;

    public GameData()
    {
        gold = 0;
        exp = 0;
        stage = 0;
        clearAmount = 0;
        heart = 0;
        skillTree = new SerializableDictionary<string, int>();
        inventory = new SerializableDictionary<string, int>();
    }
    // 스킬 트리 저장
    // 인벤토리 저장 - 안만든다. ㅠ
}
