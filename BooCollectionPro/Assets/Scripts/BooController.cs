using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �u�[�̊�{����
/// </summary>
public class BooController : MonoBehaviour
{
    /// <summary>
    /// ������ɂ��邩
    /// </summary>
    bool inBooArea = false;

    /// <summary>
    /// UFO�ɋz�����܂ꒆ��
    /// </summary>
    [System.NonSerialized] public bool isInhale = false;

    /// <summary>
    /// �A�N�V�����Ԋu�̌v�Z�p�^�C�}�[
    /// </summary>
    float actionTimer = 0f;

    /// <summary>
    /// �ړ��X�s�[�h
    /// </summary>
    const float Speed = 0.5f;

    /// <summary>
    /// �u�[�̎��
    /// </summary>
    [System.NonSerialized] public BoosManager.BooType type = BoosManager.BooType.Normal;

    //�R���|�[�l���g---------------------------------
    Rigidbody2D rb;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        actionTimer += Time.deltaTime;

        //��������A�N�V�����\����(3�b)�ɂȂ�����
        if (inBooArea && actionTimer > 3f)
        {
            //�A�N�V�������ԃ��Z�b�g
            actionTimer = 0f;

            //�����؂�ւ��i2:1 = ��~:�ړ��j 
            if (Random.Range(0, 3) != 0)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                //�����_�������ɕ���
                RandomMove();
                //�����]��
                DirectionChange();
            }
        }

        //�A�j���[�V�����؂�ւ�
        anim.SetBool("IsWalk", rb.velocity.magnitude > 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //����ɓ������Ƃ�
        if (collision.gameObject.tag == "BooArea" && !isInhale)
        {
            inBooArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //���悩��o���Ƃ�
        if (collision.gameObject.tag == "BooArea" && !isInhale)
        {
            inBooArea = false;
            //���悩��o�Ă��܂����甽�Ε����Ɉړ����ĉ�����ɖ߂�
            this.rb.velocity *= -1;
            //�����]��
            DirectionChange();
        }
    }

    /// <summary>
    /// �����_���Ɉړ�����
    /// </summary>
    void RandomMove()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        this.rb.velocity = new Vector2(x, y).normalized * Speed;
    }

    /// <summary>
    /// �����]��
    /// </summary>
    void DirectionChange()
    {
        //���݂�scale�擾
        float scaleX = Mathf.Abs(this.transform.localScale.x);
        float scaleY = this.transform.localScale.y;
        float scaleZ = this.transform.localScale.z;
        //���E�ǂ���Ɉړ��������肵�ĕ����]��
        if (this.rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        }
        else
        {
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }

    /// <summary>
    /// UFO�ɋz�����܂�鏈��
    /// </summary>
    /// <param name="InhalePos">�u�[���z�����܂�Ă����ʒu</param>
    public void Inhale(Vector3 InhalePos)
    {
        isInhale = true;

        //UFO�̕����Ɉړ��i�z�����܂��j
        Vector3 directionUFO = InhalePos - this.transform.position;
        rb.velocity = directionUFO.normalized * 3;

        //BooController����iOnTrigger�֘A�͐���s�j
        this.enabled = false;
    }

    /// <summary>
    /// �X�^�[�g�n�_(��ʊO)����̈ړ��J�n����
    /// </summary>
    public void MoveStartPoint()
    {
        inBooArea = false;
        rb.velocity = Vector2.left * Speed;
        DirectionChange();
    }

}
