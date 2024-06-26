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
        //_player.JumpPad = true;
        _anim.SetTrigger("Down");
        _player.Jump(_jumpForce);
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        _anim.SetTrigger("Up");
        //_player.JumpPad = false;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
