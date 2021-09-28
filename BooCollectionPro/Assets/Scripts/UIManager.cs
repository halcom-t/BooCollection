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
    /// BP��UI
    /// </summary>
    [SerializeField] GameObject booPointUI;
    /// <summary>
    /// BP�iText�j
    /// </summary>
    [SerializeField] Text booPointText;

    //�R���|�[�l���g----------------------------
    GameManager gameManager;
    BoosManager boosManager;

    /// <summary>
    /// BP��UI�̃A�j���[�^�[
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
    /// UFO��UI�{�^������������
    /// </summary>
    public void OnUIButtonUFO()
    {
        if (boosManager.activeBoos.Count > 0)
        {
            ufoEffectObj.SetActive(true);
        }
        
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
        Invoke("EndBPAnim", 0.25f);
    }

    void EndBPAnim()
    {
        booPointAnim.SetBool("IsBPChange", false);
    }
}
