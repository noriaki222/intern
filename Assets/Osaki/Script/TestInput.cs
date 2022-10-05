using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    [SerializeField] private HitUI hit;
    [SerializeField] private LifeUI life;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            // コンボ加算
            hit.AddHit();
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            // 体力減少
            life.LossLife();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            // 体力増加
            life.AddLife();
        }
    }
}
