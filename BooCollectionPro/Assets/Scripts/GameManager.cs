using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体の管理
/// </summary>
public class GameManager : MonoBehaviour
{
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
    }

    /// <summary>
    /// アプリが中断、再開（起動）される時
    /// </summary>
    /// <param name="pauseStatus"></param>
    void OnApplicationPause(bool pauseStatus)
    {
        //一時停止
        if (pauseStatus)
        {
            Debug.Log("中断");
        }
        //再開時（起動時）
        else
        {
            Debug.Log("再開");
        }
    }

}
