using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Carrot
{
    [SerializeField] private float _upDuration;
    [SerializeField] private float _downDuration;

    private Animator _anim;
    private bool _isUp;

    protected override void Awake()
    {
        base.Awake();
        _anim = transform.Find("Visual").GetComponent<Animator>();
        StartCoroutine(AnimCoroutine());
        _isNotDestroy = true;
    }

    protected override void CarrotAbility()
    {
        if(_isUp)
        {
            _playerHP.Damage();
        }
    }

    private IEnumerator AnimCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_upDuration);
            _anim.SetBool("IsUP", true);
            _isUp = true;
            yield return new WaitForSeconds(_downDuration);
            _anim.SetBool("IsUP", false);
            _isUp = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            CarrotAbility();
        }
    }
}
