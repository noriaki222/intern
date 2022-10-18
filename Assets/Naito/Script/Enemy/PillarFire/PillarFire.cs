using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarFire : MonoBehaviour
{
    //噴火のスピード
    [SerializeField] private float speed = 20.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lazerPos = transform.position; //Vector3型のplayerPosに現在の位置情報を格納
        lazerPos.y += speed * Time.deltaTime; //x座標にspeedを加算
        transform.position = lazerPos; //現在の位置情報に反映させる
    }
}
