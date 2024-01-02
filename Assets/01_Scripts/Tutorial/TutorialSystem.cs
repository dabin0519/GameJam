using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialType
{
    BtnClick,
    Batch,
    Normal
}

[System.Serializable]
public struct TutorialInfo
{
    public string Description;
    public TutorialType Type;
    public Transform BtnSpawnTrm;
    public TutorialButton BtnObj;
    public Transform BatchTrm;
    public Carrot BatchObj;
}

public class TutorialSystem : MonoBehaviour
{
    public List<TutorialInfo> tutorialList = new List<TutorialInfo>();

    [SerializeField] private Transform _uiContainerTrm;

    private TextMeshProUGUI _description;
    private TutorialInfo _currentTuto;
    private int _idx;

    private void Awake()
    {
        _description = _uiContainerTrm.Find("Description").GetComponent<TextMeshProUGUI>();
    }

    private void TutorialLogic()
    {
        ShowDescription();
        CheckType();
    }

    #region TutorialLogic
    private void ShowDescription()
    {
        _description.text = tutorialList[_idx].Description;
        _description.enabled = true;
    }

    private void CheckType()
    {
        if(tutorialList[_idx].Type == TutorialType.BtnClick)
        {
            // 버튼 클릭해야 넘어가게
            BtnClickLogic();
        }
        else if(tutorialList[_idx].Type == TutorialType.Batch)
        {
            // 오브젝트를 배치할 수 있게
            BatchLogic();
        }
        else if(tutorialList [_idx].Type == TutorialType.Normal)
        {
            // skip 가능하게
            NormalLogic();
        }
    }
    
    private void NextText()
    {

    }

    #endregion

    #region btnLogic
    private void BtnClickLogic()
    {
        //TutorialButton button = Instantiate(tuto.BtnObj, tuto.BtnSpawnTrm.position, Quaternion.identity);
        //StartCoroutine(BtnClickWaitCoroutine(button));
    }

    private IEnumerator BtnClickWaitCoroutine(TutorialButton button)
    {
        yield return new WaitUntil(() => button.IsClick = true);
        NextText();
    }
    #endregion

    private void BatchLogic()
    {

    }

    /*private IEnumerator CheckBatchCoroutine()
    {
        //yield return new WaitUntil(() => );
    }*/

    private void NormalLogic()
    {

    }
}
