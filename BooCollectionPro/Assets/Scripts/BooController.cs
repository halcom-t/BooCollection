using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ブーの基本動作
/// </summary>
public class BooController : MonoBehaviour
{

    Rigidbody2D rb;

    bool inBooArea = false;

    float actionTimer = 0f;

    const float Speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        actionTimer += Time.deltaTime;

        if (inBooArea && actionTimer>3f)
        {
            actionTimer = 0f;

            // 2/3の確率で停止
            if (Random.Range(0, 3) != 0)
            {
                rb.velocity = Vector2.zero;
                return;
            }

            //ランダム方向に歩く
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            rb.velocity = new Vector2(x, y) * Speed;

            float scaleX = Mathf.Abs(transform.localScale.x);
            float scaleY = transform.localScale.y;
            float scaleZ = transform.localScale.z;
            if (x > 0)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
            }
            else
            {
                transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BooArea") inBooArea = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BooArea")
        {
            inBooArea = false;
            this.rb.velocity *= -1;

            float x = this.transform.localScale.x * -1;
            float y = this.transform.localScale.y;
            float z = this.transform.localScale.z;
            this.transform.localScale = new Vector3(x,y,z);
        }
    }

}
