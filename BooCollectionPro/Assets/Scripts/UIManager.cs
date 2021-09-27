using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UFO���o�I�u�W�F�N�g
    /// </summary>
    [SerializeField] GameObject ufoEffectObj;

    BoosManager boosManager;


    // Start is called before the first frame update
    void Start()
    {
        boosManager = GetComponent<BoosManager>();
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
}
