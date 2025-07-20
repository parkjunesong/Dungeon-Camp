using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTarget { Player, Enemy };

public abstract class Effect_Base
{
    /*
    public float Effect_Value;
    public int Effect_Range;   
    public EffectTarget Effect_Target;

    public Effect_Base(float value, int range, EffectTarget target)
    {
        Effect_Value = value;
        Effect_Range = range;
        Effect_Target = target;
    }
    public abstract void SetEffectArea();
    public abstract void Execute(Unit caster);

    public List<Unit> setTarget(Unit target) // 대상 지정
    {
        Vector2 centerPos = target.pos;
        List<Unit> unitsInRange = BattleManager.Instance.GetUnitsInRange(centerPos, Effect_Range);
        return FilterByTarget(unitsInRange, caster.Team);
    }
    public List<Unit> setTarget(Vector2 pos) // 좌표 지정
    {

        return null;
    }   
    */
}
