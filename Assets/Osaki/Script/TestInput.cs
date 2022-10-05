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
            hit.AddHit();
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            life.LossLife();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            life.AddLife();
        }
    }
}
