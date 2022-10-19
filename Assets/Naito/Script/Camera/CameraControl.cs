using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera cam; //Main Camera��Camera
    private Vector3 CameraPos;
    [SerializeField] private bool ZoomFlag = false;

    void Start()
    {
        cam = this.gameObject.GetComponent<Camera>(); //Main Camera��Camera���擾����B
    }

    void Update()
    {
        if (ZoomFlag) //I�L�[��������Ă����
        {
            cam.orthographicSize = cam.orthographicSize - 0.015f; //�Y�[���C���B
        }
    }
}
