using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EndFlag : Carrot
{
    public event Action EndEvent;
    public bool IsKey;

    protected override void CarrotAbility()
    {
        _player.Active = false;
        EndEvent?.Invoke();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsKey)
            return;
        base.OnTriggerEnter2D(collision);
    }
}
