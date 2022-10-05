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
    private int jumpCount = 0;
    //プレイヤーのHP
    [SerializeField] private int PlayerHP = 5;
    //当たり判定フラグ
    private bool CollisionFlag = false;
    //点滅用変数
    [SerializeField] private SpriteRenderer sp;
    //攻撃の当たり判定用
    [SerializeField] private GameObject AttackArea;
    //アタックポイント格納用
    [SerializeField] private Transform attackPoint;

    [SerializeField] private LifeUI life;


    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void Update()
    {
        //ダメージ判定を受けているとき、点滅する
        if(CollisionFlag)
        {
            //Mathf.Absは絶対値を返す、Mathf.Sinは＋なら１，－なら0を返す
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //上の式で0と1が交互に来るので、それを透明度に入れて反転させている
            sp.color = new Color(0.0f, 1.0f, 1.0f, level);
        }
        //横移動
        transform.Translate(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
        //ジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow)&&this.jumpCount<1)
        {
            this.rbody2D.AddForce(transform.up * power);
            jumpCount++;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(AttackArea, attackPoint.position, Quaternion.identity);
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
        sp.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
    }

}
