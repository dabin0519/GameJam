using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : Carrot
{
    private Animator _anim;

    protected override void Awake()
    {
        base.Awake();

        _anim = GetComponent<Animator>();
    }
}
