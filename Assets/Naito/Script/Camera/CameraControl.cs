using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera cam; //Main CameraのCamera
    private Vector3 CameraPos;
    [SerializeField] private bool ZoomFlag = false;

    void Start()
    {
        cam = this.gameObject.GetComponent<Camera>(); //Main CameraのCameraを取得する。
    }

    void Update()
    {
        if (ZoomFlag) //Iキーが押されていれば
        {
            cam.orthographicSize = cam.orthographicSize - 0.015f; //ズームイン。
        }
    }
}
