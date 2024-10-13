using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    public static CostManager cost;
    public int Mana;
    public GameObject CostUi;

    void Awake()
    {
        cost = this;
    }
    void Start()
    {
        CostUi = gameObject;
        Mana = 3; // 임시로 3개. 턴마다 늘어나는것 구현 예정
        uiReset();
    }

    public bool CostUse(int cost)
    {
        if (Mana >= cost)
        {
            Mana -= cost;
            uiReset();
            return true;
        }
        else return false;
    }

    public void uiReset()
    {
        for(int i = 0; i < Mana; i++)
        {
            CostUi.GetComponentsInChildren<Image>()[i + 1].color = new Color32(100, 255, 255, 255);
        }
        for(int i = Mana; i < 10; i++)
        {
            CostUi.GetComponentsInChildren<Image>()[i + 1].color = new Color32(255, 255, 255, 255);
        }
    }
}
