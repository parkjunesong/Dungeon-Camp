using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerUnit : Unit
{
    public override void Init()
    {
        /*
        List<Skill_Base> Skills = new();
        foreach(Skill_Base skill in Data.Skills)
        {
            Skill_Base instance = Instantiate(skill);
            instance.SetEffect();
            Skills.Add(instance);
        }
        Skill = new Unit_Skill(Skills);
        */

        Ability = new Unit_Ablity(Data);
        Animation = new Unit_Animation(this);
        ActionController = gameObject.AddComponent<Unit_Action>();
        ActionController.Initialize();
        Ui = gameObject.AddComponent<Unit_Ui>();
        Ui.Initialize();
        /*
        Buff = gameObject.AddComponent<Unit_Buff>();
        if (Data.Passive != null)
        {
            Passive = Instantiate(Data.Passive, transform).GetComponent<Unit_Passive>();
        }
        */
    }
    public override void TurnStart()
    {
        /*
        Skill.OnTurnStart();
        Buff.OnTurnStart();
        if (Passive != null)
            Passive.OnTurnStart();
        */
    }
    public override void TurnEnd()
    {
        /*
        Skill.OnTurnEnd();
        Buff.OnTurnEnd();
        if (Passive != null)
            Passive.OnTurnEnd();
        */
    }
}