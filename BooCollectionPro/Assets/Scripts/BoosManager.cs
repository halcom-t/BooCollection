using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �S�u�[�̊Ǘ�
/// </summary>
public class BoosManager : MonoBehaviour
{
    /// <summary>
    /// �u�[���o������܂ł̊Ԋu�̌v���p�^�C�}�[
    /// </summary>
    float intervalTimer = 0f;
    /// <summary>
    /// ���������u�[�̐�
    /// </summary>
    int booCounter = 0;
    /// <summary>
    /// �u�[�̃v���t�@�u
    /// </summary>
    [SerializeField] GameObject booPre;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�u�[��20�C(Max)��������u�[�𐶐����Ȃ�
        if (booCounter == 20) return;

        //�u�[���o��������C���^�[�o�����Ԃ��v��
        intervalTimer += Time.deltaTime;
        Debug.Log(intervalTimer);

        //61�b�Ԋu��Boo�𐶐��i�o���j
        if (intervalTimer > 61f)
        {
            Instantiate(booPre);
            intervalTimer = 0f;
            booCounter++;
        }
    }
}
