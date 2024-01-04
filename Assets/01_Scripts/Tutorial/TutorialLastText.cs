using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLastText : MonoBehaviour
{
    [SerializeField] private Transform _uiContainerTrm;
    [SerializeField] private float _typingOneWordTime;
    [SerializeField] private float _textDuration;
    [SerializeField] private string _text;

    private string _description;
    private TextMeshProUGUI _descriptionTMP;
    private Image _talkBalloon;

    private void Awake()
    {
        _descriptionTMP = _uiContainerTrm.Find("Description").GetComponent<TextMeshProUGUI>();
        _talkBalloon = _uiContainerTrm.Find("TalkBalloon").GetComponent<Image>();
    }

    public void StartText()
    {
        _description = _text;
        StartCoroutine(Typing());
        _talkBalloon.enabled = true;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Intro");
    }

    private IEnumerator Typing()
    {
        for (int i = 0; i < _description.Length; i++)
        {
            _descriptionTMP.text += _description[i];
            yield return new WaitForSeconds(_typingOneWordTime);
        }
        yield return new WaitForSeconds(_textDuration);
        LoadScene();
    }
}
