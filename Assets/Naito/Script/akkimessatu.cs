using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class akkimessatu : MonoBehaviour
{
    //[SerializeField] private Shake shake;
    private float Shakefloat = 0.0f;
    private Vector3 RendaPos;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject.transform.position;
        Shakefloat = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        RendaPos = this.gameObject.transform.position;

        //òAë≈ÇÃêUìÆ
        Shakefloat = Shakefloat * 0.99f - Shakefloat * 0.01f;

        RendaPos.x = target.x + Random.Range(-Shakefloat, Shakefloat);
        RendaPos.y = target.y + Random.Range(-Shakefloat, Shakefloat);

        this.gameObject.transform.position = RendaPos;

        //if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
        //{
        //    Shakefloat = 0.3f;
        //    // óhÇÍÇÈ
        //    shake.PlayShake(4, 1);
        //}
    }
}
