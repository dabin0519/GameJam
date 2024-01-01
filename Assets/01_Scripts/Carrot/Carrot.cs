using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carrot : MonoBehaviour
{
    protected PlayerController _player;
    protected PlayerHP _playerHP;

    protected virtual void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _playerHP = FindObjectOfType<PlayerHP>();
    }

    protected abstract void CarrotAbility();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CarrotAbility();
            Destroy(gameObject);
        }
    }
}

