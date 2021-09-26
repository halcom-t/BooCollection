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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //スポーン位置が画面外なら、左（可動域内）に移動
        if (transform.position.x > 3)
        {
            rb.velocity = Vector2.left * Speed;
        }      
    }

    // Update is called once per frame
    void Update()
    {
        actionTimer += Time.deltaTime;

        //可動域内＆アクション可能時間(3秒)になったら
        if (inBooArea && actionTimer>3f)
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
        if (collision.gameObject.tag == "BooArea")
        {
            inBooArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //可動域から出たとき
        if (collision.gameObject.tag == "BooArea")
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

}
