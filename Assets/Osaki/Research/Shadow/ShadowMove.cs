using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMove : MonoBehaviour
{
    [SerializeField] private Transform moon;
    [SerializeField] private Transform target;
    private Vector3 oldPos;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        oldPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5"))
            anim.Play("PlayerShadow", 0, 0);

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
}
