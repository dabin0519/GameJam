using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : Carrot
{
    [SerializeField] private RabbitHole _targetRabbitHole;
    [SerializeField] private float _duration;

    public bool IsIn;

    protected override void Awake()
    {
        base.Awake();
        _isNotDestroy = true;
    }

    protected override void CarrotAbility()
    {
        if(IsIn)
        {
            StartCoroutine(WaitCoroutine());
            return;
        }
        _player.transform.position = _targetRabbitHole.transform.position;
        _targetRabbitHole.IsIn = true;
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(_duration);
        IsIn = false;
    }
}
