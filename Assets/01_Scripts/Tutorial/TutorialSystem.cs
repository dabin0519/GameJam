using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    public List<string> tutorialList;

    [SerializeField] private Transform _uiContainerTrm;
    [SerializeField] private float _typingOneWordTime;
    [SerializeField] private float _textDuration;

    private TextMeshProUGUI _descriptionTMP;
    private Image _talkBalloon;
    private int _idx;
    private string _description;

    private void Awake()
    {
        _descriptionTMP = _uiContainerTrm.Find("Description").GetComponent<TextMeshProUGUI>();
        _talkBalloon = _uiContainerTrm.Find("TalkBalloon").GetComponent<Image>();
    }

    public void StartText()
    {
        ShowDescription();
        _idx = 0;
        _talkBalloon.enabled = true;
    }

    #region TutorialLogic
    private void ShowDescription()
    {
        _description = tutorialList[_idx];
        StartCoroutine(Typing());
    }
    
    private void NextText()
    {
        _descriptionTMP.text = "";

        if(_idx < tutorialList.Count)
        {
            _idx++;
            ShowDescription();
        }
        else
        {
            // end
            _talkBalloon.enabled = false;
        }
    }

    #endregion

    private IEnumerator Typing()
    {
        for (int i = 0; i < _description.Length; i++)
        {
            _descriptionTMP.text += _description[i];
            yield return new WaitForSeconds(_typingOneWordTime);
        }
        yield return new WaitForSeconds(_textDuration);
        NextText();
    }
}
