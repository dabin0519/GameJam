using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseCarrot : Carrot
{
    protected override void CarrotAbility()
    {
        Vector3 player = _player.MoveDir;
        _player.MoveDir = new Vector3(-player.x, player.y);
    }
}
