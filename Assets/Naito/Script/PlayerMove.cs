using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの移動に関するスクリプト
public class PlayerMove : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField] private float speed = 8.0f;
    //プレイヤーの移動範囲
    //public float moveableRange = 30.0f;
    //プレイヤーのジャンプ力
    [SerializeField] private float power = 1.0f;
    //プレイヤーのリジットボディ
    private Rigidbody2D rbody2D;
    //プレイヤーがジャンプしていいかの処理
    [SerializeField] private int jumpCount = 0;
    //プレイヤーのHP
    [SerializeField] private int PlayerHP = 5;
    //当たり判定フラグ
    private bool CollisionFlag = false;
    //点滅用変数
    [SerializeField] private SpriteRenderer sp;
    //ライフUI用
    [SerializeField] private LifeUI life;
    //ホーミングオブジェクト
    [SerializeField] private GameObject HomingObj;
    //攻撃エリア用
    [SerializeField] private PlayerAttack AttackArea;
    //移動方向格納用
    private float x_val;


    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        x_val = Input.GetAxis("Horizontal");
        //ダメージ判定を受けているとき、点滅する
        if(CollisionFlag)
        {
            //Mathf.Absは絶対値を返す、Mathf.Sinは＋なら１，－なら0を返す
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //上の式で0と1が交互に来るので、それを透明度に入れて反転させている
            sp.color = new Color(1.0f, 1.0f, 1.0f, level);
        }
        if(x_val>0)
        {
            //右を向く
            transform.localScale = new Vector3(-0.07f, 0.07f, 1);
        }
        else if(x_val<0)
        {
            //左を向く
            transform.localScale = new Vector3(0.07f, 0.07f, 1);
        }
        //横移動
        transform.Translate(x_val * speed * Time.deltaTime, 0, 0);
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow)&&this.jumpCount<1)
        {
            this.rbody2D.AddForce(transform.up * power);
            jumpCount++;
        }
        //攻撃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackArea.AttackAreaCreate();
        }
        //プレイヤーの移動制限
        //transform.position = new Vector2(Mathf.Clamp(
        //    transform.position.x, -moveableRange, moveableRange),
        //    transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            //ダメージフラグがfalseだったらダメージ
            if(CollisionFlag==false)
            PlayerDamage();
        }
    }

    void PlayerDamage()
    {
        // 体力減少
        life.LossLife();
        CollisionFlag = true;
        //ダメージ判定が終わった後、3秒後に無敵を解除する
        Invoke("InvincibleEnd", 3.0f);
    }

    void InvincibleEnd()
    {
        CollisionFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

}
