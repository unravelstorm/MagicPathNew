using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

    private Button btnStart;
    private Button btnShop;
    private Button btnRank;
    private Button btnSound;
    private Button btnRetry;
    private ManagerVars vars;


    private void Init()
    {
        vars = ManagerVars.GetManagerVars();
        //绑定按钮
        btnStart = transform.Find("btn_start").GetComponent<Button>();
        //绑定按钮点击函数
        btnStart.onClick.AddListener(OnStartButtonClick);
        //商店按钮
        btnShop = transform.Find("Buttons/btn_shop").GetComponent<Button>();
        btnShop.onClick.AddListener(OnShopButtonClick);
        //排行榜按钮
        btnRank = transform.Find("Buttons/btn_rank").GetComponent<Button>();
        btnRank.onClick.AddListener(OnRankButtonClick);
        //音乐按钮
        btnSound = transform.Find("Buttons/btn_sound").GetComponent<Button>();
        btnSound.onClick.AddListener(OnSoundButtonClick);
        //重置数据按钮
        btnRetry = transform.Find("Buttons/btn_retry").GetComponent<Button>();
        btnRetry.onClick.AddListener(OnRetryButtonClick);
    }

    private void Awake()
    {
        Init();
        EventCenter.AddListener(EventDefine.ShowMainPanel, Show);
        EventCenter.AddListener<int>(EventDefine.ChangeSkin, ChangeShopUI);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainPanel, Show);
        EventCenter.RemoveListener<int>(EventDefine.ChangeSkin, ChangeShopUI);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Start()
    {
        if (GameData.IsAgainGame)
        {
            OnStartButtonClick();
        }
        ChangeShopUI(GameManager.Instance.GetSelectedSkin());
    }
    /// <summary>
    /// 改变商店图标
    /// </summary>
    private void ChangeShopUI(int index)
    {
        btnShop.transform.GetChild(0).GetComponent<Image>().sprite = vars.SkinSpriteList[index];
    }
    
    /// <summary>
    /// 开始按钮点击后调用
    /// </summary>
    private void OnStartButtonClick()
    {
        GameManager.Instance.IsGameStarted = true;
        EventCenter.Broadcast(EventDefine.ShowGamePanel);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 商店按钮点击事件
    /// </summary>
    private void OnShopButtonClick()
    {
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.ShowShopPanel);
    }
    /// <summary>
    /// 排行榜按钮点击事件
    /// </summary>
    private void OnRankButtonClick()
    {

    }
    /// <summary>
    /// 声音按钮点击事件  
    /// </summary>
    private void OnSoundButtonClick()
    {

    }

    /// <summary>
    /// 重置数据按钮点击事件
    /// </summary>
    private void OnRetryButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ShowRetryPanel);
    }


}
