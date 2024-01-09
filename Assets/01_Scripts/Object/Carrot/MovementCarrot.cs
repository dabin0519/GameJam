using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCarrot : Carrot
{
    [SerializeField] private int _moveCount;

    protected override void CarrotAbility()
    {
        ItemEvent?.Invoke();
        _player.Movement(_moveCount);
    }
}
