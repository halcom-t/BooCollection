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
    /// ブーのプレファブ
    /// </summary>
    [SerializeField] GameObject booPre;

    /// <summary>
    /// ブーが出現可能な位置
    /// </summary>
    List<Vector3> booPosList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ブーを出現させるインターバル時間を計測
        intervalTimer += Time.deltaTime;
        //Debug.Log(intervalTimer);

        //60秒間隔でBooを生成（出現）
        if (intervalTimer > 51f)
        {
            Instantiate(booPre);
            intervalTimer = 0f;
        }
    }
}
