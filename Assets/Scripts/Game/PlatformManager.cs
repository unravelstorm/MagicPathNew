using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {
    public SpriteRenderer[] spriteRenderers;
    public GameObject obstacle;
    /// <summary>
    /// 开始计时器
    /// </summary>
    private bool startTimer;
    /// <summary>
    /// 掉落时间
    /// </summary>
    private float fallTime;
    private Rigidbody2D platformBody;
    private void Awake()
    {
        platformBody = GetComponent<Rigidbody2D>();
    }
    public void Init(Sprite sprite, float fallTime)
    {
        platformBody.bodyType = RigidbodyType2D.Static;
        startTimer = true;
        this.fallTime = fallTime;
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = sprite;
        }
    }
    public void Init(Sprite sprite, int dir, float fallTime)
    {
        platformBody.bodyType = RigidbodyType2D.Static;
        startTimer = true;
        this.fallTime = fallTime;
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = sprite;
        }
        if (dir == 0)//向右生成障碍物
        {
            if (obstacle != null)
            {
                obstacle.transform.localPosition = new Vector3(-obstacle.transform.localPosition.x, obstacle.transform.localPosition.y, obstacle.transform.localPosition.z);
            }
        }
    }
    private void Update()
    {
        //游戏未开始，人物未移动
        if (!GameManager.Instance.IsGameStarted || !GameManager.Instance.IsStartMove)
        {
            return;
        }
        //平台开始掉落
        if (startTimer)
        {
            fallTime -= Time.deltaTime;
            if (fallTime < 0)
            {
                startTimer = false;
                if (platformBody.bodyType != RigidbodyType2D.Dynamic)
                {
                    platformBody.bodyType = RigidbodyType2D.Dynamic;
                    StartCoroutine(DelayHide());
                }
            }
        }
        //平台与摄像机距离过远，隐藏平台
        if (transform.position.y - Camera.main.transform.position.y < -6)
        {
            StartCoroutine(DelayHide());
        }
    }
    /// <summary>
    /// 隐藏平台
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
