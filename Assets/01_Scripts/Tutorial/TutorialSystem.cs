using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;

public class TutorialSystem : MonoBehaviour
{
    public List<string> tutorialList;
    public UnityEvent endEvent;

    [SerializeField] private Transform _uiContainerTrm;
    [SerializeField] private float _typingOneWordTime;
    [SerializeField] private float _textDuration;

    [SerializeField]     private CinemachineVirtualCamera _camera;
    private TextMeshProUGUI _descriptionTMP;
    private Image _talkBalloon;
    private int _idx;
    private string _description;

    private void Awake()
    {
        _descriptionTMP = _uiContainerTrm.Find("Description").GetComponent<TextMeshProUGUI>();
        _talkBalloon = _uiContainerTrm.Find("TalkBalloon").GetComponent<Image>();
    }

    private void Start()
    {
        StartText();
        _camera.transform.position = new Vector3(-6.45f, 1.35f, -10);
        _camera.m_Lens.OrthographicSize = 4f;
    }

    private void End()
    {
        endEvent?.Invoke();
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

        if(_idx < tutorialList.Count - 1)
        {
            _idx++;
            ShowDescription();
        }
        else
        {
            // end
            _talkBalloon.enabled = false;
            _camera.gameObject.SetActive(false);
            StageSystem.Instance.StartBatch();
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
