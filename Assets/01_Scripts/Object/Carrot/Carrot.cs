using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Carrot : MonoBehaviour
{
    protected PlayerController _player;
    protected BatchCheck _batchCheck;
    protected PlayerHP _playerHP;
    protected Collider2D _collider;

    protected bool _isNotDestroy = false;
    private bool _isOneCall;

    public UnityEvent ItemEvent;

    protected virtual void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerHP = FindObjectOfType<PlayerHP>();
        _batchCheck = GetComponent<BatchCheck>();
        _collider = GetComponent<Collider2D>();

        if(_batchCheck != null)
            _collider.enabled = false;
    }

    protected virtual void Update()
    {
        if(_batchCheck != null && _batchCheck.BatchClearPro)
        {
            _collider.enabled = true;
        }
    }

    protected virtual void CarrotAbility()
    {
        ItemEvent?.Invoke();
        _player.Movement(9);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isOneCall)
        {
            if(!_isNotDestroy)
                Destroy(gameObject);
            CarrotAbility();
            _isOneCall = true;
        }
    }
}

