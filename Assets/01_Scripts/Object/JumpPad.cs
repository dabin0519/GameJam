using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : Carrot
{
    [SerializeField] private float _upDuration;
    [SerializeField] private float _jumpForce;
    private Animator _anim;

    protected override void Awake()
    {
        base.Awake();

        _isNotDestroy = true;
        _anim = GetComponent<Animator>();
    }

    protected override void CarrotAbility()
    {
        _player.JumpPad = true;
        _anim.SetTrigger("Down");
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(_upDuration);
        _anim.SetTrigger("Up");
        _player.Jump(_jumpForce);
        _player.JumpPad = false;
    }
}
