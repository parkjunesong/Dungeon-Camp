using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData", order = int.MaxValue)]
public class UnitData : ScriptableObject
{
    public string Name;
    public string Class;
    public int AT, AS, HP, DF, AR, MS;
    public float CR, CD;
    public Sprite Face, Standing;
    //public List<Skill_Base> Skills;
    //public GameObject Passive;
}
