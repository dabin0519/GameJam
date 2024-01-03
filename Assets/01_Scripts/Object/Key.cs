using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum KeyType
{
    Gold,
    Shilver
}

public class Key : Carrot
{
    [SerializeField] private float _upValue;
    [SerializeField] private float _duration;
    [SerializeField] private KeyType _type;

    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(KeyAnimCoroutine());
    }

    protected override void CarrotAbility()
    {
        if(_type == KeyType.Gold)
        {
            _player.GoldKey = true;
        }
        else if(_type == KeyType.Shilver)
        {
            _player.SilverKey = true;
        }
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
