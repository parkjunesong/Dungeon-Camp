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
        public Item item; //�κ� ���� ���� ������
        public int quantity; //������ ����
    }

    //�κ��丮
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

    //���� ���� �ش� �������� �̹� �����ϴ��� Ȯ��
    private Predicate<InventorySlot> MatchItem(Item targetItem)
    {
        return delegate (InventorySlot slot) { return slot.item == targetItem; };
    }

    public bool AddItem(Item newItem, int count = 1)
    {
        var slot = inventory.Find(MatchItem(newItem)); //�������� �����ϴ� ����

        if (slot != null) //�̹� �������� �����ϸ�
        {
            slot.quantity += count;
            return true;
        }
        else if (inventory.Count < maxSlots) //�� �������̸�
        {
            inventory.Add(new InventorySlot { item = newItem, quantity = count });
            return true;
        }

        return false; //�κ��丮 ���� ��
    }

    public bool RemoveItem(Item item, int count = 1)
    {
        var slot = inventory.Find(MatchItem(item)); //�������� �����ϴ� ����

        if (slot != null && slot.quantity >= count) //������ ���� ����ϸ�
        {
            slot.quantity -= count;

            if (slot.quantity == 0) //�������� ������ ���� ����
            {
                inventory.Remove(slot);
            }

            return true;
        }

        return false;
    }
}
