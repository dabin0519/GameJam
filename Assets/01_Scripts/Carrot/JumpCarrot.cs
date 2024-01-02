using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCarrot : Carrot
{
    [SerializeField] private float _jumpForce;

    protected override void CarrotAbility()
    {
        base.CarrotAbility();
        _player.Jump(_jumpForce);
    }
}