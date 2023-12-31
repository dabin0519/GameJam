using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using EnumList;

public class BatchUI : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Puzzle puzzle;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    public void OnDrag(PointerEventData eventData) //드래그중
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.PuzzleMove(mousePos);
    }

    public void OnPointerDown(PointerEventData eventData) //창 클릭
    {
        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<PuzzleImage>(out PuzzleImage puzzleImage))
        {
            puzzle = puzzleImage.puzzle;
        }
        else
        {
            Debug.Log("Not PuzzleImage");
            return;
        }

        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.PuzzleCreate(mousePos, puzzle);
    }

    public void OnPointerUp(PointerEventData eventData) //드래그 놓을때
    {
        BatchManager.Instance.PuzzleBatch();
    }
}
