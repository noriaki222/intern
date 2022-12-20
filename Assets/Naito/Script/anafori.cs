using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anafori : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("PlayDestroy", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayDestroy()
    {
        Destroy(this.gameObject);
    }
}
