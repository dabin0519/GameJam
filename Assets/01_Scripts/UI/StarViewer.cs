using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarViewer : MonoBehaviour, ISaveManager
{
    [SerializeField] private Image starImage;
    [SerializeField] private int stageIdx;

    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.star[stageIdx]; i++)
        {
            Instantiate(starImage, transform);
        }
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
