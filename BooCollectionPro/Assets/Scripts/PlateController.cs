using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    /// <summary>
    /// メインカメラobj
    /// </summary>
    [SerializeField] GameObject mainCamera;

    //コンポーネント----------------------------------------
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
    /// 皿をタップした時
    /// </summary>
    public void OnPlate()
    {
        //ショップ画面を開く
        uiManager.OpenShop();
        //皿選択アニメ―ション開始
        anim.SetBool("IsSelected", true);
    }
}
