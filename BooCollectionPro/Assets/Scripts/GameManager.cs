using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// �Z�[�u�f�[�^�̍\��
/// </summary>
[System.Serializable]
public class SaveData
{
    public string saveTime;         //�Z�[�u�����i�����j
    public float booIntervalTime;   //�u�[�̏o���Ԋu�Ǘ�����
    public List<int> boos;          //�����ς݂̃u�[
}

/// <summary>
/// �Q�[���S�̂̊Ǘ�
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
    /// �A�v�����I�����鎞
    /// </summary>
    void OnApplicationQuit()
    {
        Debug.Log("�I��");
        Save();
    }

    /// <summary>
    /// �A�v�������f�A�ĊJ�i�N���j����鎞
    /// </summary>
    /// <param name="pauseStatus"></param>
    void OnApplicationPause(bool pauseStatus)
    {
        //�ꎞ��~��
        if (pauseStatus)
        {
            Debug.Log("���f");
            Save();
        }
        //�ĊJ���i�N�����j
        else
        {
            Debug.Log("�ĊJ");
            SaveData data = Load();
        }
    }

    /// <summary>
    /// �Q�[���f�[�^�̃Z�[�u
    /// </summary>
    public void Save()
    {
        //�Z�[�u�f�[�^�̃Z�b�g
        SaveData data = new SaveData();
        data.saveTime = System.DateTime.Now.ToString();
        data.booIntervalTime = boosManager.intervalTimer;
        data.boos = boosManager.boos;

        //�f�[�^��������
        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/savedata.json", false))
        {
            string jsonstr = JsonUtility.ToJson(data);
            Debug.Log(jsonstr);
            writer.Write(jsonstr);
            writer.Flush();
        }
    }

    /// <summary>
    /// �Q�[���f�[�^�̃��[�h
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
