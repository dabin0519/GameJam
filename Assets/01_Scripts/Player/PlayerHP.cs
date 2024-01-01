using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int _maxHP;

    private PlayerController _playerController;
    private int _hp;
    private bool _shield = false;

    public bool Shield { get { return _shield; } set { _shield = value; } }
    public int HP { get { return _hp; } set { _hp = value; } }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();

        _hp = _maxHP;
    }

    private void Update()
    {
        if(_hp <= 0)
        {
            _playerController.Active = false;
        }
    }

    public void Damage(int damage = 1)
    {
        if(!_shield)
            _hp -= damage;
    }
}
