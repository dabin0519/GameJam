using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private Button _stateButton;
    [SerializeField] private PlayData _data;

    private void Start()
    {
        if (_data.userName == "")
        {
            PanelOn();
        }
        FirebaseManager.Instance.OnUserNameExists += ExistsHandle;
        FirebaseManager.Instance.OnUserNameNotExists += NotExistsHandle;
        FirebaseManager.Instance.OnLongUserName += LongUserNameHangle;
    }


    private void OnDisable()
    {
        FirebaseManager.Instance.OnUserNameExists -= ExistsHandle;
        FirebaseManager.Instance.OnUserNameNotExists -= NotExistsHandle;
        FirebaseManager.Instance.OnLongUserName -= LongUserNameHangle;
    }

    private void ExistsHandle()
    {
        Sequence seq = DOTween.Sequence();
        TextMeshProUGUI stateText = _stateButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        stateText.text = "�̹� �����ϴ� �̸�.";
        seq.Append(_stateButton.GetComponent<Image>().DOColor(new Color(1, 0.5f, 0.5f), 0.2f)).SetEase(Ease.InQuad);
        seq.Append(_stateButton.GetComponent<Image>().DOColor(Color.white, 0.7f)).SetEase(Ease.OutQuad);
        seq.OnComplete(() => stateText.text = "Ȯ��");
    }

    private void NotExistsHandle()
    {
        Sequence seq = DOTween.Sequence();
        TextMeshProUGUI stateText = _stateButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        stateText.text = "����!";
        _stateButton.interactable = false;
        seq.Append(_stateButton.GetComponent<Image>().DOColor(new Color(0.5f, 1, 0.5f), 0.2f)).SetEase(Ease.InQuad);
        seq.Append(_stateButton.GetComponent<Image>().DOColor(Color.white, 0.7f)).SetEase(Ease.OutQuad);
        _data.userName = _nameField.text;
        seq.OnComplete(() => PanelOff());
    }

    private void LongUserNameHangle()
    {
        Sequence seq = DOTween.Sequence();
        TextMeshProUGUI stateText = _stateButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        stateText.text = "�̸��� 2���� �̻�, 11���� ���Ͽ��� �ؿ�.";
        seq.Append(_stateButton.GetComponent<Image>().DOColor(new Color(1, 0.5f, 0.5f), 0.2f)).SetEase(Ease.InQuad);
        seq.Append(_stateButton.GetComponent<Image>().DOColor(Color.white, 0.7f)).SetEase(Ease.OutQuad);
        seq.OnComplete(() => stateText.text = "Ȯ��");
    }

    public void InputUser()
    {
        FirebaseManager.Instance.CheckUser(_nameField.text);
    }

    public void PanelOn()
    {
        RectTransform rt = transform.GetComponent<RectTransform>();
        rt.DOAnchorPosY(0, 0.3f).SetEase(Ease.OutQuad);
        _stateButton.interactable = true;
    }

    public void PanelOff()
    {
        RectTransform rt = transform.GetComponent<RectTransform>();
        rt.DOAnchorPosY(rt.sizeDelta.y, 0.3f).SetEase(Ease.OutQuad);
    }
}
