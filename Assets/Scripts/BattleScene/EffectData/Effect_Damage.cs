using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Effect_Damage : Effect_Base
{
    public Effect_Damage(float value, int attackrange, int effectrange, EffectTarget target, EffectType type) : base(value, attackrange, effectrange, target, type)
    {

    }

    public override void Execute(Unit caster, List<Unit> targets)
    {
        foreach (var target in targets)
        {
            target.OnDamaged(getDamage(caster));
        }    
    }
    public float getDamage(Unit caster)
    {
        float damage = Effect_Value * caster.Ability.AT * (1 + caster.Ability.ID / 100);
        if (caster.Ability.CR >= Random.Range(0, 100)) 
            damage *= (1 + caster.Ability.CD / 100);

        return damage;
    }
}
