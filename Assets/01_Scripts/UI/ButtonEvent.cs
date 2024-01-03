using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer blackImage;

    public void EnableBtn(Behaviour behaviour, bool enable)
    {
        behaviour.enabled = enable;
    }

    public void ActiveBtn(GameObject obj, bool active)
    {
        obj.SetActive(active);
    }

    public void SceneLoadBtn(string sceneName)
    {

        float u = blackImage.material.GetFloat("_Value");
        DOTween.To(() => u, x => u = x, 0f, 1f).SetEase(Ease.OutCubic).OnUpdate(() =>
        {
            blackImage.material.SetFloat("_Value", u);
        }).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    public void OuitBtn()
    {
        Application.Quit();
    }
}