using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {
    //定义结束界面显示的分数，最高分，获得钻石数量文本
    private Text txtScore, txtMaxScore, txtDiamondCount;
    //定义再来一局，排行榜，主界面按钮
    private Button btnRestart, btnRank, btnHome;

    private void Awake()
    {
        Init();
        gameObject.SetActive(false);
        //监听显示游戏结束面板事件
        EventCenter.AddListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGameOverPanel, ShowGameOverPanel);
    }
    private void Init()
    {
        //获取游戏结束面板文本与按钮对象，并绑定按钮点击事件
        txtScore = transform.Find("txt_Score").GetComponent<Text>();
        txtMaxScore = transform.Find("txt_MaxScore").GetComponent<Text>();
        txtDiamondCount = transform.Find("Diamond/txt_DiamondCount").GetComponent<Text>();

        btnRestart = transform.Find("btn_Restart").GetComponent<Button>();
        btnRestart.onClick.AddListener(OnBtnRestartClick);

        btnRank = transform.Find("btn_Rank").GetComponent<Button>();
        btnRank.onClick.AddListener(OnBtnRankClick);

        btnHome = transform.Find("btn_Home").GetComponent<Button>();
        btnHome.onClick.AddListener(OnBtnHomeClick);
    }
    /// <summary>
    /// 显示游戏结束面板
    /// </summary>
    private void ShowGameOverPanel()
    {
        txtScore.text = GameManager.Instance.GetGameScore().ToString();
        txtDiamondCount.text = GameManager.Instance.GetGameDiamond().ToString();
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 再来一局按钮点击方法
    /// </summary>
    private void OnBtnRestartClick()
    {
        //重新加载当前Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //再来一局标识置为true
        GameData.IsAgainGame = true;
    }
    /// <summary>
    /// 排行榜按钮点击方法
    /// </summary>
    private void OnBtnRankClick()
    {

    }
    /// <summary>
    /// 主界面按钮点击方法
    /// </summary>
    private void OnBtnHomeClick()
    {
        //重新加载当前Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //再来一局标识置为false
        GameData.IsAgainGame = false;
    }
}
