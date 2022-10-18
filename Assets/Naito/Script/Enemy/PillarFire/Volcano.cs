using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    //�v���C���[�����p
    [SerializeField] private GameObject Player;
    //���΂��~�߂邩�ǂ����̃t���O
    private bool StartFlag = false;
    //�Β��I�u�W�F�N�g
    [SerializeField] private GameObject PillarFire;
    //���ΊJ�n���邽�߂̃J�E���g�p
    private float VolcanoCnt;
    //���΃^�C�~���O
    [SerializeField] private float VolcanoTiming = 1.0f;
    //���΂��Ă������̃t���O
    private bool PillarFireFlag = false;
    //�Β��o���Ԋu
    [SerializeField] private float PillarFireTiming = 0.1f;
    //�o���J�E���g�p
    private float PillarFirecnt;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("VolcanoStart", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PillarFireFlag == false)
        {
            if (StartFlag == false)
            {
                if (this.transform.position.x != Player.transform.position.x)
                {
                    Vector3 VolcanoTrans = transform.position;
                    VolcanoTrans.x = Player.transform.position.x;
                    transform.position = VolcanoTrans;
                }
            }
            else
            {
                VolcanoCnt += Time.deltaTime;
                if(VolcanoCnt>VolcanoTiming)
                {
                    PillarFireFlag = true;
                }
            }
        }
        else
        { 
            PillarFirecnt += Time.deltaTime;
            if (PillarFirecnt > PillarFireTiming)
            {
                Instantiate(PillarFire, transform.position, Quaternion.identity);
                PillarFirecnt = 0.0f;
            }
        }
    }

    void VolcanoStart()
    {
        StartFlag = true;
    }
}
