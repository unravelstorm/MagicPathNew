using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池
/// </summary>
public class ObjectPool : MonoBehaviour {
    public static ObjectPool Instance;
    private int initSpawnCount = 5;
    private List<GameObject> normalPlatformList = new List<GameObject>();
    private List<GameObject> commonPlatformList = new List<GameObject>();
    private List<GameObject> grassPlatformList = new List<GameObject>();
    private List<GameObject> winterPlatformList = new List<GameObject>();
    private List<GameObject> spikePlatformLeftList = new List<GameObject>();
    private List<GameObject> spikePlatformRightList = new List<GameObject>();
    /// <summary>
    /// 死亡特效list
    /// </summary>
    private List<GameObject> deathEffectList = new List<GameObject>();
    private List<GameObject> diamondList = new List<GameObject>();
    private ManagerVars vars;

    private void Awake()
    {
        Instance = this;
        vars = ManagerVars.GetManagerVars();
        Init();
    }

    private void Init()
    {
        //将普通平台放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            SetList(vars.normalPlatformPre, ref normalPlatformList);
        }
        //将通用多个平台放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < vars.commonPlatformGroupList.Count; j++)
            {
                SetList(vars.commonPlatformGroupList[j], ref commonPlatformList);
            }
        }
        //将草地多个平台放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < vars.grassPlatformGroupList.Count; j++)
            {
                SetList(vars.grassPlatformGroupList[j], ref grassPlatformList);
            }
        }
        //将雪地多个平台放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            for (int j = 0; j < vars.winterPlatformGroupList.Count; j++)
            {
                SetList(vars.winterPlatformGroupList[j], ref winterPlatformList);
            }
        }
        //将左钉子平台放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            SetList(vars.spikePlatformGroupLeft, ref spikePlatformLeftList);
        }
        //将右钉子平台放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            SetList(vars.spikePlatformGroupRight, ref spikePlatformRightList);
        }
        //将死亡特效放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            SetList(vars.deathEffect, ref deathEffectList);
        }
        //将钻石放入对象链表
        for (int i = 0; i < initSpawnCount; i++)
        {
            SetList(vars.diamond, ref diamondList);
        }
    }

    /// <summary>
    /// 将对象生成并放入list中
    /// </summary>
    /// <param name="go"></param>
    /// <param name="list"></param>
    private GameObject SetList(GameObject prefab, ref List<GameObject> addList)
    {
        GameObject go = Instantiate(prefab, transform);
        go.SetActive(false);
        addList.Add(go);
        return go;
    }
    /// <summary>
    /// 获取普通平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetNormalPlatform()
    {
        for (int i = 0; i < normalPlatformList.Count; i++)
        {
            if(normalPlatformList[i].activeInHierarchy == false)
            {
                return normalPlatformList[i];
            }
        }
        return SetList(vars.normalPlatformPre, ref normalPlatformList);
    }
    /// <summary>
    /// 获取通用平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetCommonPlatform()
    {
        for (int i = 0; i < commonPlatformList.Count; i++)
        {
            if (commonPlatformList[i].activeInHierarchy == false)
            {
                return commonPlatformList[i];
            }
        }
        int ran = Random.Range(0, vars.commonPlatformGroupList.Count);
        return SetList(vars.commonPlatformGroupList[ran], ref commonPlatformList);
    }
    /// <summary>
    /// 获取通用平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetGrassPlatform()
    {
        for (int i = 0; i < grassPlatformList.Count; i++)
        {
            if (grassPlatformList[i].activeInHierarchy == false)
            {
                return grassPlatformList[i];
            }
        }
        int ran = Random.Range(0, vars.grassPlatformGroupList.Count);
        return SetList(vars.grassPlatformGroupList[ran], ref grassPlatformList);
    }
    /// <summary>
    /// 获取雪地平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetWinterPlatform()
    {
        for (int i = 0; i < winterPlatformList.Count; i++)
        {
            if (winterPlatformList[i].activeInHierarchy == false)
            {
                return winterPlatformList[i];
            }
        }
        int ran = Random.Range(0, vars.winterPlatformGroupList.Count);
        return SetList(vars.winterPlatformGroupList[ran], ref winterPlatformList);
    }
    /// <summary>
    /// 获取左钉子平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetSpikePlatformLeft()
    {
        for (int i = 0; i < spikePlatformLeftList.Count; i++)
        {
            if (spikePlatformLeftList[i].activeInHierarchy == false)
            {
                return spikePlatformLeftList[i];
            }
        }
        return SetList(vars.spikePlatformGroupLeft, ref spikePlatformLeftList);
    }
    /// <summary>
    /// 获取右钉子平台
    /// </summary>
    /// <returns></returns>
    public GameObject GetSpikePlatformRight()
    {
        for (int i = 0; i < spikePlatformRightList.Count; i++)
        {
            if (spikePlatformRightList[i].activeInHierarchy == false)
            {
                return spikePlatformRightList[i];
            }
        }
        return SetList(vars.spikePlatformGroupRight, ref spikePlatformRightList);
    }
    /// <summary>
    /// 获取死亡特效
    /// </summary>
    /// <returns></returns>
    public GameObject GetDeathEffect()
    {
        for (int i = 0; i < deathEffectList.Count; i++)
        {
            if (deathEffectList[i].activeInHierarchy == false)
            {
                return deathEffectList[i];
            }
        }
        return SetList(vars.deathEffect, ref deathEffectList);
    }
    /// <summary>
    /// 获取钻石
    /// </summary>
    /// <returns></returns>
    public GameObject GetDiamond()
    {
        for (int i = 0; i < diamondList.Count; i++)
        {
            if (diamondList[i].activeInHierarchy == false)
            {
                return diamondList[i];
            }
        }
        return SetList(vars.diamond, ref diamondList);
    }
}
