using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ブーの管理
/// </summary>
public class BoosManager : MonoBehaviour
{
    /// <summary>
    /// ブーが出現するまでの間隔の計測用タイマー
    /// </summary>
    float intervalTimer = 0f;
    /// <summary>
    /// 生成したブーの数
    /// </summary>
    int booCounter = 0;
    /// <summary>
    /// ブーのプレファブ
    /// </summary>
    [SerializeField] GameObject booPre;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ブーが20匹(Max)だったらブーを生成しない
        if (booCounter == 20) return;

        //ブーを出現させるインターバル時間を計測
        intervalTimer += Time.deltaTime;
        Debug.Log(intervalTimer);

        //61秒間隔でBooを生成（出現）
        if (intervalTimer > 61f)
        {
            Instantiate(booPre);
            intervalTimer = 0f;
            booCounter++;
        }
    }
}
