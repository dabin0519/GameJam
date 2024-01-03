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
        Debug.Log(_playerController.Count);
    }

    public void SetFootImage()
    {
        int amount = (9 - _playerController.Count) - transform.childCount;
        if(amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject go = Instantiate(_image, transform);
                go.GetComponent<SpriteRenderer>().DOFade(1, 0.2f);
            }
        }
        else if(amount < 0)
        {
            for (int i = 0; i < Mathf.Abs(amount); i++)
            {
                GameObject go = transform.GetChild(0).gameObject;
                Destroy(go);
            }
        }
    }
}
