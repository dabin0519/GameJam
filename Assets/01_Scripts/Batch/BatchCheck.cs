using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchCheck : MonoBehaviour
{
    public static bool batchble = true;

    private SpriteRenderer batchAreaRenderer;
    private Collider2D col;

    [SerializeField] private bool batchClear;

    private void Awake()
    {
        batchAreaRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Building();
    }

    private void Building()
    {
        if (batchClear) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0f, LayerMask.GetMask("Puzzle"));
        Debug.Log($"{BatchTile.Instance.IsBatchObj(transform.position)}, {hit.collider}");
        if (BatchTile.Instance.IsBatchObj(transform.position) || hit.collider != null)//»¡°­
        {
            batchAreaRenderer.color = new Color(0.5f, 0f, 0f, 0.5f);
            batchble = false;
        }
        else//¸ÖÂÄ
        {
            batchAreaRenderer.color = new Color(0f, 0f, 0f, 0.5f);
            batchble = true;
        }
    }

    public void BatchClear()
    {
        batchAreaRenderer.color = Color.white;
        col.enabled = true;
        batchClear = true;
    }
}
