using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブーの基本動作
/// </summary>
public class BooController : MonoBehaviour
{
    /// <summary>
    /// 可動域内にいるか
    /// </summary>
    bool inBooArea = false;

    /// <summary>
    /// UFOに吸い込まれ中か
    /// </summary>
    [System.NonSerialized] public bool isInhale = false;

    /// <summary>
    /// アクション間隔の計算用タイマー
    /// </summary>
    float actionTimer = 0f;

    /// <summary>
    /// 移動スピード
    /// </summary>
    const float Speed = 0.5f;

    /// <summary>
    /// ブーの種類
    /// </summary>
    [System.NonSerialized] public BoosManager.BooType type = BoosManager.BooType.Normal;

    //コンポーネント---------------------------------
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

        //可動域内＆アクション可能時間(3秒)になったら
        if (inBooArea && actionTimer > 3f)
        {
            //アクション時間リセット
            actionTimer = 0f;

            //挙動切り替え（2:1 = 停止:移動） 
            if (Random.Range(0, 3) != 0)
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                //ランダム方向に歩く
                RandomMove();
                //方向転換
                DirectionChange();
            }
        }

        //アニメーション切り替え
        anim.SetBool("IsWalk", rb.velocity.magnitude > 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //可動域に入ったとき
        if (collision.gameObject.tag == "BooArea" && !isInhale)
        {
            inBooArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //可動域から出たとき
        if (collision.gameObject.tag == "BooArea" && !isInhale)
        {
            inBooArea = false;
            //可動域から出てしまったら反対方向に移動して可動域内に戻る
            this.rb.velocity *= -1;
            //方向転換
            DirectionChange();
        }
    }

    /// <summary>
    /// ランダムに移動処理
    /// </summary>
    void RandomMove()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        this.rb.velocity = new Vector2(x, y).normalized * Speed;
    }

    /// <summary>
    /// 方向転換
    /// </summary>
    void DirectionChange()
    {
        //現在のscale取得
        float scaleX = Mathf.Abs(this.transform.localScale.x);
        float scaleY = this.transform.localScale.y;
        float scaleZ = this.transform.localScale.z;
        //左右どちらに移動中か判定して方向転換
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
    /// UFOに吸い込まれる処理
    /// </summary>
    /// <param name="InhalePos">ブーが吸い込まれていく位置</param>
    public void Inhale(Vector3 InhalePos)
    {
        isInhale = true;

        //UFOの方向に移動（吸い込まれる）
        Vector3 directionUFO = InhalePos - this.transform.position;
        rb.velocity = directionUFO.normalized * 3;

        //BooController制御（OnTrigger関連は制御不可）
        this.enabled = false;
    }

    /// <summary>
    /// スタート地点(画面外)からの移動開始処理
    /// </summary>
    public void MoveStartPoint()
    {
        inBooArea = false;
        rb.velocity = Vector2.left * Speed;
        DirectionChange();
    }

}
