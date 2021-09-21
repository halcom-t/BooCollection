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

    BoosManager boosManager;

    void Start()
    {
        boosManager = GetComponent<BoosManager>();
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
        //再開時（起動時）
        else
        {
            Debug.Log("再開");
            SaveData data = Load();
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
        data.booIntervalTime = boosManager.intervalTimer;
        data.boos = boosManager.boos;

        //データ書き込み
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/savedata.json", false))
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
        using (StreamReader reader = new StreamReader(Application.dataPath + "/savedata.json"))
        {
            string datastr = "";
            datastr = reader.ReadToEnd();
            data = JsonUtility.FromJson<SaveData>(datastr);
        };
        return data;
    }

}
