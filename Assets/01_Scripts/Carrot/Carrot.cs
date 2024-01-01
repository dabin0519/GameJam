using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carrot : MonoBehaviour
{
    protected PlayerController _player;

    protected virtual void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
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

