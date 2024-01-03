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
        if(_doorType == DoorType.Gold)
        {
            if (_player.GoldKey)
                Destroy(gameObject);
            else
                _playerHP.Damage();
        }
        else if(_doorType == DoorType.Shilver)
        {
            if (_player.SilverKey)
                Destroy(gameObject);
            else
                _playerHP.Damage();
        }
    }
}
