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
    /// BPのUI
    /// </summary>
    [SerializeField] GameObject booPointUI;
    /// <summary>
    /// BP（Text）
    /// </summary>
    [SerializeField] Text booPointText;

    //コンポーネント----------------------------
    GameManager gameManager;
    BoosManager boosManager;

    /// <summary>
    /// BPのUIのアニメーター
    /// </summary>
    Animator booPointAnim;


    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        boosManager = GetComponent<BoosManager>();
        booPointAnim = booPointUI.GetComponent<Animator>();
    }

    void Start()
    {

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
        if (boosManager.activeBoos.Count > 0)
        {
            ufoEffectObj.SetActive(true);
        }
        
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
        Invoke("EndBPAnim", 0.25f);
    }

    void EndBPAnim()
    {
        booPointAnim.SetBool("IsBPChange", false);
    }
}
