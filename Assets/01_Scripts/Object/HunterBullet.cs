using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterBullet : Carrot
{
    [SerializeField] private float _moveSpeed;

    public Vector3 Dir { get; set; }

    protected override void Update()
    {
        transform.position += Dir * _moveSpeed * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Destroy(gameObject);
    }

    protected override void CarrotAbility()
    {
        _playerHP.Damage();
    }
}
