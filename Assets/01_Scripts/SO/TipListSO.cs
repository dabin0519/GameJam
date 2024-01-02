using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/TipList")]
public class TipListSO : ScriptableObject
{
    public List<string> tipList = new List<string>();
}
