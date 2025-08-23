using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Item wood;
    public Item stone;

    void Update()
    {
        //1번키 - 나무추가
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventoryManager.Instance.AddItem(wood);
            InventoryUI.Instance.UpdateSlotUI();
        }
        //2번키 - 나무제거
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.Instance.RemoveItem(wood);
            InventoryUI.Instance.UpdateSlotUI();
        }
        //3번키 - 돌추가
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.Instance.AddItem(stone);
            InventoryUI.Instance.UpdateSlotUI();
        }
        //4번키 - 돌제거
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InventoryManager.Instance.RemoveItem(stone);
            InventoryUI.Instance.UpdateSlotUI();
        }
    }
}
