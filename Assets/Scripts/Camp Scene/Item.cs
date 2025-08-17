using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Scriptable Object/Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
}
