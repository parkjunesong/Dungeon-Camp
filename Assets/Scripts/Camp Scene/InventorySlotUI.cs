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
            //아이템 정보를 UI에 연결
            itemImage.sprite = item.icon;
            itemImage.enabled = true;
            itemQuantityText.text = quantity.ToString();
        }
        else //필요한가?
        {
            Clear();
        }
    }

    public void Clear() //슬롯UI 초기화
    {
        item = null;
        quantity = 0;
        itemImage.sprite = null;
        itemImage.enabled = false;
        itemQuantityText.text = "";
    }

    public void OnClick() //슬롯 클릭시 아이템 1개 제거
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
