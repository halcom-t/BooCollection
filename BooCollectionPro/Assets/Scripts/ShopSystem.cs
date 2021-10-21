using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �H�ו��̏��
/// </summary>
[System.Serializable]
struct FoodData
{
    [SerializeField] public Sprite Image;       //�摜
    [SerializeField] public string Name;        //���O
    [SerializeField] public string Detail;      //������
    [SerializeField] public int Cost;           //���i�iBP�j
    [SerializeField] public int ExpiryDateMin;  //�ܖ������i���j
}

/// <summary>
/// �V���b�v�@�\
/// </summary>
public class ShopSystem : MonoBehaviour
{
    /// <summary>
    /// ContentsUI�ɍڂ�����
    /// �y���Ӂz�ϐ����ύX����ƃf�[�^�����邩�璍��
    /// </summary>
    [SerializeField] List<FoodData> foodDataList = new List<FoodData>();

    /// <summary>
    /// �V���b�v���X�g�ɕ��ׂ�Z���i�v���t�@�u�j
    /// </summary>
    [SerializeField] GameObject shopCellPre;

    /// <summary>
    /// �V���b�v���X�g�̃Z������ׂ�e�I�u�W�F�N�g
    /// </summary>
    [SerializeField] GameObject shopContentParent;

    //�R���|�[�l���g-------------------------------------------------
    Animator anim;

    [SerializeField] GameObject Camera;
    UIManager uiManager;


    void Awake()
    {
        anim = GetComponent<Animator>();
        uiManager = Camera.GetComponent<UIManager>();

        //�V���b�v�̃R���e���cUI�쐬
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
    /// �V���b�v����I��������̃A�j���[�V�����C�x���g
    /// </summary>
    public void AnimEventCloseShop()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// �V���b�v���X�g�ɕ��ׂ�R���e���cUI�̍쐬
    /// </summary>
    void CreateShopContentUI()
    {
        for (int i = 0; i < foodDataList.Count; i++)
        {
            //�v���t�@�u�����UI�쐬
            GameObject content = Instantiate(shopCellPre);

            //�摜
            Image image = content.transform.Find("FoodImage").GetComponent<Image>();
            image.sprite = foodDataList[i].Image;
            //���O
            Text name = content.transform.Find("NameText").GetComponent<Text>();
            name.text = foodDataList[i].Name;
            //����
            Text detail = content.transform.Find("DetailBgImage/DetailText").GetComponent<Text>();
            detail.text = foodDataList[i].Detail;
            //���i
            Text cost = content.transform.Find("CoinImage/CostText").GetComponent<Text>();
            cost.text = foodDataList[i].Cost.ToString();
            //�ܖ�����
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

            //�e�I�u�W�F�N�g�̐ݒ�
            content.transform.SetParent(shopContentParent.transform);
        }
    }

    /// <summary>
    /// �V���b�v���X�g�̃Z�����^�b�v�����Ƃ�
    /// </summary>
    /// <param name="tapCellObj">�^�b�v�����Z��</param>
    public void OnShopCell(GameObject tapCellObj)
    {
        //tapCellObj.transform.Find("FoodImage").GetComponent<Image>()

        //�I�𒆂̎M�I�u�W�F�N�g�擾
        GameObject selectedPlate = uiManager.plateObjs[uiManager.selectedPlateIndex];

        //�M�ɐH�ו���\��
        Sprite selectFood = tapCellObj.transform.Find("FoodImage").GetComponent<Image>().sprite;
        selectedPlate.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = selectFood;
    }

}
