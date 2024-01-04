using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : Carrot
{
    private Animator _anim;

    protected override void Awake()
    {
        base.Awake();

        _isNotDestroy = true;
        _anim = GetComponent<Animator>();
    }

    protected override void CarrotAbility()
    { 

    }
}
