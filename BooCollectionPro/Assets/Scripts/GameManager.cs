using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ブーが出現するまでの間隔の計測用タイマー
    /// </summary>
    float intervalTimer = 0f;

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
        //ブーを出現させるインターバル時間を計測
        intervalTimer += Time.deltaTime;
        Debug.Log(intervalTimer);

        //45秒間隔でBooを生成（出現）
        if(intervalTimer > 45f)
        {
            Instantiate(booPre, Vector3.zero, Quaternion.identity);
            intervalTimer = 0f;
        }
    }


}
