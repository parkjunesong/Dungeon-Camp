using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card_Base : MonoBehaviour
{
    public int Card_Cost;
    public string Card_Name;
    public Sprite Card_Icon;
    public abstract void execute();
}
