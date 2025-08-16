using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Card_CharaSkill", menuName = "Scriptable Object/Card/CharaSkill")]
public class Card_CharaSkill : Card_Base
{
    public override void Execute()
    {
        Unit caster = BattleManager.Instance.alivePlayerUnits[0];
        Effect_Base effect = new Effect_Damage(1, 3, 1, EffectTarget.Enemy, EffectType.SetUnit);
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Tilemap tile = SystemManager.Instance.StageData.MapData.GetComponent<MapData>().Layers[0].Tilemap[0];
        EffectPreview.Instance.StartTargeting(caster, effect, cam, tile);
        //new Effect_Damage().Execute();
    }
}
