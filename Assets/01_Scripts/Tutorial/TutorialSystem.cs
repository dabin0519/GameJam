using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialType
{
    BtnClick,
    Betch,
    Normal
}

[System.Serializable]
public struct TutorialInfo
{
    public string Description;
    public TutorialType Type;
}

public class TutorialSystem : MonoBehaviour
{
    
}
