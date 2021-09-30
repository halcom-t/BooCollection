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

    //�R���|�[�l���g----------------------------
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
    /// UFO��UI�{�^������������
    /// </summary>
    public void OnUIButtonUFO()
    {
        //�u�[������Ȃ�UFO���A�N�e�B�u�ɂ���
        if (boosManager.activeBoos.Count > 0)ufoEffectObj.SetActive(true);    
    }

    /// <summary>
    /// �M���^�b�v������
    /// </summary>
    public void OnPlateButton()
    {
        //�V���b�v��� & UFO��\��
        if (!shopUI.activeSelf && !ufoEffectObj.activeSelf)
        {
            //�V���b�v��ʕ\�����A�C�R����\��
            shopUI.SetActive(true);
            iconAreaUI.SetActive(false);
        }
    }

    /// <summary>
    /// �V���b�v��CLOSE�{�^���^�b�v��
    /// </summary>
    public void OnShopCloseButton()
    {
        shopAnim.SetBool("IsClose", true);
        iconAreaUI.SetActive(true);
    }

    /// <summary>
    /// BP�̉��Z���X�V����
    /// </summary>
    /// <param name="bp">���Z����BP</param>
    public void AddBooPoint(int bp)
    {
        gameManager.booPoint += bp;
        booPointText.text = gameManager.booPoint.ToString();
        booPointAnim.SetBool("IsBPChange", true);
        //0.25�b��ɃA�j���\�V�����I��
        Invoke("EndBPAnim", 0.25f);
    }

    /// <summary>
    /// BP�A�j���[�V�����I��
    /// </summary>
    void EndBPAnim()
    {
        booPointAnim.SetBool("IsBPChange", false);
    }
}
