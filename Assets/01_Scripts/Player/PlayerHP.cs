using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private int _maxHP;
    [SerializeField] private GameObject _shield;

    private PlayerController _playerController;

    private int _hp;
    private bool _isShield = false;

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
            _playerController.Die();
        }
    }

    public void Damage(int damage = 1)
    {
        if (!_isShield)
            _hp -= damage;
        else
            Shield(false);
    }

    public void Shield(bool value)
    {
        _isShield = value;
        _shield.SetActive(value);
    }
}