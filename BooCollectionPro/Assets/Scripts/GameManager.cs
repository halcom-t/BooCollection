using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���S�̂̊Ǘ�
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
    /// �A�v�����I�����鎞
    /// </summary>
    void OnApplicationQuit()
    {
        Debug.Log("�I��");
    }

    /// <summary>
    /// �A�v�������f�A�ĊJ�i�N���j����鎞
    /// </summary>
    /// <param name="pauseStatus"></param>
    void OnApplicationPause(bool pauseStatus)
    {
        //�ꎞ��~
        if (pauseStatus)
        {
            Debug.Log("���f");
        }
        //�ĊJ���i�N�����j
        else
        {
            Debug.Log("�ĊJ");
        }
    }

}
