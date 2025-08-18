using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Card_CharaSkill", menuName = "Scriptable Object/Card/CharaSkill")]
public class Card_CharaSkill : Card_Base
{
    Unit caster;
    Effect_Base effect;
    Tilemap tile;
    public override void SetEffect()
    {
        
    }
    public override void Execute()
    {
        caster = BattleManager.Instance.alivePlayerUnits[0];
        effect = new Effect_Damage(1, 4, 1, EffectTarget.Enemy, EffectType.SetTarget);

        EffectPreviewManager.Instance.StartTargeting(caster, effect, caster.groundTilemap);
    }
}
