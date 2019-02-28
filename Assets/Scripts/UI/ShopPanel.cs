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
    //获取所有钻石数量的txt
    private Text txtAllDiamondCount;
    //获取返回按钮
    private Button btnBack;
    //获取皮肤选择按钮
    private Button btnSkinSelect;
    //获取皮肤购买按钮
    private Button btnSkinBuy;
    //通过ScrollSkinParent.transform.localPosition.x来判断选中的是哪一个皮肤
    private int currentSkinIndex;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopPanel, Show);
        vars = ManagerVars.GetManagerVars();
        ScrollSkinParent = transform.Find("ScrollRegion/ScrollSkinParent");
        txtSkinName = transform.Find("txt_SkinName").GetComponent<Text>();
        txtAllDiamondCount = transform.Find("Diamond/txt_DiamondCount").GetComponent<Text>();

        btnBack = transform.Find("btn_Back").GetComponent<Button>();
        btnBack.onClick.AddListener(OnBackButtonClick);

        btnSkinSelect = transform.Find("btn_Select").GetComponent<Button>();
        btnSkinSelect.onClick.AddListener(OnSkinSelectButtonClick);

        btnSkinBuy = transform.Find("btn_Buy").GetComponent<Button>();
        btnSkinBuy.onClick.AddListener(OnSkinBuyButtonClick);
    }
    private void Start()
    {
        Init();
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel, Show);
    }
    private void Show()
    {
        CorrectScrollPos();
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 进入shop页面时选定好当前的皮肤
    /// </summary>
    private void CorrectScrollPos()
    {
        switch (GameManager.Instance.GetSelectedSkin())
        {
            case 0:ScrollSkinParent.transform.localPosition = new Vector3(-7.6f, 0, 0);break;
            case 1: ScrollSkinParent.transform.localPosition = new Vector3(-154f, 0, 0); break;
            case 2: ScrollSkinParent.transform.localPosition = new Vector3(-312f, 0, 0); break;
            case 3: ScrollSkinParent.transform.localPosition = new Vector3(-466f, 0, 0); break;
            default: break;
        }
    }
    private void Init()
    {
        for (int i = 0; i < vars.SkinSpriteList.Count; i++)
        {
            //实例化皮肤物体
            GameObject go = Instantiate(vars.SkinChoosePre, ScrollSkinParent);
            go.GetComponentInChildren<Image>().sprite = vars.SkinSpriteList[i];
            go.transform.localPosition = new Vector3(252 + 160 * i, 0, 0);
            //如果是未解锁的皮肤
            if (!GameManager.Instance.GetUnlockSkin(i))
            {
                go.GetComponentInChildren<Image>().color = Color.gray;
            }
            else
            {
                go.GetComponentInChildren<Image>().color = Color.white;
            }
        }
    }

    private void Update()
    {
        
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
        RefreshShopUI(currentSkinIndex);
    }

    /// <summary>
    /// 设置皮肤大小，选中的皮肤比其余的皮肤大
    /// </summary>
    /// <param name="index"></param>
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
    /// <summary>
    /// 刷新商店页面，皮肤名字，皮肤解锁信息,拥有钻石数量
    /// </summary>
    /// <param name="index"></param>
    private void RefreshShopUI(int index)
    {
        txtSkinName.text = vars.SkinNameList[index];
        //已解锁的皮肤
        if (GameManager.Instance.GetUnlockSkin(index))
        {
            btnSkinSelect.gameObject.SetActive(true);
            btnSkinBuy.gameObject.SetActive(false);
        }
        else
        {
            btnSkinSelect.gameObject.SetActive(false);
            btnSkinBuy.gameObject.SetActive(true);
            btnSkinBuy.GetComponentInChildren<Text>().text = vars.SkinPrice[index].ToString();
        }
        txtAllDiamondCount.text = GameManager.Instance.GetAllDiamond().ToString();
    }
    /// <summary>
    /// 返回按钮点击方法
    /// </summary>
    private void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.ShowMainPanel);
    }
    /// <summary>
    /// 皮肤选择按钮点击方法
    /// </summary>
    private void OnSkinSelectButtonClick()
    {
        EventCenter.Broadcast(EventDefine.ChangeSkin, currentSkinIndex);
        EventCenter.Broadcast(EventDefine.ShowMainPanel);

        gameObject.SetActive(false);
    }
    /// <summary>
    /// 皮肤购买按钮点击方法
    /// </summary>
    private void OnSkinBuyButtonClick()
    {
        int price = int.Parse(btnSkinBuy.GetComponentInChildren<Text>().text);
        if (price>GameManager.Instance.GetAllDiamond())
        {
            print("钻石数量不足！");
            return;
        }
        GameManager.Instance.UpdateAllDiamond(-price);
        GameManager.Instance.SetUnlockSkin(currentSkinIndex);
        ScrollSkinParent.GetChild(currentSkinIndex).GetChild(0).GetComponent<Image>().color = Color.white;
    }
}
