using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// セーブデータの構造
/// </summary>
[System.Serializable]
public class SaveData
{
    public string saveTime;         //セーブ時刻（現実）
    public float booIntervalTime;   //ブーの出現間隔管理時間
    public List<int> boos;          //生成済みのブー
}

/// <summary>
/// ゲーム全体の管理
/// </summary>
public class GameManager : MonoBehaviour
{

    /// <summary>
    /// セーブデータのパス
    /// </summary>
    string SavedataPath { get; } = "/savedata.json";

    /// <summary>
    /// 起動か　：true起動/false再開　（OnApplicationPause()で再開か起動かを判定する用）
    /// </summary>
    bool isStartUp = true;

    //コンポーネント-------------------------------
    BoosManager boosManager = null;



    void Awake()
    {
        boosManager = GetComponent<BoosManager>();
    }

    void Start()
    {
    }

    void Update()
    {

    }

    /// <summary>
    /// アプリが終了する時
    /// </summary>
    void OnApplicationQuit()
    {
        Debug.Log("終了");
        Save();
    }

    /// <summary>
    /// アプリが中断、再開（起動）される時
    /// </summary>
    /// <param name="pauseStatus"></param>
    void OnApplicationPause(bool pauseStatus)
    {
        //一時停止時
        if (pauseStatus)
        {
            Debug.Log("中断");
            Save();
        }
        //再開or起動時
        else
        {
            if (isStartUp) Debug.Log("起動");
            else Debug.Log("再開");

            //セーブデータの読み込み
            SaveData data = Load();

            //セーブデータがあるかチェック（なければデータ初期状態で開始）
            if (data != null)
            {
                //前回のセーブ時間からの経過時間（秒）
                float spanTimeSec = SpanTimeSec(data);
                //新規追加するブーの数
                int newBooCount = NewBooCount(data, spanTimeSec);
                //ロードしたデータをゲーム内に反映
                ReflectLoadData(data, spanTimeSec, newBooCount);
            }
         
            //以降ゲーム開いたときは"再開"
            isStartUp = false;
        }
    }

    /// <summary>
    /// ゲームデータのセーブ
    /// </summary>
    public void Save()
    {
        //セーブデータのセット
        SaveData data = new SaveData();
        data.saveTime = System.DateTime.Now.ToString();
        if (boosManager)
        {
            data.booIntervalTime = boosManager.intervalTimer;
            data.boos = boosManager.boos;
        }

        //データ書き込み
        using (StreamWriter writer = new StreamWriter(Application.dataPath + SavedataPath, false))
        {
            string jsonstr = JsonUtility.ToJson(data);
            Debug.Log(jsonstr);
            writer.Write(jsonstr);
            writer.Flush();
        }
    }

    /// <summary>
    /// ゲームデータのロード
    /// </summary>
    public SaveData Load()
    {
        SaveData data = null;

        //セーブデータがない時
        if (!File.Exists(Application.dataPath + SavedataPath))
        {
            Debug.Log("セーブデータがありません。");
            return data;
        }

        using (StreamReader reader = new StreamReader(Application.dataPath + SavedataPath))
        {
            string datastr = "";
            datastr = reader.ReadToEnd();
            data = JsonUtility.FromJson<SaveData>(datastr);
        }
        return data;
    }

    /// <summary>
    /// 前回のセーブ時間からの経過時間を取得（秒単位）
    /// </summary>
    /// <param name="data">前回のセーブデータ</param>
    /// <returns>経過時間（秒）</returns>
    float SpanTimeSec(SaveData data)
    {
        //前回セーブ時から経過した時間を取得
        System.DateTime saveTime = System.DateTime.Parse(data.saveTime);
        System.DateTime nowTime = System.DateTime.Now;
        System.TimeSpan span = nowTime - saveTime;

        //経過した時間が何秒か計算
        int secD = span.Days * 24 * 60 * 60;    //日数
        int secH = span.Hours * 60 * 60;        //時
        int secM = span.Minutes * 60;           //分
        int sec = span.Seconds;                 //秒
        float totalSec = secD + secH + secM + sec + data.booIntervalTime;

        return totalSec;
    }

    /// <summary>
    /// 新規追加する（ゲーム閉じている間に増加した）ブーの数を取得
    /// </summary>
    /// <param name="spanTimeSec">前回のセーブ時間からの経過時間（秒）</param>
    /// <param name="data">前回のセーブデータ</param>
    /// <returns>新規追加するブーの数</returns>
    int NewBooCount(SaveData data, float spanTimeSec)
    {
        //経過時間から生成可能なブーの数
        int spanCount = (int)(spanTimeSec / BoosManager.Interval);
        //ブーの最大数からみて残り生成可能な数
        int spaceCount = BoosManager.BooMax - data.boos.Count;
        //新規ブーの生成数を計算
        int newBooCount = Mathf.Min(spanCount, spaceCount);

        return newBooCount;
    }

    /// <summary>
    /// ロードしたデータをゲーム内に反映
    /// </summary>
    /// <param name="data">前回のセーブデータ</param>
    /// <param name="newBooCount">新規追加するブーの数</param>
    /// <param name="spanTimeSec">前回のセーブ時間からの経過時間（秒）</param>
    void ReflectLoadData(SaveData data, float spanTimeSec, int newBooCount)
    {
        if (!boosManager) return;

        //起動時のみ既存ブーを全て生成
        if (isStartUp)
        {
            foreach (int boo in data.boos)
            {
                boosManager.CreateBoo(boo);
            }
        }

        //新規ブーを全て作成
        for (int i = 0; i < newBooCount; i++)
        {
            boosManager.CreateBoo((int)BoosManager.BooType.Normal);
        }

        //ブー生成用のインターバル計測タイマーを、前回セーブ時から継続
        boosManager.intervalTimer = spanTimeSec % BoosManager.Interval;

    }
}
