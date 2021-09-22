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
            if (isStartUp) Debug.Log("�N��");
            else Debug.Log("�ĊJ");

            //�Z�[�u�f�[�^�̓ǂݍ���
            SaveData data = Load();

            //�Z�[�u�f�[�^�����邩�`�F�b�N�i�Ȃ���΃f�[�^������ԂŊJ�n�j
            if (data != null)
            {
                //�O��̃Z�[�u���Ԃ���̌o�ߎ��ԁi�b�j
                float spanTimeSec = SpanTimeSec(data);
                //�V�K�ǉ�����u�[�̐�
                int newBooCount = NewBooCount(data, spanTimeSec);
                //���[�h�����f�[�^���Q�[�����ɔ��f
                ReflectLoadData(data, spanTimeSec, newBooCount);
            }
         
            //�ȍ~�Q�[���J�����Ƃ���"�ĊJ"
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

    /// <summary>
    /// �O��̃Z�[�u���Ԃ���̌o�ߎ��Ԃ��擾�i�b�P�ʁj
    /// </summary>
    /// <param name="data">�O��̃Z�[�u�f�[�^</param>
    /// <returns>�o�ߎ��ԁi�b�j</returns>
    float SpanTimeSec(SaveData data)
    {
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

        return totalSec;
    }

    /// <summary>
    /// �V�K�ǉ�����i�Q�[�����Ă���Ԃɑ��������j�u�[�̐����擾
    /// </summary>
    /// <param name="spanTimeSec">�O��̃Z�[�u���Ԃ���̌o�ߎ��ԁi�b�j</param>
    /// <param name="data">�O��̃Z�[�u�f�[�^</param>
    /// <returns>�V�K�ǉ�����u�[�̐�</returns>
    int NewBooCount(SaveData data, float spanTimeSec)
    {
        //�o�ߎ��Ԃ��琶���\�ȃu�[�̐�
        int spanCount = (int)(spanTimeSec / BoosManager.Interval);
        //�u�[�̍ő吔����݂Ďc�萶���\�Ȑ�
        int spaceCount = BoosManager.BooMax - data.boos.Count;
        //�V�K�u�[�̐��������v�Z
        int newBooCount = Mathf.Min(spanCount, spaceCount);

        return newBooCount;
    }

    /// <summary>
    /// ���[�h�����f�[�^���Q�[�����ɔ��f
    /// </summary>
    /// <param name="data">�O��̃Z�[�u�f�[�^</param>
    /// <param name="newBooCount">�V�K�ǉ�����u�[�̐�</param>
    /// <param name="spanTimeSec">�O��̃Z�[�u���Ԃ���̌o�ߎ��ԁi�b�j</param>
    void ReflectLoadData(SaveData data, float spanTimeSec, int newBooCount)
    {
        if (!boosManager) return;

        //�N�����̂݊����u�[��S�Đ���
        if (isStartUp)
        {
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

        //�u�[�����p�̃C���^�[�o���v���^�C�}�[���A�O��Z�[�u������p��
        boosManager.intervalTimer = spanTimeSec % BoosManager.Interval;

    }
}
