using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectRabbit : MonoBehaviour, ISaveManager
{
    [SerializeField] private Image starImage;
    [SerializeField] private float _moveSpeed = 5f;

    [SerializeField] private List<Transform> _roadPoints;
    private List<Transform> _stagePoints = new List<Transform>();

    private GameData gameData;
    private int _roadIdx;

    private void Start()
    {
        foreach (var point in _roadPoints)
        {
            if (point.GetComponent<ButtonController>() != null)
            {
                _stagePoints.Add(point);
                Debug.Log(point.name);
            }
        }

        _roadIdx = 0;
        //_roadIdx = 마지막으로 클리어한 스테이지;
    }

    public void SetRabbitMove(int stageIdx)
    {
        StopAllCoroutines();
        for (int i = 0; i < _roadPoints.Count; i++)
        {
            if (_roadPoints[i] == _stagePoints[stageIdx - 1])
            {
                StartCoroutine(MoveRabbit(i));
                break;
            }
        }
    }

    private IEnumerator MoveRabbit(int destinationIdx)
    {
        while (true)
        {
            Vector2 destination;
            if (_roadPoints[_roadIdx].GetComponent<ButtonController>() != null)
            {
                destination = Camera.main.ScreenToWorldPoint(_roadPoints[_roadIdx].position);
            }
            else
            {
                destination = _roadPoints[_roadIdx].position;
            }

            destination.y += 0.2f;

            transform.position = Vector2.MoveTowards(transform.position,
               destination, _moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, destination) < 0.05f)
            {
                if (_roadIdx < destinationIdx)
                    _roadIdx++;
                else if (_roadIdx > destinationIdx)
                    _roadIdx--;
                else
                {
                    Debug.Log("도착");
                    break;
                }
            }
            yield return null;
        }
    }

    public void LoadData(GameData data)
    {
        gameData = data;
        
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
