using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonThunder : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject scaffold;
    [SerializeField] private GameObject thunder;

    [SerializeField] private float waitTime = 1.0f;
    [SerializeField] private float speed = 10.0f;

    private SpriteRenderer scaffold_color;
    private float timer = 0.0f;

    private enum STATE_THUNDER
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
        scaffold_color = scaffold.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case STATE_THUNDER.CREATE:
                CreateScaffold();
                state = STATE_THUNDER.WAIT;
                break;
            case STATE_THUNDER.WAIT:
                if (Wait())
                    state = STATE_THUNDER.ATTACK;
                break;
            case STATE_THUNDER.ATTACK:
                if (Attack())
                    state = STATE_THUNDER.DELETE;
                break;
            case STATE_THUNDER.DELETE:

                state = STATE_THUNDER.NONE;
                break;

            case STATE_THUNDER.NONE:
                scaffold.SetActive(false);
                thunder.SetActive(false);
                scaffold_color.color = new Color(1.0f, 1.0f, 0.0f);
                break;
        }

        Debug.Log(state);

    }

    public void StartAttack()
    {
        if (state == STATE_THUNDER.NONE)
            state = STATE_THUNDER.CREATE;
    }

    // ターゲットの足元に足場を作成
    private void CreateScaffold()
    {
        scaffold.transform.position = target.transform.position;
        scaffold.transform.position = new Vector3(scaffold.transform.position.x, -4.0f, 1.0f);

        thunder.transform.position = new Vector3(scaffold.transform.position.x, 10.0f, 1.0f);

        scaffold.SetActive(true);
        thunder.SetActive(true);
    }

    private bool Wait()
    {
        bool fin = false;
        timer += Time.deltaTime;

        if(timer >= waitTime * 0.8f)
        {
            float rate = (timer - (waitTime * 0.8f)) / (waitTime - (waitTime * 0.8f));
            
            scaffold_color.color = new Color(1.0f, 1.0f, Mathf.Lerp(scaffold_color.color.b, 1.0f, rate));
        }

        if(timer >= waitTime)
        {
            fin = true;
            timer = 0.0f;
        }

        return fin;
    }

    private bool Attack()
    {
        bool fin = false;
        thunder.transform.position = Vector3.MoveTowards(thunder.transform.position, scaffold.transform.position, speed);
        if ((thunder.transform.position - scaffold.transform.position).magnitude < 1.0f)
            fin = true;

        return fin;
    }

    private void Delete()
    {
        scaffold.SetActive(false);
        thunder.SetActive(false);
    }
}
