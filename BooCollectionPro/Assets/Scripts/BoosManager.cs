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
    /// �u�[�̏o���Ԋu
    /// </summary>
    public const float Interval = 61f;
    /// <summary>
    /// �u�[�̏o���\�ő吔
    /// </summary>
    public const int BooMax = 20;

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
        //�u�[��Max��������u�[�𐶐����Ȃ�
        if (boos.Count >= BooMax) return;

        //�u�[���o��������C���^�[�o�����v��
        intervalTimer += Time.deltaTime;
        //Debug.Log(intervalTimer);

        //�o�����ԂɂȂ�����Boo�𐶐��i�o���j
        if (intervalTimer > Interval)
        {
            CreateBoo((int)BooType.Normal);
            //�C���^�[�o�����Z�b�g
            intervalTimer = 0f;
        }
    }

    /// <summary>
    /// �u�[�̐���
    /// </summary>
    /// <param name="booType">��������u�[�̎��</param>
    /// <param name="isRandom">��������u�[�̈ʒu�������_���Ȉʒu�ɂ��邩</param>
    public void CreateBoo(int booType, bool isRandom = false)
    {
        //�u�[�̐���
        GameObject boo = Instantiate(booPres[booType]);
        //�u�[�������_���Ȕz�u�ɂ���
        if (isRandom)
        {
            float x = Random.Range(-2.4f, 2.4f);
            float y = Random.Range(-2f, 1f);
            boo.transform.position = new Vector3(x, y, boo.transform.position.z);
        }
        //���������u�[�̎�ނ��L�^
        boos.Add(booType);
    }
}
