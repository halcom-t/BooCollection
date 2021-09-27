using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UFO演出オブジェクト
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
    /// UFOのUIボタンを押した時
    /// </summary>
    public void OnUIButtonUFO()
    {
        if (boosManager.activeBoos.Count > 0)
        {
            ufoEffectObj.SetActive(true);
        }
        
    }
}
