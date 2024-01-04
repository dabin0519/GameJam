using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchCheck : MonoBehaviour
{
    public static bool batchble = true;

    private SpriteRenderer batchAreaRenderer;
    private Collider2D col;

    [SerializeField] private bool batchClear;

    public bool BatchClearPro { get { return batchClear; } set { batchClear = value; } }

    private Color _currentColor;

    private void Awake()
    {
        batchAreaRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        _currentColor = batchAreaRenderer.color;
    }

    private void Update()
    {
        Building();
    }

    private void Building()
    {
        if (batchClear) return;

        Collider2D hit = Physics2D.OverlapBox(transform.position, new Vector3(0.5f, 0.5f, 0.5f), 0, LayerMask.GetMask("Puzzle", "Player"));
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0.5f, LayerMask.GetMask("Puzzle", "Player"));
        //Debug.Log($"{BatchTile.Instance.IsBatchObj(transform.position)}, {hit.collider}");
        if (BatchTile.Instance.IsBatchObj(transform.position) || hit != null)//»¡°­
        {
            batchAreaRenderer.color = new Color(1f, 0f, 0f, 0.7f);
            batchble = false;
        }
        else//¸ÖÂÄ
        {
            batchAreaRenderer.color = new Color(1f, 1f, 1f, 0.7f);
            batchble = true;
        }
    }

    public void BatchClear()
    {
        batchAreaRenderer.color = _currentColor;
        col.enabled = true;
        batchClear = true;
    }
}
