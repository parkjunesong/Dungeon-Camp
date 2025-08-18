using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitClass { Novice, Swordsman, Archer, Rogue, Wizard, Shaman };

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData")]
public class UnitData : ScriptableObject
{
    public string Name;
    public UnitClass Class;
    public int AT, AS, HP, DF, AR, MS;
    public float CR, CD;
    public Sprite Face, Standing;
    //public List<Skill_Base> Skills;
    //public GameObject Passive;
}
