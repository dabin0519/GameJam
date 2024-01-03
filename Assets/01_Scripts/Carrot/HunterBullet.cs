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

    protected override void CarrotAbility()
    {
        _playerHP.Damage();
    }
}
