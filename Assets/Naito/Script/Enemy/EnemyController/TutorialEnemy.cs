using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    //通常弾のプレハブ入れる用
    [SerializeField] private GameObject Bullet;

    //通常弾の出現場所を格納する用
    [SerializeField] private Transform BulletPoint;

    //各弾の発射に使うカウント
    private float Bulletcnt;

    //各弾が一回出るのにかかる時間
    [SerializeField] private float BulletTiming = 3.0f;

    //ダメージを食らっているかどうかのフラグ
    private bool DamageFlag = false;
    //点滅用に点滅させたいオブジェクトを入れる
    [SerializeField] private SpriteRenderer sp;
    //敵のHPを管理しているUIを入れる用
    [SerializeField] private EnemyHpBar enemyHp;
    //敵の現在のHPを格納する用
    [SerializeField] private float NowHP;

    //SE出す用
    AudioSource audioSource;
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    //色々揺らす用
    [SerializeField] private Shake shake;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //今のHPを格納
        NowHP = enemyHp.GetNowHp();
        //ダメージ判定を受けているとき、点滅する
        if (DamageFlag)
        {
            //Mathf.Absは絶対値を返す、Mathf.Sinは＋なら１，－なら0を返す
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            //上の式で0と1が交互に来るので、それを透明度に入れて反転させている
            sp.color = new Color(1.0f, 0.0f, 0.0f, level);
        }
        Bulletcnt += Time.deltaTime;
        //通常弾
        if (Bulletcnt > BulletTiming)
        {
            Instantiate(Bullet, BulletPoint.position, Quaternion.identity);
            audioSource.PlayOneShot(sound1);
            Bulletcnt = 0;
        }
    }

    public void EnemyDamage(float damage)
    {
        if (DamageFlag == false)
        {
            DamageFlag = true;
            // 敵体力減少
            enemyHp.DecHp(damage);
            audioSource.PlayOneShot(sound2);
            //ダメージ判定が終わった後、3秒後に無敵を解除する
            Invoke("InvincibleEnd", 3.0f);
            shake.PlayShake(2, 0);
        }
    }
    void InvincibleEnd()
    {
        DamageFlag = false;
        sp.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

}
