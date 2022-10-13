using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private PlayLife[] lifes;
    private int lifeNum;
    private int addNum;
    // Start is called before the first frame update
    void Start()
    {
        lifeNum = lifes.Length;
        addNum = 0;
    }

    private void Update()
    {
        // Debug.Log(lifeNum);

        if(lifeNum > 0 && lifes[lifeNum - 1].GetLossFin())
        {
            lifes[lifeNum - 1].gameObject.SetActive(false);
            --lifeNum;
        }

        if(addNum > 0)
        {
            lifes[lifeNum].PlayAddAnim();
            lifes[lifeNum].gameObject.SetActive(true);
            --addNum;
            ++lifeNum;
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

    public void AddLife(int num = 1)
    {
        if(lifes.Length >= lifeNum + num)
        {
            addNum = num;
        }
        else
        {
            addNum = lifes.Length - lifeNum;
        }
    }
}
