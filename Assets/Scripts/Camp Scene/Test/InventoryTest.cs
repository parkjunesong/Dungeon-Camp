using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Item wood;
    public Item stone;

    void Update()
    {
        //1��Ű - �����߰�
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventoryManager.Instance.AddItem(wood);
            InventoryUI.Instance.UpdateSlotUI();
        }
        //2��Ű - ��������
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.Instance.RemoveItem(wood);
            InventoryUI.Instance.UpdateSlotUI();
        }
        //3��Ű - ���߰�
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.Instance.AddItem(stone);
            InventoryUI.Instance.UpdateSlotUI();
        }
        //4��Ű - ������
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InventoryManager.Instance.RemoveItem(stone);
            InventoryUI.Instance.UpdateSlotUI();
        }
    }
}
