using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] private int _cnt;
    [SerializeField] private int _shuffleCnt;
    [SerializeField] private List<int> _stageList;
    private Queue<int> _queue;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform);
            _queue = new Queue<int>();
            _stageList = new List<int>();

            ListSettUp();
            Shuffle();
            EnQueue();
        }
    }

    private void ListSettUp()
    {
        for(int i = 1; i <= _cnt; i++)
        {
            _stageList.Add(i);
        }
    }

    private void EnQueue()
    {
        foreach(var i in _stageList)
        {
            _queue.Enqueue(i);
        }
    }

    private void Shuffle()
    {
        for(int i = 0; i <= _shuffleCnt; ++i)
        {
            int r1 = Random.Range(0, _stageList.Count);
            int r2 = Random.Range(0, _stageList.Count);
            (_stageList[r1], _stageList[r2]) = (_stageList[r2], _stageList[r1]);
        }

        Debug.Log("??");
    }

    public void LoadStage()
    {
        if(_queue.Count > 0)
        {
            string level = $"Stage{_queue.Dequeue()}Scene";
            SceneLoader.Instance.LoadScene(level);
        }
        else
        {
            SceneManager.LoadScene("GameoverScene");
        }
    }
}
