using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    private ManagerVars vars;
    //获取滑动区域
    private Transform ScrollSkinParent;
    //获取面板显示名字的txt
    private Text txtSkinName;
    //获取返回按钮
    private Button btnBack;
    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopPanel, Show);
        vars = ManagerVars.GetManagerVars();
        ScrollSkinParent = transform.Find("ScrollRegion/ScrollSkinParent");
        txtSkinName = transform.Find("txt_SkinName").GetComponent<Text>();
        btnBack = transform.Find("btn_Back").GetComponent<Button>();
        btnBack.onClick.AddListener(OnBackButtonClick);
        Init();
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel, Show);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Init()
    {
        for (int i = 0; i < vars.SkinSpriteList.Count; i++)
        {
            //实例化皮肤物体
            GameObject go = Instantiate(vars.SkinChoosePre, ScrollSkinParent);
            go.GetComponentInChildren<Image>().sprite = vars.SkinSpriteList[i];
            go.transform.localPosition = new Vector3(252 + 160 * i, 0, 0);
        }
    }

    private void Update()
    {
        //通过ScrollSkinParent.transform.localPosition.x来判断选中的是哪一个皮肤
        int currentSkinIndex;
        if (ScrollSkinParent.transform.localPosition.x > -70)
        {
            currentSkinIndex = 0;
        }
        else if (ScrollSkinParent.transform.localPosition.x <= -70 && ScrollSkinParent.transform.localPosition.x > -200)
        {
            currentSkinIndex = 1;
        }
        else if (ScrollSkinParent.transform.localPosition.x <= -200 && ScrollSkinParent.transform.localPosition.x > -355)
        {
            currentSkinIndex = 2;
        }
        else if (ScrollSkinParent.transform.localPosition.x <= -355)
        {
            currentSkinIndex = 3;
        }
        else
        {
            currentSkinIndex = 0;
        }
        SetSkinSize(currentSkinIndex);
        SetSkinName(currentSkinIndex);
    }

    private void SetSkinSize(int index)
    {
        for (int i = 0; i < ScrollSkinParent.childCount; i++)
        {
            if (index == i)
            {
                ScrollSkinParent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
            }
            else
            {
                ScrollSkinParent.GetChild(i).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
            }
        }
    }
    private void SetSkinName(int index)
    {
        txtSkinName.text = vars.SkinNameList[index];
    }

    private void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
    }
}
