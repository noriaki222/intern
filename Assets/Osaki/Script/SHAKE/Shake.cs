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

    [SerializeField] private ShakeInfo shakeInfo;
    [SerializeField] private GameObject[] targets;
    private List<Vector3> initPos = new List<Vector3>();

    private bool isShake = false;   // �U�����Ă��邩
    private float totalTime;        // �o�ߎ���

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < targets.Length; ++i)
        {
            initPos.Add(targets[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShake) return;

        // �h���
        for(int i = 0; i < targets.Length; ++i)
        {
            targets[i].transform.position = GetUpdateShakePos(shakeInfo, totalTime, initPos[i]);
        }

        // ���Ԍo�ߌ㏉���ʒu��
        totalTime += Time.deltaTime;
        if(totalTime > shakeInfo.duration)
        {
            isShake = false;
            totalTime = 0.0f;

            for(int i = 0; i < targets.Length; ++i)
            {
                // �����ʒu��
                targets[i].transform.position = initPos[i];
            }
        }

    }

    public void PlayShake()
    {
        if(!isShake)
            shakeInfo.off = new Vector2(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f)); 
        isShake = true;
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
