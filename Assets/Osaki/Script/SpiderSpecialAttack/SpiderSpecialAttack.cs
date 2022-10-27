using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 始点、終点座標の範囲
上：-12.0f < x < 12.0f, y = 6.0f; 
下：-12.0f < x < 12.0f, y = -6.0f;
左：x = -12.0f, -6.0f < y < 6.0f;
右：x = 12.0f, -6.0f < y < 6.0f;
 */

public class SpiderSpecialAttack : MonoBehaviour
{
    [SerializeField] private GameObject spiderSilk;
    [SerializeField] private GameObject small_spider;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float small_speed = 1.0f;

    [SerializeField] private int LineNum = 3;

    [SerializeField] private AudioClip sound;


    private List<SpiderSpecialParam> param = new List<SpiderSpecialParam>();
    private List<Rigidbody2D> rbs = new List<Rigidbody2D>();
    private AudioSource audioSource;
    private bool[] isPlaySe;

    // 攻撃ステート
    private enum STATE_SILK
    {
        CREATE,
        MOVE_SILK,
        ATTACK,
        DELETE,

        MAX_STATE,
        NONE
    };
    private STATE_SILK state = STATE_SILK.NONE;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < LineNum; ++i)
        {
            param.Add(new SpiderSpecialParam());
        }

        for(int i = 0; i < param.Count; ++i)
        {
            param[i].spiderSilk = Instantiate(spiderSilk);
            param[i].small_spider = Instantiate(small_spider);
            rbs.Add(param[i].small_spider.GetComponent<Rigidbody2D>());
        }

        audioSource = GetComponent<AudioSource>();
        isPlaySe = new bool[LineNum];
        for(int i = 0; i < LineNum; ++i)
        {
            isPlaySe[i] = false;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case STATE_SILK.CREATE:
                CreateLine();
                state = STATE_SILK.MOVE_SILK;
                break;
            case STATE_SILK.MOVE_SILK:
                if(MoveSpiderSilk())
                    state = STATE_SILK.ATTACK;
                break;
            case STATE_SILK.ATTACK:
                if(AttackSmallSpider())
                    state = STATE_SILK.DELETE;
                break;
            case STATE_SILK.DELETE:
                if(DeleteSilk())
                    state = STATE_SILK.NONE;
                break;
            default:
                break;
        }
    }

    public void StartAttack()
    {
        if(state == STATE_SILK.NONE)
        {
            state = STATE_SILK.CREATE;
        }
    }

    // 糸の始点と終点を決定し、ベクトルを作成し、
    // オブジェクトを開始地点へ移動
    private void CreateLine()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        for (int i = 0; i < param.Count; ++i)
        {
            // 始点と終点を決定
            int work = UnityEngine.Random.Range(0, 4);

            int work2 = UnityEngine.Random.Range(0, 4);
            while (work2 == work)
            {
                work2 = UnityEngine.Random.Range(0, 4);
            }

            param[i].startPos = GetRandPos(work, true);
            param[i].endPos = GetRandPos(work2);

            while((param[i].endPos - param[i].startPos).magnitude < 12.0f)
                param[i].endPos = GetRandPos(work2);

            // 角度を求める
            float angle = GetAngle(param[i].startPos, param[i].endPos);

            // オブジェクトを回転
            var rot = param[i].spiderSilk.transform.eulerAngles;
            rot.z = angle;
            param[i].spiderSilk.transform.eulerAngles = rot;

            // オブジェクトを移動
            float objLength = param[i].spiderSilk.transform.localScale.x;
            // float objLength = param[i].spiderSilk.GetComponent<SpriteRenderer>().bounds.size.x * 2.0f;
            float off_x, off_y;
            float off_ex, off_ey;

            if (((param[i].endPos.y - param[i].startPos.y) / (param[i].endPos.x - param[i].startPos.x)) >= 0.0f)
            {
                //Debug.Log("傾き：正");
                if ((param[i].endPos.x - param[i].startPos.x) >= 0.0f)
                {
                    //Debug.Log("左下から右上");
                    off_x = param[i].startPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                    off_y = param[i].startPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                    off_ex = param[i].endPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                    off_ey = param[i].endPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                }
                else
                {
                    //Debug.Log("右上から左下");
                    off_x = param[i].startPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                    off_y = param[i].startPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                    off_ey = param[i].endPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                    off_ex = param[i].endPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                }
            }
            else
            {
                //Debug.Log("傾き：負");
                if ((param[i].endPos.x - param[i].startPos.x) >= 0.0f)
                {
                    //Debug.Log("左上から右下");
                    off_x = param[i].startPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                    off_y = param[i].startPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                    off_ex = param[i].endPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                    off_ey = param[i].endPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                }
                else
                {
                    //Debug.Log("右下から左上");
                    off_x = param[i].startPos.x + ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                    off_y = param[i].startPos.y - ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                    off_ex = param[i].endPos.x - ((objLength / 2) * Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)));
                    off_ey = param[i].endPos.y + ((objLength / 2) * Mathf.Abs(Mathf.Sin(angle * Mathf.Deg2Rad)));
                }
            }

            param[i].stopPos = new Vector3((param[i].endPos.x + param[i].startPos.x) / 2, (param[i].endPos.y + param[i].startPos.y) / 2, 0.0f);

            param[i].spiderSilk.transform.position = new Vector3(off_x, off_y, 0.0f);
            param[i].off_start = new Vector3(off_x, off_y, 0.0f);
            param[i].off_end = new Vector3(off_ex, off_ey, 0.0f);

            param[i].small_spider.transform.position = param[i].off_start;
            var dir = param[i].endPos - param[i].small_spider.transform.position;
            param[i].small_spider.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);

            param[i].small_spider.tag = "EnemyBullet";

            param[i].spiderSilk.SetActive(true);
            param[i].small_spider.SetActive(true);
        }
    }

    // 2座標から角度を求める
    private float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }

    // ベクトルにそって糸を始点から終点へ向かって移動させる
    private bool MoveSpiderSilk()
    {
        bool fin = false;
        int go = 1;
        for(int i = 0; i < go; ++i)
        {
            param[i].spiderSilk.transform.position = Vector3.MoveTowards(param[i].spiderSilk.transform.position, param[i].stopPos, speed);

            if (go < param.Count && ((param[go - 1].spiderSilk.transform.position - param[go - 1].stopPos).magnitude < 1.0f))
            {
                ++go;
            }
        }

        if(!isPlaySe[go -1])
        {
            isPlaySe[go - 1] = true;
            audioSource.PlayOneShot(sound);
        }

        if (((param[param.Count - 1].spiderSilk.transform.position - param[param.Count - 1].stopPos).magnitude < 1.0f))
        {
            fin = true;
            for (int i = 0; i < isPlaySe.Length; ++i)
                isPlaySe[i] = false;
        }

        return fin;
    }

    // ベクトルにそって子蜘蛛を移動させる
    private bool AttackSmallSpider()
    {
        bool fin = false;
        int go = 1;
        for (int i = 0; i < go; ++i)
        {
            if (rbs[i].velocity.magnitude == 0.0f)
                param[i].small_spider.transform.position = Vector3.MoveTowards(param[i].small_spider.transform.position, param[i].off_end, speed * small_speed);

            if ((param[go - 1].small_spider.transform.position - param[go - 1].off_end).magnitude < 15.0f)
                param[go - 1].small_spider.SetActive(false);

            if (go < param.Count && !param[go - 1].small_spider.activeSelf)
            {
                ++go;
            }
        }
        if (((param[param.Count - 1].small_spider.transform.position - param[param.Count - 1].off_end).magnitude < 1.0f))
            fin = true;

        return fin;
    }

    // 表示させた糸を非表示に
    private bool DeleteSilk()
    {
        bool fin = false;
        int go = 1;
        for (int i = 0; i < go; ++i)
        {
            param[i].spiderSilk.transform.position = Vector3.MoveTowards(param[i].spiderSilk.transform.position, param[i].off_end, speed);

            if (go < param.Count && ((param[go - 1].spiderSilk.transform.position - param[go - 1].off_end).magnitude < 1.0f))
            {
                rbs[i].velocity = Vector2.zero;
                param[i].spiderSilk.SetActive(false);
                param[i].small_spider.SetActive(false);
                ++go;
            }
        }

        if (!isPlaySe[go - 1])
        {
            isPlaySe[go - 1] = true;
            audioSource.PlayOneShot(sound);
        }

        if (((param[param.Count - 1].spiderSilk.transform.position - param[param.Count - 1].off_end).magnitude < 1.0f))
        {
            fin = true;
            for (int i = 0; i < isPlaySe.Length; ++i)
                isPlaySe[i] = false;
        }

        return fin;
    }

    // 画面端の座標を返す
    // 引数：0～3で画面の上下左右を指定
    // 戻り値：座標
    private Vector3 GetRandPos(int num, bool flg = false)
    {
        Vector3 pos = Vector3.zero;
        switch(num)
        {
            case 0: // 上
                if (!flg)
                    pos.x = UnityEngine.Random.Range(-12.0f, 12.0f);
                else
                    pos.x = 0.0f;
                pos.y = 6.0f;
                break;
            case 1: // 下
                if (!flg)
                    pos.x = UnityEngine.Random.Range(-12.0f, 12.0f);
                else
                    pos.x = 0.0f;
                pos.y = -6.0f;
                break;
            case 2: // 左
                pos.x = -12.0f;
                if (!flg)
                    pos.y = UnityEngine.Random.Range(-6.0f, 6.0f);
                else
                    pos.y = 0.0f;
                break;
            case 3: // 右
                pos.x = 12.0f;
                if (!flg)
                    pos.y = UnityEngine.Random.Range(-6.0f, 6.0f);
                else
                    pos.y = 0.0f;
                break;
        }

        return pos;
    }
}
