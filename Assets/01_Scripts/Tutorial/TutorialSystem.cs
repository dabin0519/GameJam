using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;

public class TutorialSystem : MonoBehaviour
{
    public static TutorialSystem Instance;

    public List<string> tutorialList;
    public UnityEvent endEvent;

    [SerializeField] private Transform blackOutTrm;
    [SerializeField] private Transform _uiContainerTrm;
    [SerializeField] private float _typingOneWordTime;
    [SerializeField] private float _textDuration;
    [SerializeField] private Transform _camera;
    [SerializeField] private Animator _playerAnimator;

    public bool TextLogic;

    private TextMeshProUGUI _descriptionTMP;
    private Image _talkBalloon;
    private int _idx;
    private string _description;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _talkBalloon = _uiContainerTrm.Find("TalkBalloon").GetComponent<Image>();
        _descriptionTMP = _talkBalloon.transform.Find("Description").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _descriptionTMP.text = "";
        StartText();
        TextLogic = true;
        blackOutTrm.position = new Vector3(_camera.position.x, _camera.position.y);
    }

    public void End()
    {
        endEvent?.Invoke();
    }

    public void StartText()
    {
        ShowDescription();
        _idx = 0;
        _talkBalloon.enabled = true;
        _playerAnimator.SetBool("IsFront", true);
    }

    private bool _isOneCall;

    public void Skip()
    {
        Debug.Log("???");
        if(!_isOneCall)
        {
            _isOneCall = true;
            StopAllCoroutines();
            StartCoroutine(SkipCoroutine());
        }
    }

    private IEnumerator SkipCoroutine()
    {
        Debug.Log("skipCor");
        _descriptionTMP.text = _description;
        yield return new WaitForSeconds(_textDuration);
        _isOneCall = false;
        NextText();
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
            _playerAnimator.SetBool("IsFront", false);
            TextLogic = false;
            _talkBalloon.enabled = false;
            _camera.gameObject.SetActive(false);
            blackOutTrm.position = Vector3.zero;
            StageSystem.Instance.StartBatch();
        }
    }

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

    #endregion
}
