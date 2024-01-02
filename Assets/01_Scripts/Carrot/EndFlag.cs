using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndFlag : Carrot
{
    public UnityEvent EndEvent;
    public bool IsKey;

    protected override void CarrotAbility()
    {
        if (IsKey)
            return;
        _player.Active = false;
        EndEvent?.Invoke();
    }
}
