using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCarrot : Carrot
{
    protected override void CarrotAbility()
    {
        _playerHP.Shield(true);
    }
}
