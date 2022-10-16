using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    [SerializeField] private HitUI hit;
    [SerializeField] private LifeUI life;
    [SerializeField] private EnemyHpBar enemyHp;
    [SerializeField] private float decreaseHp = 20.0f;
    [SerializeField] private SpiderSpecialAttack attack;
    [SerializeField] private DragonThunder thunder;
    [SerializeField] private int thunderNum = 1;
    [SerializeField] private Shake shake;
    [SerializeField] private int shakeNum = 0;
    [SerializeField] private int shakeType = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            // ƒRƒ“ƒ{‰ÁZ
            hit.AddHit();
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            // ‘Ì—ÍŒ¸­
            life.LossLife();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            // ‘Ì—Í‘‰Á
            life.AddLife();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            // “G‘Ì—ÍŒ¸­
            enemyHp.DecHp(decreaseHp);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            // ’wå‚Ì•KE‹Z
            attack.StartAttack();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            // ——‹
            thunder.StartAttack(thunderNum);
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            // —h‚ê‚é
            shake.PlayShake(shakeNum, shakeType);
        }
    }
}
