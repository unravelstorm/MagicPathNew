using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class RetryPanel : MonoBehaviour
{
    /// <summary>
    /// 背景
    /// </summary>
    private Image bg;
    /// <summary>
    /// 主窗口
    /// </summary>
    private GameObject dialog;
    private Button btnNO;
    private Button btnYes;
    private ManagerVars vars;
    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        EventCenter.AddListener(EventDefine.ShowRetryPanel, Show);
        //获取游戏内对应元素
        bg = transform.Find("bg").GetComponent<Image>();
        dialog = transform.Find("Dialog").gameObject;
        btnNO = transform.Find("Dialog/btn_No").GetComponent<Button>();
        btnNO.onClick.AddListener(OnButtonNoClick);
        btnYes = transform.Find("Dialog/btn_Yes").GetComponent<Button>();
        btnYes.onClick.AddListener(OnButtonYesClick);

        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 0);
        dialog.transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowRetryPanel, Show);
    }
    private void Show()
    {
        gameObject.SetActive(true);
        bg.DOColor(new Color(bg.color.r, bg.color.g, bg.color.b, 0.4f), 0.3f);
        dialog.transform.DOScale(Vector3.one, 0.3f);
    }
    /// <summary>
    /// "是"按钮点击
    /// </summary>
    private void OnButtonYesClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio, vars.audioButton);
        GameManager.Instance.ResetGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /// <summary>
    /// "是"否按钮点击
    /// </summary>
    private void OnButtonNoClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio, vars.audioButton);
        bg.DOColor(new Color(bg.color.r, bg.color.g, bg.color.b, 0), 0.3f);
        dialog.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }

}
