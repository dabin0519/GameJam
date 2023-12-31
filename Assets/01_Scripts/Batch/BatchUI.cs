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

    public void OnDrag(PointerEventData eventData) //�巡����
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        BatchManager.Instance.PuzzleMove(mousePos);
    }

    public void OnPointerDown(PointerEventData eventData) //â Ŭ��
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

    public void OnPointerUp(PointerEventData eventData) //�巡�� ������
    {
        BatchManager.Instance.PuzzleBatch();
    }
}
