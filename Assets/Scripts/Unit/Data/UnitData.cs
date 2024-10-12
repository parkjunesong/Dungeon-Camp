using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Unit_Alive, Unit_Dead, Summoned, Object }
[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData", order = int.MaxValue)]
public class UnitData : ScriptableObject
{
    public string Name;
    public string Class;
    public UnitType UT;
    public int AT, AS, HP, DF, AR, MS;
    public float CR, CD;
    public Sprite Face, Standing;
}
