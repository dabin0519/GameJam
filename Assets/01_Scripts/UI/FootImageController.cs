using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootImageController : MonoBehaviour
{
    [SerializeField] private GameObject _image;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        SetFootImage();
        //Debug.Log(_playerController.MoveCnt);
    }

    public void SetFootImage()
    {
        
    }
}
