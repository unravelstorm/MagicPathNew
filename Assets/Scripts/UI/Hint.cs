using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hint : MonoBehaviour
{
    private Image imgHint;
    private Text txtHint;
    private void Awake()
    {
        imgHint = gameObject.GetComponent<Image>();
        txtHint = gameObject.GetComponentInChildren<Text>();
        imgHint.color = new Color(imgHint.color.r, imgHint.color.g, imgHint.color.b, 0);
        txtHint.color = new Color(txtHint.color.r, txtHint.color.g, txtHint.color.b, 0);
        EventCenter.AddListener(EventDefine.ShowHint, Show);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowHint, Show);
    }
    private void Show()
    {
        StopCoroutine("DelayDisappear");
        transform.localPosition = new Vector3(0, -200f, 0);
        //y经过0.3s变为0,完成后实现延时消失
        transform.DOLocalMoveY(-70, 0.3f).OnComplete(() => { StartCoroutine("DelayDisappear");});
        //背景图片显示出来
        imgHint.DOColor(new Color(imgHint.color.r, imgHint.color.g, imgHint.color.b, 0.4f), 0.1f);
        //文字显示出来
        txtHint.DOColor(new Color(txtHint.color.r, txtHint.color.g, txtHint.color.b, 1), 0.1f);
    }
    /// <summary>
    /// 延时消失
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayDisappear()
    {
        yield return new WaitForSeconds(1f);
        transform.DOLocalMoveY(0, 0.3f);
        imgHint.DOColor(new Color(imgHint.color.r, imgHint.color.g, imgHint.color.b, 0), 0.1f);
        txtHint.DOColor(new Color(txtHint.color.r, txtHint.color.g, txtHint.color.b, 0), 0.1f);
    }
}
