using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchCheck : MonoBehaviour
{
    private SpriteRenderer batchAreaRenderer;
    public static bool batchble = true;

    private void Awake()
    {
        batchAreaRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0f, LayerMask.GetMask("Puzzle"));
        Debug.Log($"{BatchTile.Instance.IsBatchObj(transform.position)}, {hit.collider}");
        if (BatchTile.Instance.IsBatchObj(transform.position) || hit.collider != null)//»¡°­
        {
            batchAreaRenderer.color = new Color(0.5f, 0f, 0);
            batchble = false;
        }
        else//¸ÖÂÄ
        {
            batchAreaRenderer.color = Color.white;
            batchble = true;
        }

    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Puzzle"))
    //    {
    //        batchble = false;
    //        batchAreaRenderer.color = new Color(0.5f, 0, 0);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Puzzle"))
    //    {
    //        batchble = true;
    //        batchAreaRenderer.color = new Color(0f, 0.5f, 0);
    //    }
    //}
}
