using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum EffectTarget { Player, Enemy };
public enum EffectType { SetUnit, SetArea, Projectile };
public enum EffectPriority { Strongest, Weakest, Nearest, Farest, All};

public abstract class Effect_Base
{
    public float Effect_Value;
    public int Effect_Range; // 효과 범위
    public int Attack_Range; // caster를 중심으로 반지름
    public EffectTarget Effect_Target;
    public EffectType Effect_Type;

    public Effect_Base(float value, int attackrange, int effectrange, EffectTarget target, EffectType type)
    {
        Effect_Value = value;
        Effect_Range = effectrange;
        Attack_Range = attackrange;
        Effect_Target = target;
        Effect_Type = type;
    }
    public abstract void Execute();

    /*
    public List<Unit> setTarget(Unit target) // 효과 대상 탐색
    {
        if (Effect_Type == EffectType.Projectile)
        {

        }
        else if (Effect_Type == EffectType.SetArea)
        {

        }
        else if (Effect_Type == EffectType.SetUnit)
        {
            
        }
        else return null;

        if (Effect_Target == EffectTarget.Enemy)
        {
            
        }
        else if (Effect_Target == EffectTarget.Player)
        {
            
        }
    }
    */
}
