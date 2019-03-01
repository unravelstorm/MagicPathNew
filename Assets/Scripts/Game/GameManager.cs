using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    

    /// <summary>
    /// 游戏是否开始
    /// </summary>
    public bool IsGameStarted { get; set; }
    /// <summary>
    /// 游戏是否结束
    /// </summary>
    public bool IsGameOver { get; set; }
    /// <summary>
    /// 游戏是否暂停
    /// </summary>
    public bool IsGamePause { get; set; }
    /// <summary>
    /// 是否开始移动
    /// </summary>
    public bool IsStartMove { get; set; }
    /// <summary>
    /// 游戏成绩
    /// </summary>
    private int gameScroe;
    /// <summary>
    /// 一局游戏获得的钻石
    /// </summary>
    private int gameDiamond;

    private ManagerVars vars;
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
    /// 所拥有的钻石数量
    /// </summary>
    private int diamondCount;
    /// <summary>
    /// 初始化游戏数据
    /// </summary>
    private void InitGameData()
    {
        //判断是否第一次游戏
        if (gameData != null)
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
            isFirstGame = false;
            isMusicOn = true;
            bestScoreArr = new int[3];
            selectedSkin = 0;
            unlockSkin = new bool[vars.SkinSpriteList.Count];
            unlockSkin[0] = true;
            diamondCount = 10;
            gameData = new GameData();
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

    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        Instance = this;
        //监听增加成绩事件码
        EventCenter.AddListener(EventDefine.AddScore, AddGameScore);
        //监听增加钻石事件码
        EventCenter.AddListener(EventDefine.AddDiamond, AddGameDiamond);
        //监听开始移动事件码
        EventCenter.AddListener(EventDefine.PlayerStartMove, IsPlayerStartMove);
        //监听保存成绩事件码
        EventCenter.AddListener<int>(EventDefine.SaveScore, SaveScore);
        GameLoad();
        InitGameData();
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.AddScore, AddGameScore);
        EventCenter.RemoveListener(EventDefine.AddDiamond, AddGameDiamond);
        EventCenter.RemoveListener(EventDefine.PlayerStartMove, IsPlayerStartMove);
        EventCenter.RemoveListener<int>(EventDefine.SaveScore, SaveScore);
    }
    /// <summary>
    /// 监听开始移动事件码，调用此方法
    /// </summary>
    private void IsPlayerStartMove()
    {
        IsStartMove = true;
    }
    /// <summary>
    /// 增加游戏成绩
    /// </summary>
    private void AddGameScore()
    {
        if (!IsGameStarted || IsGamePause || IsGameOver)
        {
            return;
        }
        gameScroe++;
        EventCenter.Broadcast(EventDefine.UpdateScoreText, gameScroe);
    }
    /// <summary>
    /// 增加游戏钻石
    /// </summary>
    private void AddGameDiamond()
    {
        if (!IsGameStarted || IsGamePause || IsGameOver)
        {
            return;
        }
        gameDiamond++;
        EventCenter.Broadcast(EventDefine.UpdateDiamondText, gameDiamond);
    }

    /// <summary>
    /// 获取游戏分数
    /// </summary>
    /// <returns></returns>
    public int GetGameScore()
    {
        return gameScroe;
    }
    /// <summary>
    /// 获取游戏钻石
    /// </summary>
    /// <returns></returns>
    public int GetGameDiamond()
    {
        return gameDiamond;
    }
    /// <summary>
    /// 获取当前皮肤解锁信息
    /// </summary>
    /// <returns></returns>
    public bool GetUnlockSkin(int index)
    {
        return unlockSkin[index];
    }
    /// <summary>
    /// 解锁当前皮肤
    /// </summary>
    /// <returns></returns>
    public void SetUnlockSkin(int index)
    {
        unlockSkin[index] = true;
        GameSave();
    }
    /// <summary>
    /// 设置选中的皮肤
    /// </summary>
    /// <param name="index"></param>
    public void SetSelectedSkin(int index)
    {
        selectedSkin = index;
        GameSave();
    }
    /// <summary>
    /// 获取选中的皮肤
    /// </summary>
    /// <returns></returns>
    public int GetSelectedSkin()
    {
        return selectedSkin;
    }

    /// <summary>
    /// 获取所拥有的钻石
    /// </summary>
    public int GetAllDiamond()
    {
        return diamondCount;
    }
    /// <summary>
    /// 更新所拥有的钻石
    /// </summary>
    public void UpdateAllDiamond(int num)
    {
        diamondCount += num;
        GameSave();
    }

    /// <summary>
    /// 游戏存储
    /// </summary>
    public void GameSave()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData.data"))
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
    public void GameLoad()
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

    /// <summary>
    /// 重置游戏数据
    /// </summary>
    public void ResetGameData()
    {
        isFirstGame = false;
        isMusicOn = true;
        bestScoreArr = new int[3];
        selectedSkin = 0;
        unlockSkin = new bool[vars.SkinSpriteList.Count];
        unlockSkin[0] = true;
        diamondCount = 10;
        GameSave();
    }
    /// <summary>
    /// 保存成绩
    /// </summary>
    private void SaveScore(int score)
    {
        List<int> list = bestScoreArr.ToList();
        list.Add(score);
        //从大到小排序
        list.Sort((x, y) => (-x.CompareTo(y)));
        //去除最低分
        list.RemoveAt(list.Count - 1);
        bestScoreArr = list.ToArray();
        GameSave();
    }
    /// <summary>
    /// 获取最高分
    /// </summary>
    /// <returns></returns>
    public int GetBestScore()
    {
        return bestScoreArr.Max();
    }
    /// <summary>
    /// 获取排行榜分数
    /// </summary>
    /// <returns></returns>
    public int[] GetRankScore()
    {
        List<int> list = bestScoreArr.ToList();
        //从大到小排序
        list.Sort((x, y) => (-x.CompareTo(y)));
        bestScoreArr = list.ToArray();
        return bestScoreArr;
    }
    /// <summary>
    /// 获取音效是否开启
    /// </summary>
    /// <returns></returns>
    public bool GetIsMusicOn()
    {
        return isMusicOn;
    }
    /// <summary>
    /// 设置音效是否开启
    /// </summary>
    /// <param name="value"></param>
    public void SetIsMusicOn(bool value)
    {
        isMusicOn = value;
        GameSave();
    }
}
