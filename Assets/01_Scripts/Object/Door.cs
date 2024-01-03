using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    Gold,
    Shilver
}

public class Door : Carrot
{
    [SerializeField] private DoorType _doorType;

    protected override void Awake()
    {
        base.Awake();
        _isNotDestroy = true;
    }

    protected override void CarrotAbility()
    {
        if(_doorType == DoorType.Gold && _player.GoldKey)
        {
            Destroy(gameObject);
        }
        else if(_doorType == DoorType.Shilver && _player.SilverKey)
        {
            Destroy(gameObject);
        }
    }
}
