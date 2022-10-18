using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMove : MonoBehaviour
{
    [SerializeField] private Transform moon;
    [SerializeField] private Transform target;
    private Vector3 oldPos;
    private bool onGround = false;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ˆÚ“®
        var amount = target.position - oldPos;
        float angle = Mathf.Atan2(moon.position.y, moon.position.x);
        var rot_amount = new Vector3(amount.x - (amount.y * Mathf.Cos(angle)), amount.y, amount.z);
        transform.position += new Vector3(rot_amount.x, -rot_amount.y, rot_amount.z);

        oldPos = target.position;

        // Œü‚«
        var scale = transform.localScale;
        scale.x = Mathf.Sign(target.localScale.x);
        transform.localScale = scale;
    }

    public void SetStandFlg(bool flg)
    {
        onGround = flg;
    }
}
