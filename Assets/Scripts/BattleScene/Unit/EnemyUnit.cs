using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyUnit : Unit
{
    int skillNo;
    public int MoveCount;
    public EffectPriority MovePriority;
    public override void Init()
    {
        MovePriority = EffectPriority.Nearest;
        /*
        List<Skill_Base> Skills = new List<Skill_Base>();
        foreach (Skill_Base skill in Data.Skills)
        {
            Skill_Base instance = Instantiate(skill);
            instance.SetEffect();
            Skills.Add(instance);
        }
        Skill = new Unit_Skill(Skills);
        */
        
        /*
        Buff = gameObject.AddComponent<Unit_Buff>();
        if (Data.Passive != null)
        {
            Passive = Instantiate(Data.Passive, transform).GetComponent<Unit_Passive>();
        }

        skillNo = 0;
        MoveCount = Skill.SkillList[skillNo].Skill_CoolTime; // ���� ���� ��ų ��ü
        */
    }

    public override void TurnStart()
    {       
        /*
        MoveCount--;
        Ui.UpdateCountText(MoveCount);
        Skill.OnTurnStart();
        Buff.OnTurnStart();
        if (Passive != null)
            Passive.OnTurnStart();
        */
    }
    public override void TurnEnd() // ������ ����Ǵ� ������ ���, �� ��� ����� �ؾ���
    {      
        ActionController.Move();                                       
    }    
}