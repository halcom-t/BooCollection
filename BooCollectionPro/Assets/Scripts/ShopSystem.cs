using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 食べ物の情報
/// </summary>
[System.Serializable]
struct FoodData
{
    [SerializeField] public Sprite Image;       //画像
    [SerializeField] public string Name;        //名前
    [SerializeField] public string Detail;      //説明文
    [SerializeField] public int Cost;           //価格（BP）
    [SerializeField] public int ExpiryDateMin;  //賞味期限（分）
}

/// <summary>
/// ショップ機能
/// </summary>
public class ShopSystem : MonoBehaviour
{
    /// <summary>
    /// ContentsUIに載せる情報
    /// 【注意】変数名変更するとデータ消えるから注意
    /// </summary>
    [SerializeField] List<FoodData> foodDataList = new List<FoodData>();

    /// <summary>
    /// ショップリストに並べるセル（プレファブ）
    /// </summary>
    [SerializeField] GameObject shopCellPre;

    /// <summary>
    /// ショップリストのセルを並べる親オブジェクト
    /// </summary>
    [SerializeField] GameObject shopContentParent;

    //コンポーネント-------------------------------------------------
    Animator anim;

    [SerializeField] GameObject Camera;
    UIManager uiManager;


    void Awake()
    {
        anim = GetComponent<Animator>();
        uiManager = Camera.GetComponent<UIManager>();

        //ショップのコンテンツUI作成
        CreateShopContentUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ショップを閉じ終わった時のアニメーションイベント
    /// </summary>
    public void AnimEventCloseShop()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// ショップリストに並べるコンテンツUIの作成
    /// </summary>
    void CreateShopContentUI()
    {
        for (int i = 0; i < foodDataList.Count; i++)
        {
            //プレファブを基にUI作成
            GameObject content = Instantiate(shopCellPre);

            //画像
            Image image = content.transform.Find("FoodImage").GetComponent<Image>();
            image.sprite = foodDataList[i].Image;
            //名前
            Text name = content.transform.Find("NameText").GetComponent<Text>();
            name.text = foodDataList[i].Name;
            //説明
            Text detail = content.transform.Find("DetailBgImage/DetailText").GetComponent<Text>();
            detail.text = foodDataList[i].Detail;
            //価格
            Text cost = content.transform.Find("CoinImage/CostText").GetComponent<Text>();
            cost.text = foodDataList[i].Cost.ToString();
            //賞味期限
            Text expiryDate = content.transform.Find("ExpiryDateText").GetComponent<Text>();
            int h = foodDataList[i].ExpiryDateMin / 60;
            int min = foodDataList[i].ExpiryDateMin % 60;
            if (h == 0)
            {
                expiryDate.text = string.Format("{0}min", min);
            }
            else if (min == 0)
            {
                expiryDate.text = string.Format("{0}h", h);
            }
            else
            {
                expiryDate.text = string.Format("{0}h{1}min", h, min);
            }


            content.GetComponent<Button>().onClick.AddListener(() => { OnShopCell(content); });

            //親オブジェクトの設定
            content.transform.SetParent(shopContentParent.transform);
        }
    }

    /// <summary>
    /// ショップリストのセルをタップしたとき
    /// </summary>
    /// <param name="tapCellObj">タップしたセル</param>
    public void OnShopCell(GameObject tapCellObj)
    {
        //tapCellObj.transform.Find("FoodImage").GetComponent<Image>()

        //選択中の皿オブジェクト取得
        GameObject selectedPlate = uiManager.plateObjs[uiManager.selectedPlateIndex];

        //皿に食べ物を表示
        Sprite selectFood = tapCellObj.transform.Find("FoodImage").GetComponent<Image>().sprite;
        selectedPlate.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = selectFood;
    }

}
