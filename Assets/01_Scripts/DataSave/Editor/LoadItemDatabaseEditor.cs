using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
public class LoadItemDatabaseEditor : Editor
{
    private SaveManager _saveManager;
    private string _soFileName = "ItemDB";

    private void OnEnable()
    {
        _saveManager = (SaveManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate item DB"))
        {
            CreateItemDBAsset();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    private void CreateItemDBAsset()
    {
        /*List<ItemDataSO> loadedList = new();

        string[] assetIDArray = AssetDatabase.FindAssets("", new[] { "Assets/08_SO/Items" });

        foreach(string assetID in assetIDArray)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetID);
            ItemDataSO item = AssetDatabase.LoadAssetAtPath<ItemDataSO>(assetPath);

            if(item != null)
            {
                loadedList.Add(item);
            }
        }

        string dpPath = $"Assets/08_SO/{_soFileName}.asset";
        ItemDataBaseSO itemDB = AssetDatabase.LoadAssetAtPath<ItemDataBaseSO>(dpPath);

        if(itemDB == null)
        {
            // ���� so�� ����
            itemDB = ScriptableObject.CreateInstance<ItemDataBaseSO>(); // �޸𸮿��� ������.
            itemDB.itemList = loadedList;
            string realPath = AssetDatabase.GenerateUniqueAssetPath(dpPath);
            AssetDatabase.CreateAsset(itemDB, realPath);

            Debug.Log($"item db created at {dpPath}");
        }
        else
        {
            // �����͸� ��ġ�� �ȴ�.
            itemDB.itemList = loadedList;
            EditorUtility.SetDirty(itemDB); // �� ������!
        }*/
    }
}
