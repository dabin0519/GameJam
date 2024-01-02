using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IntroBackground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Tilemap tileground;

    private void Update()
    {
        background.size += Vector2.right * Time.deltaTime;
        tileground.tileAnchor += Vector3.left * 2 * Time.deltaTime;
        if (tileground.tileAnchor.x <= -12.5f)
        {
            tileground.tileAnchor = new Vector3(0.5f, 0.5f, 0);
        }
    }
}
