using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum DamageType { Normal, Penetrate };

public class Effect_Damage : Effect_Base
{
    /*
    public DamageType DamageType;
    public int IgnoreDefence = 0;

    public Effect_Damage(float value, int range, EffectTarget target, DamageType damagetype, int ignoreDefence) : base(value, range, target)
    {
        DamageType = damagetype;
        IgnoreDefence = ignoreDefence;
    }

    public override void Execute(Unit caster)
    {
        foreach (var target in setTarget(caster))
        {
            target.OnDamaged(getDamage(caster), DamageType, IgnoreDefence);
        }               
    }
    public float getDamage(Unit caster)
    {
        float damage = Effect_Value * caster.Ability.AT * (1 + caster.Ability.ID / 100);
        if (caster.Ability.CR >= Random.Range(0, 100)) 
            damage *= (1 + caster.Ability.CD / 100);

        return damage;
    }
    */
}
