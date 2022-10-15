using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �Ώۂ̃I�u�W�F�N�g��h�炷
 * Parameter�F�h�炷�Ώۂ̃I�u�W�F�N�g�z��
 *          �@�U��
 *            �h��鎞��
 *            
 *            ���̍��W
 */

public class Shake : MonoBehaviour
{
    [System.Serializable]
    public struct ShakeInfo
    {
       public float duration; // ����
       public float strenght; // ����
       public float vibrate;  // �傫��
       [HideInInspector]
       public Vector2 off;    // �I�t�Z�b�g
    }

    [SerializeField] private ShakeInfo[] shakeInfo;
    [SerializeField] private GameObject[] targets;
    private List<Vector3> initPos = new List<Vector3>();

    private bool[] isShake;   // �U�����Ă��邩
    private float[] totalTime;        // �o�ߎ���
    private int[] shakeType;    // shakeInfo�̃^�C�v

    // Start is called before the first frame update
    void Start()
    {
        totalTime = new float[targets.Length];
        isShake = new bool[targets.Length];
        shakeType = new int[targets.Length];
        for(int i = 0; i < targets.Length; ++i)
        {
            initPos.Add(targets[i].transform.position);
            totalTime[i] = 0.0f;
            shakeType[i] = 0;
            isShake[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < targets.Length; ++i)
        {
            if (!isShake[i])
                continue;

            // �h���
            targets[i].transform.position = GetUpdateShakePos(shakeInfo[shakeType[i]], totalTime[i], initPos[i]);

            // ���Ԍo�ߌ㏉���ʒu��
            totalTime[i] += Time.deltaTime;
            if(totalTime[i] > shakeInfo[shakeType[i]].duration)
            {
                isShake[i] = false;
                totalTime[i] = 0.0f;

                // �����ʒu��
                targets[i].transform.position = initPos[i];
            }
        }
    }

    public void PlayShake(int element = -1, int type = 0)
    {
        if(element < 0 || element >= isShake.Length)
        {
            for(int i = 0; i < isShake.Length; ++i)
            {
                isShake[i] = true;
                if(type < shakeInfo.Length && type >= 0)
                    shakeType[i] = type;
            }
        }
        else
        {
            isShake[element] = true;
            if (type < shakeInfo.Length && type >= 0)
                shakeType[element] = type;
        }

        for(int i = 0; i < shakeInfo.Length; ++i)
        {
            shakeInfo[i].off = new Vector2(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f)); 
        }
    }

    private Vector3 GetUpdateShakePos(ShakeInfo info, float totalTime, Vector3 initPos)
    {
        // �p�[�����m�C�Y�l�擾
        float randomX = GetPerlinNoiseValue(info.off.x, info.strenght, totalTime);
        float randomY = GetPerlinNoiseValue(info.off.y, info.strenght, totalTime);
        randomX *= info.strenght;
        randomY *= info.strenght;

        float rate = 1.0f - totalTime / info.duration;
        randomX = Mathf.Clamp(randomX, -info.vibrate * rate, info.vibrate * rate);
        randomY = Mathf.Clamp(randomY, -info.vibrate * rate, info.vibrate * rate);

        var pos = initPos;
        pos.x += randomX;
        pos.y += randomY;

        return pos;
    }

    private float GetPerlinNoiseValue(float off, float speed, float time)
    {
        // Mathf.PerlinNoise(x, y)  2D���ʏ�ɐ������ꂽfloat�l�̋[�������_���p�^�[��
        // �߂�l�F0.0f�`1.0f�͈͓̔��̒l
        // x, y:���W�@�������W���w�肷��Γ����l���A���Ă���B���ʂ͊�{�I�ɖ����B
        var perlinNoise = Mathf.PerlinNoise(off + speed * time, 0.0f);
        // 0.0f �` 1.0f �� -1.0f �` 1.0f�ɕϊ�
        return (perlinNoise - 0.5f) * 2.0f;
    }
}
