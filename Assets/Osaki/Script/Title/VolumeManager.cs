using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Bloom bloom;

    [SerializeField] private float max = 1.0f;
    [SerializeField] private float speed = 0.1f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        volume.profile.TryGet(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        bloom.intensity.value = (Mathf.Sin(timer) + 1) / 2 * max;
        timer += speed;
    }
}
