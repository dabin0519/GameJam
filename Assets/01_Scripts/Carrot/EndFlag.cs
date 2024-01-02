using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndFlag : Carrot
{
    public UnityEvent EndEvent;

    protected override void CarrotAbility()
    {
        _player.Active = false;
        EndEvent?.Invoke();
    }
}
