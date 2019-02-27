using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    /// <summary>
    /// 是否再来一局
    /// </summary>
    public static bool IsAgainGame = false;

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
    #region 设置数据
    public void SetIsFirstGame(bool isFirstGame)
    {
        this.isFirstGame = isFirstGame;
    }
    public void SetIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }
    public void SetBestScoreArr(int[] bestScoreArr)
    {
        this.bestScoreArr = bestScoreArr;
    }
    public void SetSelectedSkin(int selectedSkin)
    {
        this.selectedSkin = selectedSkin;
    }
    public void SetUnlockSkin(bool[] unlockSkin)
    {
        this.unlockSkin = unlockSkin;
    }
    public void SetDiamondCount(int diamondCount)
    {
        this.diamondCount = diamondCount;
    }
    #endregion
    #region 获取数据
    public bool GetIsFirstGame()
    {
        return isFirstGame;
    }
    public bool GetIsMusicOn()
    {
        return isMusicOn;
    }
    public int[] GetBestScoreArr()
    {
        return bestScoreArr;
    }
    public int GetSelectedSkin()
    {
        return selectedSkin;
    }
    public bool[] GetUnlockSkin()
    {
        return unlockSkin;
    }
    public int GetDiamondCount()
    {
        return diamondCount;
    }
    #endregion
}
