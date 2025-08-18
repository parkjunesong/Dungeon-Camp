using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor.Playables;
using UnityEngine;

public enum UnitState
{
    Alive,       // 기본 상태
    Dead,        // 사망
    Summon,      // 소환된 유닛 (소환수 등)
    Structure    // 구조물 (타겟팅 불가, 이동 불가 등)
}

public class Unit_Ablity
{
    private UnitData Data;
    //private BuffModifier BuffModifiers;

    public UnitState State;
    public string Team;
    public string Name;
    public UnitClass Class;
    public int AT, AS, HP, AR, MS;
    public float CR, CD;

    // 스테이터스 미표기
    public int DF, RD, ID; 
    public int Shild;
    public int maxHP;

    public Unit_Ablity(UnitData data)
    {
        Data = data;
        State = UnitState.Alive;
        Team = "";
        Name = data.Name;
        Class = data.Class;
        AT = data.AT;
        AS = data.AS;
        HP = data.HP;
        maxHP = HP;
        DF = data.DF;
        AR = data.AR;
        MS = data.MS;
        CR = data.CR;
        CD = data.CD;
        RD = 0;
        ID = 0;
        Shild = 0;
    }

    public void OnDamaged(Unit unit, float damage)
    {
        float dam = damage * 100 / (100 + DF);
        FloatingTextManager.Instance.ShowFloatingText(unit.transform, ((int)(dam * (1.0f - RD / 100))).ToString(), Color.red);

        if (Shild > 0)
        {
            Shild -= (int)(dam * (1.0f - RD / 100));
            if (Shild <= 0)
            {
                HP -= -Shild;
                Shild = 0;
            }
        }
        else
        {
            HP -= (int)(dam * (1.0f - RD / 100));
        }
    }
    public void OnHealed(Unit unit, float heal)
    {
        FloatingTextManager.Instance.ShowFloatingText(unit.transform, heal.ToString(), Color.green);

        if (heal + HP < maxHP)
            HP += (int)(heal);
        else
            HP = maxHP;
    }
    public void OnShieldGained(Unit unit, float shild)
    {
        FloatingTextManager.Instance.ShowFloatingText(unit.transform, shild.ToString(), Color.yellow);
        Shild += (int)(shild);
    }

    /*
    public void RecalculBuff(List<Buff_Base> buffs)
    {
        BuffModifiers = new BuffModifier();

        foreach (var buff in buffs)
        {
            BuffModifiers.AT += buff.Modifier.AT * buff.Level;
            BuffModifiers.DF += buff.Modifier.DF * buff.Level;
            BuffModifiers.SP += buff.Modifier.SP * buff.Level;
            BuffModifiers.HP += buff.Modifier.HP * buff.Level;
            BuffModifiers.CR += buff.Modifier.CR * buff.Level;
            BuffModifiers.CD += buff.Modifier.CD * buff.Level;
            BuffModifiers.RD += buff.Modifier.RD * buff.Level;
            BuffModifiers.ID += buff.Modifier.ID * buff.Level;

            AT = (int)(Data.AT * (1 + BuffModifiers.AT / 100));
            SP = (int)(Data.SP * (1 + BuffModifiers.SP));
            maxHP = (int)(Data.HP * (1 + BuffModifiers.HP / 100));
            DF = (int)(Data.DF * (1 + BuffModifiers.DF / 100));
            CR = Data.CR + BuffModifiers.CR;
            CD = Data.CD + BuffModifiers.CD;
            RD = Data.RD + BuffModifiers.RD;
            ID = Data.ID + BuffModifiers.ID;
        }
    }
    */
}
