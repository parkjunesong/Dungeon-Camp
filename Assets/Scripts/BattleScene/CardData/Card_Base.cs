using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card_Base : ScriptableObject
{
    public int Card_Cost;
    public string Card_Name;
    public Sprite Card_Icon;
    public List<Effect_Base> EffectList = new();
    public virtual bool IsAvailable() { return true; }
    public abstract void SetEffect();
    public abstract void Execute();
}
