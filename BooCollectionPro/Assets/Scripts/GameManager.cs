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

    /// <summary>
    /// �Z�[�u�f�[�^�̃p�X
    /// </summary>
    string SavedataPath { get; } = "/savedata.json";

    /// <summary>
    /// �N�����@�Ftrue�N��/false�ĊJ�@�iOnApplicationPause()�ōĊJ���N�����𔻒肷��p�j
    /// </summary>
    bool isStartUp = true;

    //�R���|�[�l���g-------------------------------
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
        //�ĊJor�N����
        else
        {
            if (isStartUp)
            {
                Debug.Log("�N��");
            }
            else
            {
                Debug.Log("�ĊJ");
            }

            //�Z�[�u�f�[�^�̓ǂݍ���
            SaveData data = Load();

            //�Z�[�u�f�[�^���Ȃ���Ώ�����ԂŊJ�n
            if (data == null)
            {
                isStartUp = false;
                return;
            }

            //�O��Z�[�u������o�߂������Ԃ��擾
            System.DateTime saveTime = System.DateTime.Parse(data.saveTime);
            System.DateTime nowTime = System.DateTime.Now;
            System.TimeSpan span = nowTime - saveTime;

            //�o�߂������Ԃ����b���v�Z
            int secD = span.Days * 24 * 60 * 60;    //����
            int secH = span.Hours * 60 * 60;        //��
            int secM = span.Minutes * 60;           //��
            int sec = span.Seconds;                 //�b
            float totalSec = secD + secH + secM + sec + data.booIntervalTime;

            //�o�ߎ��Ԃ��琶���\�ȃu�[�̐�
            int spanCount = (int)(totalSec / BoosManager.Interval);
            //�u�[�̍ő吔����݂Ďc�萶���\�Ȑ�
            int spaceCount = BoosManager.BooMax - data.boos.Count;
            //�V�K�u�[�̐��������v�Z
            int newBooCount = Mathf.Min(spanCount, spaceCount);

            //�Z�[�u�f�[�^���f
            if (boosManager) {

                //�N�����̂�
                if (isStartUp)
                {
                    //�����u�[��S�Đ���
                    foreach (int boo in data.boos)
                    {
                        boosManager.CreateBoo(boo);
                    }
                }

                //�V�K�u�[��S�č쐬
                for (int i = 0; i < newBooCount; i++)
                {
                    boosManager.CreateBoo((int)BoosManager.BooType.Normal);
                }

                //�u�[�̃C���^�[�o����O��Z�[�u������p��
                boosManager.intervalTimer = totalSec % BoosManager.Interval;
            }

            //�ȍ~�Q�[���J�����Ƃ��͍ĊJ����
            isStartUp = false;

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
        if (boosManager)
        {
            data.booIntervalTime = boosManager.intervalTimer;
            data.boos = boosManager.boos;
        }      

        //�f�[�^��������
        using (StreamWriter writer = new StreamWriter(Application.dataPath + SavedataPath, false))
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

        //�Z�[�u�f�[�^���Ȃ���
        if (!File.Exists(Application.dataPath + SavedataPath))
        {
            Debug.Log("�Z�[�u�f�[�^������܂���B");
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

}
