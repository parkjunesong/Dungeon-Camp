using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    public Transform inventoryPanel;
    public GameObject slotPrefab;

    private List<InventorySlotUI> inventorySlotUIs = new List<InventorySlotUI>();

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
    }

    void Start()
    {
        GenerateSlotUIs();
        UpdateSlotUI();
    }

    void GenerateSlotUIs() //빈슬롯UI 생성
    {
        for (int i = 0; i < InventoryManager.Instance.maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlotUI inventorySlotUI = slot.GetComponent<InventorySlotUI>();
            inventorySlotUIs.Add(inventorySlotUI);
        }
    }

    public void UpdateSlotUI() //슬롯UI 업데이트
    {
        for (int i = 0; i < inventorySlotUIs.Count; i++)
        {
            if (i < InventoryManager.Instance.inventory.Count) //아이템이 있는 슬롯은 UI에 아이템 표시
            {
                var slot = InventoryManager.Instance.inventory;
                inventorySlotUIs[i].Setup(slot[i].item, slot[i].quantity);
            }
            else //없으면 빈슬롯
            {
                inventorySlotUIs[i].Clear();
            }
        }
    }
}
