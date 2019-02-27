using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// 游戏钻石
    /// </summary>
    private int gameDiamond;

    private void Awake()
    {
        Instance = this;
        //监听增加成绩事件码
        EventCenter.AddListener(EventDefine.AddScore, AddGameScore);
        //监听增加钻石事件码
        EventCenter.AddListener(EventDefine.AddDiamond, AddGameDiamond);
        //监听开始移动事件码
        EventCenter.AddListener(EventDefine.PlayerStartMove, IsPlayerStartMove);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.AddScore, AddGameScore);
        EventCenter.RemoveListener(EventDefine.AddDiamond, AddGameDiamond);
        EventCenter.RemoveListener(EventDefine.PlayerStartMove, IsPlayerStartMove);
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
}
