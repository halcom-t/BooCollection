using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UFO���o�I�u�W�F�N�g
    /// </summary>
    [SerializeField] GameObject ufoEffectObj;

    /// <summary>
    /// ShopUI
    /// </summary>
    [SerializeField] GameObject shopUI;
    /// <summary>
    /// �V���b�v�̃A�j���[�^�[
    /// </summary>
    Animator shopAnim;

    /// <summary>
    /// ��ʉ����̃A�C�R��UI�o�[
    /// </summary>
    [SerializeField] GameObject iconAreaUI;

    /// <summary>
    /// BP��UI
    /// </summary>
    [SerializeField] GameObject booPointUI;
    /// <summary>
    /// BP�iText�j
    /// </summary>
    [SerializeField] Text booPointText;
    /// <summary>
    /// BP��UI�̃A�j���[�^�[
    /// </summary>
    Animator booPointAnim;

    /// <summary>
    /// �M�I�u�W�F�N�g�iC,L,R�j
    /// </summary>
    [SerializeField] List<GameObject> plateObjs;
    /// <summary>
    /// �e�M�I�u�W�F�N�g��Animator�iC,L,R�j
    /// </summary>
    List<Animator> plateAnims = new List<Animator>();
    /// <summary>
    /// ���ݑI�𒆂̎M�I�u�W�F�N�g�̃C���f�b�N�X�ԍ�
    /// </summary>
    int selectedPlateIndex;


    //�R���|�[�l���g----------------------------
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
    /// UFO��UI�{�^������������
    /// </summary>
    public void OnUIButtonUFO()
    {
        //�u�[������Ȃ�UFO���A�N�e�B�u�ɂ���
        if (boosManager.activeBoos.Count > 0)ufoEffectObj.SetActive(true);    
    }

    /// <summary>
    /// �V���b�v��ʂ��J������
    /// </summary>
    /// <param name="selectPlate">�I�����ꂽ�M�I�u�W�F�N�g</param>
    public void OpenShop(GameObject selectPlate)
    {
        //�V���b�v��� & UFO��\��
        if (!shopUI.activeSelf && !ufoEffectObj.activeSelf)
        {
            //�V���b�v��ʕ\�����A�C�R����\��
            shopUI.SetActive(true);
            iconAreaUI.SetActive(false);
        }

        //�M�A�j���؂�ւ�
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
    /// �V���b�v��CLOSE�{�^���^�b�v��
    /// </summary>
    public void OnShopCloseButton()
    {
        //�V���b�v��ʂ����
        shopAnim.SetTrigger("Close");
        iconAreaUI.SetActive(true);
        //�M�A�j���I��
        plateAnims[selectedPlateIndex].SetBool("IsSelected", false);
    }

    /// <summary>
    /// BP�̉��Z���X�V����
    /// </summary>
    /// <param name="bp">���Z����BP</param>
    public void AddBooPoint(int bp)
    {
        gameManager.booPoint += bp;
        booPointText.text = gameManager.booPoint.ToString();
        //�A�j���[�V�����Đ�
        booPointAnim.SetTrigger("BpChange");
    }

}
