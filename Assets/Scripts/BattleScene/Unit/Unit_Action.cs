using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Unit_Action : MonoBehaviour
{
    private Unit unit;
    private Effect_Base attack;
    private Effect_Base skill;

    public void Initialize()
    {
        unit = transform.GetComponent<Unit>();

        EffectTarget effectTarget = EffectTarget.Ground;
        if (unit.Ability.Team == "Player") effectTarget = EffectTarget.Enemy;
        else if(unit.Ability.Team == "Enemy") effectTarget = EffectTarget.Player;

        switch (unit.Ability.Class)
        {
            case UnitClass.Novice:
                {
                    attack = new Effect_Damage(0.4f, unit.Ability.AR, 0, effectTarget, EffectType.SetTarget, EffectPriority.None, true);
                    skill = null;
                    break;
                }
            case UnitClass.Swordsman:
                {
                    attack = new Effect_Damage(1f, unit.Ability.AR, 0, effectTarget, EffectType.SetTarget, EffectPriority.None, true);
                    skill = null;
                    break;
                }
            case UnitClass.Archer:
                {
                    attack = new Effect_Damage(0.8f, unit.Ability.AR, 0, effectTarget, EffectType.Projectile, EffectPriority.Nearest, true);
                    skill = null;
                    break;
                }
            case UnitClass.Rogue:
                {
                    attack = new Effect_Damage(0.8f, unit.Ability.AR, 0, effectTarget, EffectType.SetTarget, EffectPriority.None, true);
                    skill = null;
                    break;
                }
            case UnitClass.Wizard:
                {
                    attack = new Effect_Damage(0.75f, unit.Ability.AR, 1, effectTarget, EffectType.SetTarget, EffectPriority.None, true);
                    skill = new Effect_Teleport(0, 2, 0, EffectTarget.Ground, EffectType.SetTarget, EffectPriority.None, true);
                    break;
                }
            case UnitClass.Shaman:
                {
                    attack = new Effect_Damage(0.6f, unit.Ability.AR, 1, effectTarget, EffectType.SetTarget, EffectPriority.None, true);
                    skill = new Effect_Teleport(0, 2, 0, EffectTarget.Ground, EffectType.SetTarget, EffectPriority.None, true);
                    break;
                }
        }
    }

    public void Move()
    {
        Effect_Base effect = new Effect_Move(0, unit.Ability.MS, 0, EffectTarget.Ground, EffectType.SetTarget, EffectPriority.None, true);
        EffectPreviewManager.Instance.StartTargeting(unit, effect, unit.groundTilemap);
    }  
    public void NormalAttack()
    {       
        EffectPreviewManager.Instance.StartTargeting(unit, attack, unit.groundTilemap);
    }
    public void ClassSkill()
    {
        EffectPreviewManager.Instance.StartTargeting(unit, skill, unit.groundTilemap);
    }
}
