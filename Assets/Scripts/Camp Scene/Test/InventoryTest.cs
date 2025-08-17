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
        }
        //2��Ű - ��������
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.Instance.RemoveItem(wood);
        }
        //3��Ű - ���߰�
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.Instance.AddItem(stone);
        }
        //4��Ű - ������
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InventoryManager.Instance.RemoveItem(stone);
        }
    }
}
