using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private PlayLife[] lifes;
    private int lifeNum;
    // Start is called before the first frame update
    void Start()
    {
        lifeNum = lifes.Length;
    }

    private void Update()
    {
        Debug.Log(lifeNum);

        if(lifeNum > 0 && lifes[lifeNum - 1].GetLossFin())
        {
            lifes[lifeNum - 1].gameObject.SetActive(false);
            --lifeNum;
        }
    }

    public int GetLifeNum()
    {
        return lifeNum;
    }

    public void LossLife()
    {
        if(lifeNum > 0)
        {
            lifes[lifeNum - 1].PlayLossAnim();
        }
    }
}
