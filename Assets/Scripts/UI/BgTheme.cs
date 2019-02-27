using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgTheme : MonoBehaviour {
    //获取Sprite属性
    private SpriteRenderer m_SpriteRenderer;
    //定义素材管理器
    private ManagerVars vars;

    private void Awake()
    {
        vars = ManagerVars.GetManagerVars();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        int bgThemeNum = Random.Range(0, vars.bgThemeSpriteList.Count);
        m_SpriteRenderer.sprite = vars.bgThemeSpriteList[bgThemeNum];
    }
}
