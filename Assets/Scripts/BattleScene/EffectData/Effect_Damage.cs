using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Effect_Damage : Effect_Base
{
    public Effect_Damage(float value, int attackrange, int effectrange, EffectTarget target, EffectType type, EffectPriority priority = EffectPriority.None, bool isaction = false) : base(value, attackrange, effectrange, target, type, priority, isaction) { }

    public override void Execute(Unit caster, List<Vector3Int> TargetTilePos)
    {
        foreach (var target in FindTarget(setPriority(TargetTilePos)))
        {
            target.OnDamaged(getDamage(caster));
        }
        if(isAction) BattleManager.Instance.UseActionPoint(caster);
    }
    public float getDamage(Unit caster)
    {
        float damage = Effect_Value * caster.Ability.AT * (1 + caster.Ability.ID / 100);
        if (caster.Ability.CR >= Random.Range(0, 100)) 
            damage *= (1 + caster.Ability.CD / 100);

        return damage;
    }
}
