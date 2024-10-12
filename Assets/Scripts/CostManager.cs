using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostClass
{
    private int type;   
    private int prev;
    public int Type
    {
        get { return type; }
        set { type = value; }
    }
    public int Prev
    {
        get { return prev; }
        set { prev = value; }
    }
    public CostClass(int i)
    {
        type = i;
        prev = 10;
    }
}

public class CostManager : MonoBehaviour
{
    public static CostManager cost;
    public List<CostClass> CostList = new List<CostClass>();
    Sprite[] CostSprite = new Sprite[4];
    public GameObject CostUi;

    void Awake()
    {
        cost = this;
    }
    void Start()
    {
        CostUi = GameObject.Find("코스트보드");
        CostSprite[0] = Resources.Load("Mana", typeof(Sprite)) as Sprite;
        CostSprite[1] = Resources.Load("Prana", typeof(Sprite)) as Sprite;
        CostSprite[2] = Resources.Load("Karna", typeof(Sprite)) as Sprite;        
        CostSprite[3] = Resources.Load("Used", typeof(Sprite)) as Sprite;

        // 테스트 세팅
        for (int i = 0; i < 6; i++) CostList.Add(new CostClass(0));    
        for (int i = 6; i < 12; i++) CostList.Add(new CostClass(1));
        ImageReset();
    }

    public void CostUse(int[] target)
    {
        int[] goal = new int[3] { 0, 0, 0 };
        while (goal[0] < target[0] || goal[1] < target[1] || goal[2] < target[2])
        {
            int no = 0;
            foreach (CostClass co in CostList)
            {
                if (co.Type == 0 && goal[0] < target[0])
                {
                    CostList[no].Prev = CostList[no].Type;
                    CostList[no].Type = 3;
                    goal[0]++;
                }
                else if (co.Type == 1 && goal[1] < target[1])
                {
                    CostList[no].Prev = CostList[no].Type;
                    CostList[no].Type = 3;
                    goal[1]++;
                }
                else if ((co.Type == 2 || co.Type == 3) && goal[2] < target[2])
                {
                    CostList[no].Prev = CostList[no].Type;
                    CostList[no].Type = 4;
                    goal[2]++;
                }
                no++;
            }
        }
        ImageReset();
    }

    public void CostReset()
    {
        int no = 0;
        foreach (CostClass co in CostList)
        {
            if (co.Type == 3)
            {
                if (co.Prev == 0) CostList[no].Type = 1;
                else if (co.Prev == 1) CostList[no].Type = 0;
                else if (co.Prev == 2) CostList[no].Type = 2;
                CostList[no].Prev = 3;
            }
            else if(co.Type == 4)
            {
                CostList[no].Type = 2;
                CostList[no].Prev = 4;
            }
            no++;
        }
        ImageReset();
    }

    public int[] CostCount()
    {
        int no = 0;
        int[] result = new int[5] { 0, 0, 0, 0, 0 };

        foreach (CostClass co in CostList)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (co.Type == i)
                {
                    result[i]++;
                    break;
                }
            }
            no++;
        }
        return result;
    }

    void ImageReset()
    {
        SetOrder();

        int no = 0;
        foreach (CostClass co in CostList)
        {
            for (int i = 0; i <= 4; i++)           
                if (co.Type == i)               
                    CostUi.GetComponentsInChildren<Image>()[no + 1].sprite = CostSprite[i];                               
            no++;
        }
    }
    void SetOrder() 
    {
        CostList.Sort(delegate (CostClass A, CostClass B)
        {
            if (A.Type < B.Type) return -1;
            else return 1;
        });      
    }
}
