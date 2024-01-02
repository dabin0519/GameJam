using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carrot : MonoBehaviour
{
    protected PlayerController _player;
    protected BatchCheck _batchCheck;
    protected PlayerHP _playerHP;
    protected Collider2D _collider;

    protected bool _isNotDestroy = false;

    protected virtual void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerHP = FindObjectOfType<PlayerHP>();
        _batchCheck = GetComponent<BatchCheck>();
        _collider = GetComponent<Collider2D>();

        _collider.enabled = false;
    }

    protected virtual void Update()
    {
        if(_batchCheck != null && _batchCheck.BatchClearPro)
        {
            _collider.enabled = true;
        }
    }

    protected abstract void CarrotAbility();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CarrotAbility();
            if(!_isNotDestroy)
                Destroy(gameObject);
        }
    }
}

