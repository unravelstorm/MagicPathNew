using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "CreateManagerVasContainer")]
public class ManagerVars : ScriptableObject {
    /// <summary>
    /// 获取此类
    /// </summary>
    /// <returns></returns>
    public static ManagerVars GetManagerVars()
    {
        return Resources.Load<ManagerVars>("ManagerVasContainer");
    }
    /// <summary>
    /// 获取背景图片
    /// </summary>
    public List<Sprite> bgThemeSpriteList = new List<Sprite>();
    /// <summary>
    /// 获取不同的主题平台
    /// </summary>
    public List<Sprite> platformThemeSpriteList = new List<Sprite>();
    /// <summary>
    /// 获取皮肤图片
    /// </summary>
    public List<Sprite> SkinSpriteList = new List<Sprite>();
    /// <summary>
    /// 获取皮肤名字
    /// </summary>
    public List<string> SkinNameList = new List<string>();
    /// <summary>
    /// 获取选择的皮肤预制体
    /// </summary>
    public GameObject SkinChoosePre;
    /// <summary>
    /// 人物预制体
    /// </summary>
    public GameObject chacaterPre;
    /// <summary>
    /// 普通平台预制体
    /// </summary>
    public GameObject normalPlatformPre;
    /// <summary>
    /// 下一个平台所产生的位移
    /// </summary>
    public float nextXPos = 0.554f, nextYPos = 0.645f;
    /// <summary>
    /// 普通组合平台链表
    /// </summary>
    public List<GameObject> commonPlatformGroupList = new List<GameObject>();
    /// <summary>
    /// 草地组合平台链表
    /// </summary>
    public List<GameObject> grassPlatformGroupList = new List<GameObject>();
    /// <summary>
    /// 冬季组合平台链表
    /// </summary>
    public List<GameObject> winterPlatformGroupList = new List<GameObject>();
    /// <summary>
    /// 左边钉子组合平台
    /// </summary>
    public GameObject spikePlatformGroupLeft;
    /// <summary>
    /// 右边钉子组合平台
    /// </summary>
    public GameObject spikePlatformGroupRight;
    /// <summary>
    /// 死亡特效
    /// </summary>
    public GameObject deathEffect;
    /// <summary>
    /// 钻石
    /// </summary>
    public GameObject diamond;
}
