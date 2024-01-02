using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Key : Carrot
{
    [SerializeField] private EndFlag _endFlag;
    [SerializeField] private float _upValue;
    [SerializeField] private float _duration;

    protected override void Awake()
    {
        base.Awake();
        _endFlag.IsKey = true;
       // StartCoroutine(KeyAnimCoroutine());
    }

    protected override void CarrotAbility()
    {
        _endFlag.IsKey = false;
    }

    private IEnumerator KeyAnimCoroutine()
    {
        Vector3 playerPos = transform.position;
        while(true)
        {
            float y = playerPos.y + _upValue;
            transform.DOMoveY(y, _duration);
            yield return new WaitForSeconds(_duration);
            y = playerPos.y - _upValue;
            transform.DOMoveY(y, _duration);
            yield return new WaitForSeconds(_duration);
        }
    }
}
