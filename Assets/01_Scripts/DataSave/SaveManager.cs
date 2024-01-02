using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    [SerializeField] private string _fileName;

    private GameData _gameData;
    private List<ISaveManager> _saveManagerList;
    private FileDataHandler _fileDataHandler;

    [SerializeField] private bool _isEncrypt;

    [SerializeField] private SerializableDictionary<string, int> _testDic;
    [SerializeField] private bool _isBase64;

    private void Start()
    {
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _isEncrypt, _isBase64);
        _saveManagerList = FindAllSaveManagers();

        LoadGame();

        _testDic = new();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _fileDataHandler.Load();
        if(_gameData == null)
        {
            Debug.Log("no save data found");
            NewGame();
        }

        foreach(var saveManager in _saveManagerList)
        {
            saveManager.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var saveManager in _saveManagerList)
        {
            saveManager.SaveData(ref _gameData);
        }

        _fileDataHandler.Save(_gameData);
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        return FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>().ToList();
    }

    [ContextMenu("Delete save file")]
    public void DeleteSaveData()
    {
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _isEncrypt, _isBase64);
        _fileDataHandler.DeleteSaveData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
