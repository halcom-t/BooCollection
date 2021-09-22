using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UFO演出オブジェクト
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
    /// UFOのUIボタンを押した時
    /// </summary>
    public void OnUIButtonUFO()
    {
        ufoEffectObj.SetActive(true);
    }
}
