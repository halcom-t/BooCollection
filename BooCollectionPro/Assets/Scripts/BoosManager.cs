using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �S�u�[�̊Ǘ�
/// </summary>
public class BoosManager : MonoBehaviour
{
    /// <summary>
    /// �u�[�̎��
    /// </summary>
    enum BooType
    {
        Normal, //�u�[�i�m�[�}���j
        Kabu,   //�J�u�\

        Count   //�u�[�̎�ނ̐�
    }


    /// <summary>
    /// �u�[���o������܂ł̊Ԋu�̌v���p�^�C�}�[
    /// </summary>
    float intervalTimer = 0f;
    /// <summary>
    /// ���������u�[�̎�ދL�^�p
    /// �iBooType��int�ɕϊ����ċL�^����j
    /// </summary>
    List<int> nowBoos = new List<int>();
    /// <summary>
    /// �u�[�̃v���t�@�u(�u�[�̃v���t�@�u��BooType���ŃZ�b�g)
    /// </summary>
    [SerializeField] GameObject[] booPres = new GameObject[(int)BooType.Count];


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�u�[��20�C(Max)��������u�[�𐶐����Ȃ�
        if (nowBoos.Count >= 20) return;

        //�u�[���o��������C���^�[�o�����v��
        intervalTimer += Time.deltaTime;
        //Debug.Log(intervalTimer);

        //61�b�Ԋu��Boo�𐶐��i�o���j
        if (intervalTimer > 6f)
        {
            //�u�[�̐���
            int booType = (int)BooType.Normal;
            Instantiate(booPres[booType]);
            //���������u�[�̎�ނ��L�^
            nowBoos.Add(booType);
            //�C���^�[�o�����Z�b�g
            intervalTimer = 0f;
        }
    }
}
