using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Card_CharaSkill", menuName = "Scriptable Object/Card/CharaSkill")]
public class Card_CharaSkill : Card_Base
{
    Effect_Base effect;
    EffectType type;
    Tilemap tile;
    Vector3Int casterTile;   
    public override void SetEffect()
    {
        
    }
    public override void Execute()
    {
        tile = SystemManager.Instance.StageData.MapData.GetComponent<MapData>().Layers[0].Tilemap[0];
        casterTile = tile.WorldToCell(BattleManager.Instance.alivePlayerUnits[0].transform.position);
        effect = new Effect_Damage(1, 7, 1, EffectTarget.Enemy, EffectType.SetUnit);
        type = EffectType.Projectile;

        EffectPreviewManager.Instance.StartTargeting(effect, type, casterTile, tile);
    }
}
