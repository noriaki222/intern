using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarFire : MonoBehaviour
{
    //���΂̃X�s�[�h
    [SerializeField] private float speed = 20.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lazerPos = transform.position; //Vector3�^��playerPos�Ɍ��݂̈ʒu�����i�[
        lazerPos.y += speed * Time.deltaTime; //x���W��speed�����Z
        transform.position = lazerPos; //���݂̈ʒu���ɔ��f������
    }
}
