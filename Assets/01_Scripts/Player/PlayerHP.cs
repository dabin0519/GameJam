using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour
{
    public event Action DieEvent;

    [SerializeField] private int _maxHP;
    [SerializeField] private GameObject _shield;
    [SerializeField] private float _dontDieDuration;

    private PlayerController _playerController;

    private int _hp;
    private bool _isShield = false;
    private bool _isDead = false;
    private bool _noDie;

    public int HP { get { return _hp; } set { _hp = value; } }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();

        _hp = _maxHP;
    }

    private void Update()
    {
        if(_hp <= 0 && !_isDead)
        {
            _isDead = true;
            _playerController.Die();
            DieEvent?.Invoke();
        }
    }

    public void Damage(int damage = 1)
    {
        Debug.Log($"Damaged : {damage}");
        /*if (_noDie)
            return;*/

        if (!_isShield)
            _hp -= damage;
        else
            Shield(false);
    }

    public void Shield(bool value)
    {
        _isShield = value;
        Debug.Log("shield");
        _shield.SetActive(value);

        if(value)
        {
            StartCoroutine(ShieldCoroutine());
        }
    }

    private IEnumerator ShieldCoroutine()
    {
        _noDie = true;
        yield return new WaitForSeconds(_dontDieDuration);
        _noDie = false;
    }
}
