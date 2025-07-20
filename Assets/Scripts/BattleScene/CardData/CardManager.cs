using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    public GameObject CardUi;
    public List<Card_Base> Deck;
    public List<Card_Base> Hand = new();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        CardUi = gameObject;
        Deck = new List<Card_Base>(SystemManager.Instance.DeckData.Cards);

        ShuffleCard(Deck);
        for (int i = 0; i < 5; i++) DrawCard();
    }
    public void UseCardFromHand(int i)
    {
        if (Hand.Count > i)
        {
            Card_Base card = Hand[i].GetComponent<Card_Base>();           
            if (CostManager.Instance.IsCostAvailable(card.Card_Cost))
            {
                card.Execute();
                Hand.Remove(Hand[i]);
                BattleManager.Instance.TurnEnd();
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
    public void ShuffleCard(List<Card_Base> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            (cards[i], cards[randomIndex]) = (cards[randomIndex], cards[i]);
        }
    }

    public void uiReset()
    {
       
    }
}
