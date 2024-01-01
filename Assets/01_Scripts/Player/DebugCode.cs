using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugCode : MonoBehaviour
{
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private float _changeJumpForce;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = _playerTrm.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _playerController.Jump(_changeJumpForce);
        }
    }
}
