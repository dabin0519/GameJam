using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHole : Carrot
{
    public RabbitHole targetRabbitHole;
    [SerializeField] private float _duration;

    public bool IsIn;

    private RabbitHole[] _rabbitHoles;

    protected override void Awake()
    {
        base.Awake();
        _isNotDestroy = true;

        if(_batchCheck != null)
        {
            _rabbitHoles = FindObjectsOfType<RabbitHole>();
            foreach(var r in _rabbitHoles)
            {
                if(this != r)
                {
                    targetRabbitHole = r;
                    r.targetRabbitHole = this;
                }
            }
        }
    }

    protected override void CarrotAbility()
    {
        if(IsIn)
        {
            StartCoroutine(WaitCoroutine());
            return;
        }
        _player.transform.position = targetRabbitHole.transform.position;
        targetRabbitHole.IsIn = true;
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(_duration);
        IsIn = false;
    }
}
