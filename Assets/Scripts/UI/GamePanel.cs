using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
    private Button btn_Pause;
    private Button btn_Play;
    private Text txt_DiamondCount;
    private Text txt_Score;

    private ManagerVars vars;

    private void Init()
    {
        btn_Pause = transform.Find("btn_pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OnPauseButtonClick);
        btn_Play = transform.Find("btn_play").GetComponent<Button>();
        btn_Play.onClick.AddListener(OnPlayButtonClick);
        txt_DiamondCount = transform.Find("Diamond/txt_diamondCount").GetComponent<Text>();
        txt_Score = transform.Find("txt_score").GetComponent<Text>();
        btn_Play.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        //监听显示面板事件码
        EventCenter.AddListener(EventDefine.ShowGamePanel, Show);
        //监听更新分数文本事件码
        EventCenter.AddListener<int>(EventDefine.UpdateScoreText, UpdateScoreText);
        //监听更新钻石文本事件码
        EventCenter.AddListener<int>(EventDefine.UpdateDiamondText, UpdateDiamondText);
        Init();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGamePanel, Show);
        EventCenter.RemoveListener<int>(EventDefine.UpdateScoreText, UpdateScoreText);
        EventCenter.RemoveListener<int>(EventDefine.UpdateDiamondText, UpdateDiamondText);
    }
    /// <summary>
    /// 更新分数文本
    /// </summary>
    /// <param name="score"></param>
    private void UpdateScoreText(int score)
    {
        txt_Score.text = score.ToString();
    }
    /// <summary>
    /// 更新钻石文本
    /// </summary>
    /// <param name="score"></param>
    private void UpdateDiamondText(int diamond)
    {
        txt_DiamondCount.text = diamond.ToString();
    }
    /// <summary>
    /// 显示此面板
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 暂停按钮点击
    /// </summary>
    private void OnPauseButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio, vars.audioButton);
        btn_Play.gameObject.SetActive(true);
        btn_Pause.gameObject.SetActive(false);
        //游戏暂停
        Time.timeScale = 0;
        GameManager.Instance.IsGamePause = true;

    }
    /// <summary>
    /// 开始按钮点击
    /// </summary>
    private void OnPlayButtonClick()
    {
        EventCenter.Broadcast(EventDefine.PlayAudio, vars.audioButton);
        btn_Play.gameObject.SetActive(false);
        btn_Pause.gameObject.SetActive(true);
        //继续游戏
        Time.timeScale = 1;
        GameManager.Instance.IsGamePause = false;

    }
}
