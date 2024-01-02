using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public int gold;
    public int exp;
    public int stage;
    public int[] star;

    public SerializableDictionary<string, int> skillTree;
    public SerializableDictionary<string, int> inventory;

    public GameData()
    {
        gold = 0;
        exp = 0;
        stage = 0;
        star = new int[31];//�ִ� ������������ + 1��ŭ �ʱ�ȭ
        skillTree = new SerializableDictionary<string, int>();
        inventory = new SerializableDictionary<string, int>();
    }
    // ��ų Ʈ�� ����
    // �κ��丮 ���� - �ȸ����. ��
}
