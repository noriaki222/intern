using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarFireEffactEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndEffact", 6.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndEffact()
    {
        Destroy(this.gameObject);
    }
}
