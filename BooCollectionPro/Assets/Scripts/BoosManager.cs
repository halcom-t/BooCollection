using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �u�[�̏��
/// </summary>
public struct BooData
{
    public GameObject obj;              //�u�[�̃I�u�W�F�N�g
    public BooController controller;    //�u�[�ɃA�^�b�`����Ă�R���g���[���[�iBooController�j
}


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
    public const float Interval = 6f;
    /// <summary>
    /// �u�[�̏o���\�ő吔
    /// </summary>
    public const int BooMax = 20;

    /// <summary>
    /// �u�[���o������܂ł̊Ԋu�̌v���p�^�C�}�[
    /// </summary>
    [System.NonSerialized] public float intervalTimer = 0f;

    /// <summary>
    /// �A�N�e�B�u�ȃu�[
    /// </summary>
    [System.NonSerialized] public List<BooData> activeBoos = new List<BooData>();
    /// <summary>
    /// ��A�N�e�B�u�ȃu�[
    /// </summary>
    [System.NonSerialized] public List<BooData> notActiveBoos = new List<BooData>();

    /// <summary>
    /// �u�[�̃v���t�@�u
    /// </summary>
    [SerializeField] GameObject booPre;

    [SerializeField] GameObject UFOEffectObj;
    UFOController ufoController;


    void Awake()
    {
        ufoController = UFOEffectObj.GetComponent<UFOController>();

        //�u�[�𐶐�
        for (int i = 0; i < BooMax; i++)
        {
            BooData boo = new BooData();
            boo.obj = Instantiate(booPre);
            boo.controller = boo.obj.GetComponent<BooController>();
            notActiveBoos.Add(boo);
            notActiveBoos[i].obj.SetActive(false);
        }      
    }

    void Start()
    {

    }

    void Update()
    {
        //UFO�ɋz�����܂ꂽ�u�[���`�F�b�N
        CheckInhaleFinish();

        //UFO���o�����͏������Ȃ�
        if (ufoController.isUFOActive) return;

        //�A�N�e�B�u�ȃu�[��������ɒB���Ă���Ȃ珈�����Ȃ�
        if (activeBoos.Count >= BooMax)
        {
            //�C���^�[�o�����Z�b�g
            intervalTimer = 0f;
            return;
        } 

        //�u�[���o��������C���^�[�o�����v��
        intervalTimer += Time.deltaTime;
        //Debug.Log(intervalTimer);

        //�o�����ԂɂȂ�����Boo���A�N�e�B�u�ɂ���i�o���j
        if (intervalTimer > Interval)
        {
            SetBooActive((int)BooType.Normal);
            //�C���^�[�o�����Z�b�g
            intervalTimer = 0f;
        }
    }

    /// <summary>
    /// �u�[���A�N�e�B�u�ɂ���
    /// </summary>
    /// <param name="booType">�u�[�̎��</param>
    /// <param name="isRandom">�u�[�̈ʒu�������_���Ȉʒu�ɂ��邩</param>
    public void SetBooActive(int booType, bool isRandom = false)
    {
        //��A�N�e�B�u�ȃu�[�����Ȃ���Ώ������Ȃ�
        if (notActiveBoos.Count <= 0) return;

        //�u�[���A�N�e�B�u������ނ̐ݒ�
        BooData boo = notActiveBoos[0];
        boo.obj.SetActive(true);
        boo.controller.type = (BooType)booType;

        if (isRandom)
        {
            //�����_���Ȕz�u�ɂ���
            float x = Random.Range(-2.4f, 2.4f);
            float y = Random.Range(-2f, 1f);
            boo.obj.transform.position = new Vector3(x, y, boo.obj.transform.position.z);
        }
        else
        {
            //��ʊO�ɔz�u�ɂ���
            boo.obj.transform.position = booPre.transform.position;
            boo.controller.MoveStartPoint();
        }

        //�u�[�Ǘ��p���X�g�̍X�V
        activeBoos.Add(boo);
        notActiveBoos.RemoveAt(0);

        Debug.Log("activeBoos:" + activeBoos.Count + "/notActiveBoos:" + notActiveBoos.Count);
    }

    /// <summary>
    /// UFO�ɋz�����܂ꂽ�u�[�̏���
    /// </summary>
    void CheckInhaleFinish()
    {
        foreach (BooData boo in activeBoos)
        {
            //�u�[��UFO�̈ʒu�܂ŏ㏸���Ă������A�N�e�B�u�ɂ���
            if (boo.obj.transform.position.y > 3.8f)
            {
                boo.obj.SetActive(false);
                boo.controller.isInhale = false;
                activeBoos.Remove(boo);
                notActiveBoos.Add(boo);
                //activeBoos�̃T�C�Y�ύX�ɂ��G���[�΍�Ŕ�����
                return;
            }
        }
    }

}
