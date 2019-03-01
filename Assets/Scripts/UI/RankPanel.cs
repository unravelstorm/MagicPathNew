using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RankPanel : MonoBehaviour
{
    private Button btnBackBg;
    private List<Text> txtScore;
    private GameObject goDialog;

    private ManagerVars vars;

    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        //监听显示排行榜界面
        EventCenter.AddListener(EventDefine.ShowRankPanel,Show);
        //获取返回按钮，并监听
        btnBackBg = transform.Find("btn_Bg").GetComponent<Button>();
        btnBackBg.onClick.AddListener(OnBackButtonClick);
        //获取分数面板
        goDialog = transform.Find("Dialog").gameObject;
        goDialog.transform.localScale = Vector3.zero;
        //获取分数
        txtScore = new List<Text>();
        foreach (Transform score in goDialog.transform)
        {
            txtScore.Add(score.GetComponentInChildren<Text>());
        }
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowRankPanel, Show);
    }

    private void Show()
    {
        for (int i = 0; i < txtScore.Count; i++)
        {
            txtScore[i].text = GameManager.Instance.GetRankScore()[i].ToString();
        }
        gameObject.SetActive(true);
        goDialog.GetComponent<Transform>().DOScale(Vector3.one, 0.3f);
    }
    private void OnBackButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio, vars.audioButton);
        goDialog.GetComponent<Transform>().DOScale(Vector3.zero, 0.3f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
