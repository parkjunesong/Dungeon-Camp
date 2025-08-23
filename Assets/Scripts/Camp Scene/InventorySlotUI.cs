using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image itemImage;
    public Text itemQuantityText;

    private Item item;
    private int quantity;

    public void Setup(Item newItem, int count)
    {
        item = newItem;
        quantity = count;

        if (item != null)
        {
            //������ ������ UI�� ����
            itemImage.sprite = item.icon;
            itemImage.enabled = true;
            itemQuantityText.text = quantity.ToString();
        }
        else //�ʿ��Ѱ�?
        {
            Clear();
        }
    }

    public void Clear() //����UI �ʱ�ȭ
    {
        item = null;
        quantity = 0;
        itemImage.sprite = null;
        itemImage.enabled = false;
        itemQuantityText.text = "";
    }

    public void OnClick() //���� Ŭ���� ������ 1�� ����
    {
        if (item != null)
        {
            bool removed = InventoryManager.Instance.RemoveItem(item, 1);

            if (removed)
            {
                Debug.Log(item.itemName + "1 removed");

                InventoryUI.Instance.UpdateSlotUI();
            }
            else
            {
                Debug.Log("Failed" + quantity + "left");
            }
        }
    }
}
