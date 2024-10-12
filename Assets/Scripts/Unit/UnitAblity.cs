using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class UnitAblity
{
    public string Team;
    public string Name;
    public string Class;
    public UnitType UT;
    public int AT, AS, HP, DF, AR, MS;
    public float CR, CD;

    public UnitAblity(UnitData data)
    {
        Name = data.Name;
        Class = data.Class;
        UT = data.UT;
        AT = data.AT;
        AS = data.AS;
        HP = data.HP;
        DF = data.DF;
        CR = data.CR;
        CD = data.CD;
        AR = data.AR;
        MS = data.MS;
    }

    public void Damaged(float damage, int ignore)
    {
        float dam = damage * 100 / (100 + DF * (1 - ignore / 100));
        HP -= (int)dam;//(int)(dam * (1 - RD / 100));
        if (HP <= 0) HP = 0;
    }
}
