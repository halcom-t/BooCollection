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

    /// <summary>
    /// 皿オブジェクト（C,L,R）
    /// </summary>
    [SerializeField] List<GameObject> plateObjs;
    /// <summary>
    /// 各皿オブジェクトのAnimator（C,L,R）
    /// </summary>
    List<Animator> plateAnims = new List<Animator>();
    /// <summary>
    /// 現在選択中の皿オブジェクトのインデックス番号
    /// </summary>
    int selectedPlateIndex;


    //コンポーネント----------------------------
    GameManager gameManager;
    BoosManager boosManager;


    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        boosManager = GetComponent<BoosManager>();
        booPointAnim = booPointUI.GetComponent<Animator>();
        shopAnim = shopUI.GetComponent<Animator>();
        for (int i = 0; i < 3; i++)
        {
            plateAnims.Add(plateObjs[i].GetComponent<Animator>());
        }
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
    /// ショップ画面を開く処理
    /// </summary>
    /// <param name="selectPlate">選択された皿オブジェクト</param>
    public void OpenShop(GameObject selectPlate)
    {
        //ショップ画面 & UFO非表示
        if (!shopUI.activeSelf && !ufoEffectObj.activeSelf)
        {
            //ショップ画面表示＆アイコン非表示
            shopUI.SetActive(true);
            iconAreaUI.SetActive(false);
        }

        //皿アニメ切り替え
        for (int i = 0; i < plateObjs.Count; i++)
        {
            if (selectPlate == plateObjs[i])
            {
                plateAnims[i].SetBool("IsSelected", true);
                selectedPlateIndex = i;
            }
            else
            {
                plateAnims[i].SetBool("IsSelected", false);
            }
        }
    }

    /// <summary>
    /// ショップのCLOSEボタンタップ時
    /// </summary>
    public void OnShopCloseButton()
    {
        //ショップ画面を閉じる
        shopAnim.SetTrigger("Close");
        iconAreaUI.SetActive(true);
        //皿アニメ終了
        plateAnims[selectedPlateIndex].SetBool("IsSelected", false);
    }

    /// <summary>
    /// BPの加算＆更新処理
    /// </summary>
    /// <param name="bp">加算するBP</param>
    public void AddBooPoint(int bp)
    {
        gameManager.booPoint += bp;
        booPointText.text = gameManager.booPoint.ToString();
        //アニメーション再生
        booPointAnim.SetTrigger("BpChange");
    }

}
