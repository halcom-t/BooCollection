using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �u�[���o������܂ł̊Ԋu�̌v���p�^�C�}�[
    /// </summary>
    float intervalTimer = 0f;

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
        //�u�[���o��������C���^�[�o�����Ԃ��v��
        intervalTimer += Time.deltaTime;
        Debug.Log(intervalTimer);

        //45�b�Ԋu��Boo�𐶐��i�o���j
        if(intervalTimer > 45f)
        {
            Instantiate(booPre, Vector3.zero, Quaternion.identity);
            intervalTimer = 0f;
        }
    }


}
