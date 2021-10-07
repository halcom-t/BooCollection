using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    /// <summary>
    /// ���C���J����obj
    /// </summary>
    [SerializeField] GameObject mainCamera;

    //�R���|�[�l���g----------------------------------------
    UIManager uiManager;
    Animator anim;


    void Awake()
    {
        uiManager = mainCamera.GetComponent<UIManager>();
        anim = GetComponent<Animator>();
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
    /// �M���^�b�v������
    /// </summary>
    public void OnPlate()
    {
        //�V���b�v��ʂ��J��
        uiManager.OpenShop();
        //�M�I���A�j���\�V�����J�n
        anim.SetBool("IsSelected", true);
    }
}
