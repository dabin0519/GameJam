using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCarrot : Carrot
{
    protected override void CarrotAbility()
    {
        _playerHP.Damage();
    }
}
