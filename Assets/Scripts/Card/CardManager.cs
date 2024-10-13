using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager card;
    public GameObject CardUi;
    public List<GameObject> Hand = new List<GameObject>();
    public List<GameObject> Deck = new List<GameObject>(); 
    // 개발용으로 덱 남겨둠. 베이스캠프에서 편성 만들면 바꿀 예정

    void Awake()
    {
        card = this;
        CardUi = gameObject;
        DrawCard(); DrawCard(); DrawCard(); DrawCard(); DrawCard();
    }

    public void UseCardNo(int i)
    {
        if (Hand.Count > i)
        {
            Card_Base card = Hand[i].GetComponent<Card_Base>();
            if (CostManager.cost.GetComponent<CostManager>().CostUse(card.Card_Cost))
            {
                card.execute();
                Hand.Remove(Hand[i]);
            }
            else
                Debug.Log("Can't Use Card");
        }      
    }

    public void DrawCard()
    {
        Hand.Add(Deck[0]);
        Deck.RemoveAt(0);
    }

    public void uiReset()
    {
       
    }
}
