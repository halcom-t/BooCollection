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
    /// �u�[�̃v���t�@�u
    /// </summary>
    [SerializeField] GameObject booPre;

    /// <summary>
    /// �u�[���o���\�Ȉʒu
    /// </summary>
    List<Vector3> booPosList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�u�[���o��������C���^�[�o�����Ԃ��v��
        intervalTimer += Time.deltaTime;
        //Debug.Log(intervalTimer);

        //60�b�Ԋu��Boo�𐶐��i�o���j
        if (intervalTimer > 51f)
        {
            Instantiate(booPre);
            intervalTimer = 0f;
        }
    }
}
