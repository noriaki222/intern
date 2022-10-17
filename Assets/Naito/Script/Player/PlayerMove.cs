using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーの移動に関するスクリプト
public class PlayerMove : MonoBehaviour
{
    //プレイヤーの移動速度
    [SerializeField] private float Dashspeed = 6.0f;
    //プレイヤーの徒歩スピード
    [SerializeField] private float Walkspeed = 2.0f;
    //プレイヤーの移動範囲
    //public float moveableRange = 30.0f;
    //プレイヤーのジャンプ力
    [SerializeField] private float power = 1.0f;
    //プレイヤーのリジットボディ
    private Rigidbody2D rbody2D;
    //プレイヤーがジャンプしていいかの処理
    [SerializeField] private int jumpCount = 0;
    //プレイヤーのHP
    //[SerializeField] private int PlayerHP = 5;
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
    //罠にかかっているかの判断用
    [SerializeField] private bool BoolTrap = false;
    //プレイヤーがトラップから抜けるのに必要なクリック数
    private int Trapcnt;
    //アニメーションのインスタンスを受け取る用
    private Animator anim;
    //SE出す用
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    //色々揺らす用
    [SerializeField] private Shake shake;
    //攻撃中、余計な動作をしない用
    private bool NotMove = false;
    //攻撃が終わったと判断する用
    private float AttackCnt;


    private void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    //Update is called once per frame
    void Update()
    {
        if (CollisionFlag)
        {
            //ダメージ判定を受けているとき、点滅する
            //Mathf.Absは絶対値を返す、Mathf.Sinは＋なら１，－なら0を返す
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //上の式で0と1が交互に来るので、それを透明度に入れて反転させている
            sp.color = new Color(1.0f, 1.0f, 1.0f, level);
        }
        if (BoolTrap == false && NotMove == false)
        {
            rbody2D.isKinematic = false;
            x_val = Input.GetAxis("Horizontal");
            Debug.Log(x_val);
            if (x_val > 0)
            {
                //右を向く
                transform.localScale = new Vector3(0.1f, 0.1f, 1);
                //歩く
                transform.Translate(Walkspeed * Time.deltaTime, 0, 0);
                if (x_val >= 0.8f)
                {
                    //走る
                    transform.Translate(Dashspeed * Time.deltaTime, 0, 0);
                }
            }
            else if (x_val < 0)
            {
                //左を向く
                transform.localScale = new Vector3(-0.1f, 0.1f, 1);
                //歩く
                transform.Translate(-Walkspeed * Time.deltaTime, 0, 0);
                if (x_val <= -0.8f)
                {
                    //走る
                    transform.Translate(-Dashspeed * Time.deltaTime, 0, 0);
                }
            }
            //横移動
            //transform.Translate(x_val * speed * Time.deltaTime, 0, 0);
            //ジャンプ
            if ((Input.GetKeyDown(KeyCode.UpArrow) && this.jumpCount < 1) || ((Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") ||
                Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3")) && this.jumpCount < 1))
            {
                this.rbody2D.AddForce(transform.up * power);
                anim.SetBool("JumpFlag", true);
                jumpCount++;
                Invoke("JumpFlagReset", 0.1f);
            }
            //攻撃
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
            {
                anim.SetBool("AttackFlag", true);
                NotMove = true;
                Invoke("StartAttack", 0.45f);
            }
            //プレイヤーの移動制限
            //transform.position = new Vector2(Mathf.Clamp(
            //    transform.position.x, -moveableRange, moveableRange),
            //    transform.position.y);
        }
        else if(BoolTrap == true && NotMove == false)
        {
            //rbody2D.velocity = Vector3.zero;
            //rbody2D.isKinematic = true;
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("joystick button 1") ||
                Input.GetKeyDown("joystick button 2") || Input.GetKeyDown("joystick button 3") || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
            {
                Trapcnt++;
            }
            if(Trapcnt>10)
            {
                BoolTrap = false;
            }
        }
        else if((BoolTrap == false && NotMove == true)|| (BoolTrap == true && NotMove == true))
        {
            AttackCnt += Time.deltaTime;
            if(AttackCnt > 0.5f)
            {
                NotMove = false;
                AttackCnt = 0.0f;
            }
        }
        if(transform.position.y < 0)
        {
            anim.SetBool("LandingFlag", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            jumpCount = 0;
            anim.SetBool("LandingFlag", false);
        }

        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            //ダメージフラグがfalseだったらダメージ
            if(CollisionFlag==false)
            PlayerDamage();
        }
        if(other.gameObject.CompareTag("Trap"))
        {
            BoolTrap = true;
            Trapcnt = 0;
        }
    }

    void PlayerDamage()
    {
        // 体力減少
        life.LossLife();
        CollisionFlag = true;
        audioSource.PlayOneShot(sound1);
        shake.PlayShake(0, 0);
        //ダメージ判定が終わった後、3秒後に無敵を解除する
        Invoke("InvincibleEnd", 3.0f);
    }

    void InvincibleEnd()
    {
        CollisionFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    void StartAttack()
    {
        AttackArea.AttackAreaCreate();
        anim.SetBool("AttackFlag", false);
    }

    void JumpFlagReset()
    {
        anim.SetBool("JumpFlag", false);
    }

}
