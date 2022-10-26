using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class BloomChange : MonoBehaviour
{
    [SerializeField] private GameObject[] clouds;
    private Light2D moon;
    [SerializeField] private Volume volume;
    private Bloom bloom;
    // Start is called before the first frame update
    void Start()
    {
        moon = GetComponent<Light2D>();
        volume.profile.TryGet(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        float dis = Mathf.Abs(clouds[0].transform.position.x - transform.position.x);
        int cnt = 1;
        for(int i = 0; i < clouds.Length; ++i)
        {
            var work = Mathf.Abs(clouds[i].transform.position.x - transform.position.x);
            if (work < 4.0f)
                ++cnt;
            if(dis > work)
            {
                dis = work;
            }
        }

        var ratio = Mathf.InverseLerp(1f, 5f, cnt);
        float power = Mathf.Lerp(1.5f, 2f, dis / (4.0f + ratio));
        moon.intensity = power;

        bloom.intensity.value = Mathf.InverseLerp(1.5f, 2f, power);
    }
}
