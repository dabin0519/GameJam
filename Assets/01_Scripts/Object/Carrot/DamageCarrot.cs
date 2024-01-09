using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCarrot : Carrot
{
    private bool _isOneCall;

    protected override void CarrotAbility()
    {
        ItemEvent?.Invoke();
        _playerHP.Damage();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isOneCall)
        {
            CarrotAbility();
            _isOneCall = true;
            Destroy(gameObject);
        }
    }
}
