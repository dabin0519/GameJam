using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : Carrot
{
    [SerializeField] private Transform _startTrm;

    protected override void Awake()
    {
        base.Awake();
        _player.transform.position = transform.position;
        //_player.transform.position = _startTrm.position;
    }

    protected override void CarrotAbility()
    {
        //Do nothing
    }
}
