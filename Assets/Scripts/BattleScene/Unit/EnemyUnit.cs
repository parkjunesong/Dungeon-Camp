using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    int skillNo;
    public int MoveCount;
    public override void Init()
    {
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

        skillNo = 0;
        MoveCount = Skill.SkillList[skillNo].Skill_CoolTime; // 패턴 따라 스킬 교체
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
    public override void TurnEnd() // 적에게 적용되는 버프의 경우, 턴 계산 고민좀 해야함
    {       
        /*
        if (MoveCount == 0)
        {
            OnSkillUsed(skillNo);
            MoveCount = Skill.SkillList[0].Skill_CoolTime;
        }
        Skill.OnTurnEnd();
        Buff.OnTurnEnd();
        if (Passive != null)
            Passive.OnTurnEnd();
        */
    }
}