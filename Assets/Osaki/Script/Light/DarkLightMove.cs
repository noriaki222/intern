using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkLightMove : MonoBehaviour
{
    [SerializeField] private float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0.0f, 0.0f);
        if(transform.position.x >= 15.0f)
        {
            transform.position = new Vector3(-15.0f, transform.position.y, transform.position.z);
        }
    }
}
