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

    void GenerateSlotUIs() //�󽽷�UI ����
    {
        for (int i = 0; i < InventoryManager.Instance.maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, inventoryPanel);
            InventorySlotUI inventorySlotUI = slot.GetComponent<InventorySlotUI>();
            inventorySlotUIs.Add(inventorySlotUI);
        }
    }

    public void UpdateSlotUI() //����UI ������Ʈ
    {
        for (int i = 0; i < inventorySlotUIs.Count; i++)
        {
            if (i < InventoryManager.Instance.inventory.Count) //�������� �ִ� ������ UI�� ������ ǥ��
            {
                var slot = InventoryManager.Instance.inventory;
                inventorySlotUIs[i].Setup(slot[i].item, slot[i].quantity);
            }
            else //������ �󽽷�
            {
                inventorySlotUIs[i].Clear();
            }
        }
    }
}
