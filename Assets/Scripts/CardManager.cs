using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager card;
    public GameObject CardUi;

    void Awake()
    {
        card = this;
        CardUi = GameObject.Find("ÇÚµå");
    }
    public void UseCard(Card_Base card)
    {
        int[] CardCost = card.Card_Cost;
        int[] CostNow = CostManager.cost.GetComponent<CostManager>().CostCount();

        if (CostNow[0] >= CardCost[0] && CostNow[1] >= CardCost[1] && (CostNow[2] >= CardCost[2] || CostNow[3] >= CardCost[2]))
        {
            card.execute();
            CostManager.cost.GetComponent< CostManager> ().CostUse(CardCost);
        }
        else
            Debug.Log("Can't Use Card");
    }
    
    public void uiReset()
    {
    }
}
