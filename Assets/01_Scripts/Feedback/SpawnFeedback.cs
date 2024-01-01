using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFeedback : Feedback
{
    [SerializeField] private GameObject _spawnObj;
    [SerializeField] private float _duration;

    public override void CreateFeedback()
    {
        GameObject newGameObject = Instantiate(_spawnObj, transform.position, Quaternion.identity);
        Destroy(newGameObject, _duration);
    }

    public override void FinishFeedback()
    {
        
    }
}
