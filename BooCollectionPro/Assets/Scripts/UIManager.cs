using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UFO���o�I�u�W�F�N�g
    /// </summary>
    [SerializeField] GameObject ufoEffectObj;


    // Start is called before the first frame update
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
        ufoEffectObj.SetActive(true);
    }
}
