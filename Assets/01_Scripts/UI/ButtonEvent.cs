using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer blackImage;
    [SerializeField] private GameObject[] disActiveObjs;

    private void Start()
    {
        blackImage.material.SetFloat("_Value", 0);
        float u = blackImage.material.GetFloat("_Value");
        DOTween.To(() => u, x => u = x, 1f, 1f).SetEase(Ease.OutCubic).OnUpdate(() =>
        {
            blackImage.material.SetFloat("_Value", u);
        });
    }

    public void EnableBtn(Behaviour behaviour, bool enable)
    {
        behaviour.enabled = enable;
    }

    public void ActiveBtn(GameObject obj, bool active)
    {
        obj.SetActive(active);
    }

    public void SceneLoad(string sceneName)
    {
        if (disActiveObjs.Length != 0)
        {
            foreach (GameObject obj in disActiveObjs)
                obj.SetActive(false);
        }

        float u = blackImage.material.GetFloat("_Value");
        DOTween.To(() => u, x => u = x, 0f, 1.5f).SetEase(Ease.OutCubic).OnUpdate(() =>
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
