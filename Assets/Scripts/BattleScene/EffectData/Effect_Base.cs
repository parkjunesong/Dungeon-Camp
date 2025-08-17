using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectTarget { Player, Enemy };
public enum EffectType { SetUnit, SetArea, Projectile };
public enum EffectPriority { None, Strongest, Weakest, Nearest, Farest}

public abstract class Effect_Base
{
    public float Effect_Value;
    public int Effect_Range; // 효과 범위
    public int Attack_Range; // caster를 중심으로 반지름
    public EffectTarget Effect_Target;
    public EffectType Effect_Type;
    public EffectPriority Effect_Priority;

    public Effect_Base(float value, int attackrange, int effectrange, EffectTarget target, EffectType type, EffectPriority priority)
    {
        Effect_Value = value;
        Effect_Range = effectrange;
        Attack_Range = attackrange;
        Effect_Target = target;
        Effect_Type = type;
        Effect_Priority = priority;
    }
    public abstract void Execute(Unit caster, List<Unit> targets);   

    public List<Unit> setPriority(List<Unit> targets)
    {
        if (targets.Count == 0) return new List<Unit>();

        switch (Effect_Priority)
        {
            case EffectPriority.None: // All
                {
                    return targets;
                }
            case EffectPriority.Strongest:
                {
                    break;
                }
            case EffectPriority.Weakest:
                {
                    break;
                }
            case EffectPriority.Nearest:
                {
                    return new List<Unit>() { targets[0] };
                }
            case EffectPriority.Farest:
                {
                    return new List<Unit>() { targets[targets.Count - 1] };
                }
        }
        return null;
    }
}
