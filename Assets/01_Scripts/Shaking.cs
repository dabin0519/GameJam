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
    [SerializeField] GameObject foot1;
    [SerializeField] GameObject foot2;
    private void Start()
    {
        StartCoroutine(ShakingPos());
        StartCoroutine(ShakingPos2());
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
        EndEvent?.Invoke();
    }
}
