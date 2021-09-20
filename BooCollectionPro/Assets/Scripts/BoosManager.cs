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
    public enum BooType
    {
        Normal, //�u�[�i�m�[�}���j
        Kabu,   //�J�u�\

        Count   //�u�[�̎�ނ̐�
    }


    /// <summary>
    /// �u�[���o������܂ł̊Ԋu�̌v���p�^�C�}�[
    /// </summary>
    [System.NonSerialized] public float intervalTimer = 0f;
    /// <summary>
    /// ���������u�[�̎�ދL�^�p
    /// �iBooType��int�ɕϊ����ċL�^����j
    /// </summary>
    [System.NonSerialized] public List<int> boos = new List<int>();
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
        if (boos.Count >= 20) return;

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
            boos.Add(booType);
            //�C���^�[�o�����Z�b�g
            intervalTimer = 0f;
        }
    }
}
