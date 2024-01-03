using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseKey : MonoBehaviour
{
    public static bool isKey = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isKey = true;
            gameObject.SetActive(false);
        }
    }
}
