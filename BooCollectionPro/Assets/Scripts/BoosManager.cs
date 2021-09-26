using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ブーの情報
/// </summary>
public struct BooData
{
    public GameObject obj;              //ブーのオブジェクト
    public BooController controller;    //ブーにアタッチされてるコントローラー（BooController）
}


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
    /// ブーの出現間隔
    /// </summary>
    public const float Interval = 6f;
    /// <summary>
    /// ブーの出現可能最大数
    /// </summary>
    public const int BooMax = 20;

    /// <summary>
    /// ブーが出現するまでの間隔の計測用タイマー
    /// </summary>
    [System.NonSerialized] public float intervalTimer = 0f;

    /// <summary>
    /// アクティブなブー
    /// </summary>
    [System.NonSerialized] public List<BooData> activeBoos = new List<BooData>();
    /// <summary>
    /// 非アクティブなブー
    /// </summary>
    List<BooData> notActiveBoos = new List<BooData>();

    /// <summary>
    /// ブーのプレファブ
    /// </summary>
    [SerializeField] GameObject booPre;


    void Awake()
    {
        for (int i = 0; i < BooMax; i++)
        {
            BooData boo = new BooData();
            boo.obj = Instantiate(booPre);
            boo.controller = boo.obj.GetComponent<BooController>();
            notActiveBoos.Add(boo);
            notActiveBoos[i].obj.SetActive(false);
        }      
    }

    void Start()
    {

    }

    void Update()
    {
        //アクティブなブーが上限数なら処理しない
        if (activeBoos.Count >= BooMax)
        {
            //インターバルリセット
            intervalTimer = 0f;
            return;
        } 

        //ブーを出現させるインターバルを計測
        intervalTimer += Time.deltaTime;
        //Debug.Log(intervalTimer);

        //出現時間になったらBooをアクティブにする（出現）
        if (intervalTimer > Interval)
        {
            SetBooActive((int)BooType.Normal);
            //インターバルリセット
            intervalTimer = 0f;
        }
    }

    /// <summary>
    /// ブーをアクティブにする
    /// </summary>
    /// <param name="booType">ブーの種類</param>
    /// <param name="isRandom">ブーの位置をランダムな位置にするか</param>
    public void SetBooActive(int booType, bool isRandom = false)
    {
        //非アクティブなブーがいなければ処理しない
        if (notActiveBoos.Count <= 0) return;

        //ブーをアクティブ化＆種類の設定
        BooData boo = notActiveBoos[0];
        boo.obj.SetActive(true);
        boo.controller.type = (BooType)booType;

        //ブーをランダムな配置にする
        if (isRandom)
        {
            float x = Random.Range(-2.4f, 2.4f);
            float y = Random.Range(-2f, 1f);
            boo.obj.transform.position = new Vector3(x, y, boo.obj.transform.position.z);
        }
        else
        {
            boo.obj.transform.position = booPre.transform.position;
        }

        //ブー管理用リストの更新
        activeBoos.Add(boo);
        notActiveBoos.RemoveAt(0);

        Debug.Log("activeBoos:" + activeBoos.Count + "/notActiveBoos:" + notActiveBoos.Count);
    }

}
