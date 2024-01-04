using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public class Shaking : MonoBehaviour
{
    public UnityEvent EndEvent;

    [SerializeField] GameObject gm;
    [SerializeField] GameObject gm1;
    [SerializeField] GameObject carrot;
    private void Start()
    {
        StartCoroutine(ShakingPos());
        StartCoroutine(ShakingPos2());
        StartCoroutine(ShakingPos3());
    }
    IEnumerator ShakingPos()
    {
        gm.transform.DOMoveY(1.55f, 1).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(1f);
        gm.transform.DOShakePosition(0.5f, 3, 10);
    }
    IEnumerator ShakingPos2()
    {
        yield return new WaitForSeconds(1f);
        gm1.transform.DOMoveX(0f, 1).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(1f);
        gm1.transform.DOShakePosition(0.5f, 1, 10);

        yield return new WaitForSeconds(1f);

    }
    IEnumerator ShakingPos3()
    {
        yield return new WaitForSeconds(1.7f);
        carrot.transform.DOMoveX(3.3f, 1).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(1f);
        carrot.transform.DOShakeRotation(2.5f,45,5);
        yield return new WaitForSeconds(1.5f);
        EndEvent?.Invoke();
    }
}
