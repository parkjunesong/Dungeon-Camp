using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [System.Serializable]
    public class InventorySlot
    {
        public Item item; //인벤 슬롯 안의 아이템
        public int quantity; //아이템 수량
    }

    //인벤토리
    public List<InventorySlot> inventory = new List<InventorySlot>();
    public int maxSlots = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    //슬롯 내에 해당 아이템이 이미 존재하는지 확인
    private Predicate<InventorySlot> MatchItem(Item targetItem)
    {
        return delegate (InventorySlot slot) { return slot.item == targetItem; };
    }

    public bool AddItem(Item newItem, int count = 1)
    {
        var slot = inventory.Find(MatchItem(newItem)); //아이템이 존재하는 슬롯

        if (slot != null) //이미 아이템이 존재하면
        {
            slot.quantity += count;
            return true;
        }
        else if (inventory.Count < maxSlots) //새 아이템이면
        {
            inventory.Add(new InventorySlot { item = newItem, quantity = count });
            return true;
        }

        return false; //인벤토리 가득 참
    }

    public bool RemoveItem(Item item, int count = 1)
    {
        var slot = inventory.Find(MatchItem(item)); //아이템이 존재하는 슬롯

        if (slot != null && slot.quantity >= count) //아이템 수가 충분하면
        {
            slot.quantity -= count;

            if (slot.quantity == 0) //아이템이 없으면 슬롯 비우기
            {
                inventory.Remove(slot);
            }

            return true;
        }

        return false;
    }
}
