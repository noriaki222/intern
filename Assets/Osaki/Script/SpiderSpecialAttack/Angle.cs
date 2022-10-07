using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angle : MonoBehaviour
{
    [SerializeField]
    GameObject _start;

    [SerializeField]
    GameObject _target;

    void Start()
    {
       
    }

    private void Update()
    {
        float angle = GetAngle(_start.transform.position, _target.transform.position);
        Debug.Log(angle);
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 dt = target - start;
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;

        return degree;
    }
}
