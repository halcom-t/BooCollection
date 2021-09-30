using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UFO演出オブジェクト
    /// </summary>
    [SerializeField] GameObject ufoEffectObj;

    /// <summary>
    /// ShopUI
    /// </summary>
    [SerializeField] GameObject shopUI;
    /// <summary>
    /// ショップのアニメーター
    /// </summary>
    Animator shopAnim;

    /// <summary>
    /// 画面下部のアイコンUIバー
    /// </summary>
    [SerializeField] GameObject iconAreaUI;

    /// <summary>
    /// BPのUI
    /// </summary>
    [SerializeField] GameObject booPointUI;
    /// <summary>
    /// BP（Text）
    /// </summary>
    [SerializeField] Text booPointText;
    /// <summary>
    /// BPのUIのアニメーター
    /// </summary>
    Animator booPointAnim;

    //コンポーネント----------------------------
    GameManager gameManager;
    BoosManager boosManager;


    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        boosManager = GetComponent<BoosManager>();
        booPointAnim = booPointUI.GetComponent<Animator>();
        shopAnim = shopUI.GetComponent<Animator>();
    }

    void Start()
    {
        booPointText.text = gameManager.booPoint.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// UFOのUIボタンを押した時
    /// </summary>
    public void OnUIButtonUFO()
    {
        //ブーがいるならUFOをアクティブにする
        if (boosManager.activeBoos.Count > 0)ufoEffectObj.SetActive(true);    
    }

    /// <summary>
    /// 皿をタップした時
    /// </summary>
    public void OnPlateButton()
    {
        //ショップ画面 & UFO非表示
        if (!shopUI.activeSelf && !ufoEffectObj.activeSelf)
        {
            //ショップ画面表示＆アイコン非表示
            shopUI.SetActive(true);
            iconAreaUI.SetActive(false);
        }
    }

    /// <summary>
    /// ショップのCLOSEボタンタップ時
    /// </summary>
    public void OnShopCloseButton()
    {
        shopAnim.SetBool("IsClose", true);
        iconAreaUI.SetActive(true);
    }

    /// <summary>
    /// BPの加算＆更新処理
    /// </summary>
    /// <param name="bp">加算するBP</param>
    public void AddBooPoint(int bp)
    {
        gameManager.booPoint += bp;
        booPointText.text = gameManager.booPoint.ToString();
        booPointAnim.SetBool("IsBPChange", true);
        //0.25秒後にアニメ―ション終了
        Invoke("EndBPAnim", 0.25f);
    }

    /// <summary>
    /// BPアニメーション終了
    /// </summary>
    void EndBPAnim()
    {
        booPointAnim.SetBool("IsBPChange", false);
    }
}
