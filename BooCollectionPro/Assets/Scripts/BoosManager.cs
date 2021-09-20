using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ブーの管理
/// </summary>
public class BoosManager : MonoBehaviour
{
    /// <summary>
    /// ブーの種類
    /// </summary>
    public enum BooType
    {
        Normal, //ブー（ノーマル）
        Kabu,   //カブ―

        Count   //ブーの種類の数
    }


    /// <summary>
    /// ブーが出現するまでの間隔の計測用タイマー
    /// </summary>
    [System.NonSerialized] public float intervalTimer = 0f;
    /// <summary>
    /// 生成したブーの種類記録用
    /// （BooTypeをintに変換して記録する）
    /// </summary>
    [System.NonSerialized] public List<int> boos = new List<int>();
    /// <summary>
    /// ブーのプレファブ(ブーのプレファブをBooType順でセット)
    /// </summary>
    [SerializeField] GameObject[] booPres = new GameObject[(int)BooType.Count];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ブーが20匹(Max)だったらブーを生成しない
        if (boos.Count >= 20) return;

        //ブーを出現させるインターバルを計測
        intervalTimer += Time.deltaTime;
        //Debug.Log(intervalTimer);

        //61秒間隔でBooを生成（出現）
        if (intervalTimer > 6f)
        {
            //ブーの生成
            int booType = (int)BooType.Normal;
            Instantiate(booPres[booType]);
            //生成したブーの種類を記録
            boos.Add(booType);
            //インターバルリセット
            intervalTimer = 0f;
        }
    }
}
