using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 indexをグローバルで添字として管理、
 ループ文にせずindexを移動させていく
 各関数は引数を用いない
*/
public class DragonThunder : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject scaffold;
    [SerializeField] private GameObject thunder;

    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private float speed = 10.0f;

    private SpriteRenderer scaffold_color;
    private float timer = 0.0f;

    [SerializeField] private int max_thunder = 3;
    private int index = 0;
    private int targetIdx = 1;

    private List<DragonThunderParam> param = new List<DragonThunderParam>();

    public enum STATE_THUNDER
    {
        CREATE,
        WAIT,
        ATTACK,
        DELETE,

        MAX_STATE,
        NONE
    }
    private STATE_THUNDER state = STATE_THUNDER.NONE;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < max_thunder; ++i)
        {
            param.Add(new DragonThunderParam());
            param[i].scaffold = Instantiate(scaffold);
            param[i].thunder = Instantiate(thunder);
            param[i].scaffold_color = param[i].scaffold.GetComponent<SpriteRenderer>();
        }
        targetIdx = param.Count;

        scaffold_color = scaffold.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        switch (param[index].state)
        {
            case STATE_THUNDER.CREATE:
                CreateScaffold(index);
                param[index].state = STATE_THUNDER.WAIT;
                break;
            case STATE_THUNDER.WAIT:
                if (Wait(index))
                    param[index].state = STATE_THUNDER.ATTACK;
                break;
            case STATE_THUNDER.ATTACK:
                if (Attack(index))
                    param[index].state = STATE_THUNDER.DELETE;
                break;
            case STATE_THUNDER.DELETE:
                Delete(index);
                param[index].state = STATE_THUNDER.NONE;
                if (index + 1 < targetIdx)
                {
                    ++index;
                    param[index].state = STATE_THUNDER.CREATE;
                }
                break;
        }
    }

    public void StartAttack(int max_num = 1)
    {
        int cnt = 0;
        for(int i = 0; i < param.Count; ++i)
        {
            if (param[i].state == STATE_THUNDER.NONE)
                ++cnt;
        }

        if(cnt >= param.Count)
        {
            if (max_num < param.Count)
                targetIdx = max_num;
            else
                targetIdx = param.Count;
            param[0].state = STATE_THUNDER.CREATE;
            index = 0;
        }
    }

    // ターゲットの足元に足場を作成
    private void CreateScaffold(int num)
    {
        if (targetIdx == 1)
        {
            param[num].scaffold.transform.position = target.transform.position;
            param[num].scaffold.transform.position = new Vector3(param[num].scaffold.transform.position.x, -4.0f, 1.0f);

            param[num].thunder.transform.position = new Vector3(param[num].scaffold.transform.position.x, 10.0f, 1.0f);
        }
        else
        {
            float rand_x = Random.Range(target.transform.position.x - 5.0f, target.transform.position.x + 5.0f);
            param[num].scaffold.transform.position = new Vector3(rand_x, target.transform.position.y, target.transform.position.z);
            param[num].scaffold.transform.position = new Vector3(param[num].scaffold.transform.position.x, -4.0f, 1.0f);

            param[num].thunder.transform.position = new Vector3(param[num].scaffold.transform.position.x, 10.0f, 1.0f);
        }

        param[num].scaffold_color.color = new Color(1.0f, 1.0f, 0.0f);
        param[num].scaffold.SetActive(true);
        param[num].thunder.SetActive(true);
    }

    private bool Wait(int num)
    {
        bool fin = false;
        param[num].timer += Time.deltaTime;

        if(param[num].timer >= waitTime * 0.8f)
        {
            float rate = (param[num].timer - (waitTime * 0.8f)) / (waitTime - (waitTime * 0.8f));

            param[num].scaffold_color.color = new Color(1.0f, 1.0f, Mathf.Lerp(param[num].scaffold_color.color.b, 1.0f, rate));
        }

        if(param[num].timer >= waitTime)
        {
            fin = true;
            param[num].timer = 0.0f;
        }

        return fin;
    }

    private bool Attack(int num)
    {
        bool fin = false;
        param[num].thunder.transform.position = Vector3.MoveTowards(param[num].thunder.transform.position, param[num].scaffold.transform.position, speed);
        if ((param[num].thunder.transform.position - param[num].scaffold.transform.position).magnitude < 1.0f)
            fin = true;

        return fin;
    }

    private void Delete(int num)
    {
        param[num].scaffold.SetActive(false);
        param[num].thunder.SetActive(false);
    }
}
