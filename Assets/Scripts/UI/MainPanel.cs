using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {
    ManagerVars vars;

    private Button btnStart;
    private Button btnShop;
    private Button btnRank;
    private Button btnSound;

    private GameData gameData;
    /// <summary>
    /// 是否第一次游戏
    /// </summary>
    private bool isFirstGame;
    /// <summary>
    /// 是否开启音乐
    /// </summary>
    private bool isMusicOn;
    /// <summary>
    /// 最佳成绩数组
    /// </summary>
    private int[] bestScoreArr;
    /// <summary>
    /// 选中的皮肤序号
    /// </summary>
    private int selectedSkin;
    /// <summary>
    /// 皮肤是否解锁
    /// </summary>
    private bool[] unlockSkin;
    /// <summary>
    /// 钻石数量
    /// </summary>
    private int diamondCount;
    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    private void InitGameData()
    {
        //判断是否第一次游戏
        if (gameData!=null)
        {
            isFirstGame = gameData.GetIsFirstGame();
        }
        else
        {
            isFirstGame = true;
        }
        //如果第一次游戏
        if (isFirstGame)
        {
            isMusicOn = true;
            bestScoreArr = new int[3];
            selectedSkin = 0;
            unlockSkin = new bool[vars.SkinSpriteList.Count];
            unlockSkin[0] = true;
            diamondCount = 10;
            GameSave();
        }
        else
        {
            isMusicOn = gameData.GetIsMusicOn();
            bestScoreArr = gameData.GetBestScoreArr();
            selectedSkin = gameData.GetSelectedSkin();
            unlockSkin = gameData.GetUnlockSkin();
            diamondCount = gameData.GetDiamondCount();
        }
    }
    private void Init()
    {
        vars = ManagerVars.GetManagerVars();
        //绑定按钮
        btnStart = transform.Find("btn_start").GetComponent<Button>();
        //绑定按钮点击函数
        btnStart.onClick.AddListener(OnStartButtonClick);

        btnShop = transform.Find("Buttons/btn_shop").GetComponent<Button>();
        btnShop.onClick.AddListener(OnShopButtonClick);

        btnRank = transform.Find("Buttons/btn_rank").GetComponent<Button>();
        btnRank.onClick.AddListener(OnRankButtonClick);

        btnSound = transform.Find("Buttons/btn_sound").GetComponent<Button>();
        btnSound.onClick.AddListener(OnSoundButtonClick);
    }

    private void Awake()
    {
        Init();
        EventCenter.AddListener(EventDefine.ShowMainPanel, Show);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainPanel, Show);
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
    /// 游戏存储
    /// </summary>
    private void GameSave()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath+"/GameData.data"))
            {
                gameData.SetIsFirstGame(isFirstGame);
                gameData.SetIsMusicOn(isMusicOn);
                gameData.SetBestScoreArr(bestScoreArr);
                gameData.SetDiamondCount(diamondCount);
                gameData.SetSelectedSkin(selectedSkin);
                gameData.SetUnlockSkin(unlockSkin);
                bf.Serialize(fs, gameData);
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
    /// <summary>
    /// 游戏读取
    /// </summary>
    private void GameLoad()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + "/GameData.data", FileMode.Open))
            {
                gameData = (GameData)bf.Deserialize(fs);
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }
}
