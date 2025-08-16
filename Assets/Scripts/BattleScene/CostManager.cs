using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostManager : MonoBehaviour
{
    public static CostManager Instance { get; private set; }
    public int Cost;
    public GameObject CostUi;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        CostUi = gameObject;
        Cost = 3; // 임시로 3개. 턴마다 늘어나는것 구현 예정
        uiReset();
    }

    public bool IsCostAvailable(int cost)
    {
        if (Cost >= cost)
        {
            Cost -= cost;
            uiReset();
            return true;
        }
        else return false;
    }

    public void uiReset()
    {
        for (int i = 0; i < Cost; i++)
        {
            CostUi.GetComponentsInChildren<Image>()[i + 1].color = new Color32(100, 255, 255, 255);
        }
        for (int i = Cost; i < 10; i++)
        {
            CostUi.GetComponentsInChildren<Image>()[i + 1].color = new Color32(255, 255, 255, 255);
        }
    }
}
