using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        if(unit.Ability.Team == "Player")
        {
            Effect_Base effect = new Effect_Move(0, unit.Ability.MS+5, 0, EffectTarget.Ground, EffectType.SetTarget, EffectPriority.None, true);
            EffectPreviewManager.Instance.StartTargeting(unit, effect, unit.groundTilemap);
            unit.Ui.ShowActionSelector(false);
        }
        else if(unit.Ability.Team == "Enemy")
        {
            EffectPriority priority = unit.GetComponent<EnemyUnit>().MovePriority;
            List<Vector3Int> playerTiles = Pathfinding.GetPlayerUnitTiles();
            Vector3Int startTile = unit.groundTilemap.WorldToCell(unit.transform.position);
            List<Vector3Int> Path = null;

            foreach (var tile in playerTiles)
            {
                Vector3Int[] adj = {
                    tile + Vector3Int.up,
                    tile + Vector3Int.down,
                    tile + Vector3Int.left,
                    tile + Vector3Int.right
                };
                foreach (var adjTile in adj)
                {
                    if (!unit.groundTilemap.HasTile(adjTile)) continue; // 땅 없음
                    if (Pathfinding.GetUnitOnTile(adjTile) != null) continue; // 다른 유닛이 점유 중

                    List<Vector3Int> path = Pathfinding.FindPath(unit.groundTilemap, startTile, adjTile, unit);
                    if (path != null && (Path == null || path.Count < Path.Count))
                    {
                        Path = path;
                    }
                }              
            }
            if (Path != null && Path.Count > 1)
            {              
                int moveSteps = Mathf.Min(unit.Ability.MS, Path.Count - 1);
                Vector3Int endTile = Path[moveSteps];
                new Effect_Move(0, unit.Ability.MS, 0, EffectTarget.Ground, EffectType.SetTarget, priority, false, true).Execute(unit, new List<Vector3Int> { endTile });
            }
        }
    }
    public void NormalAttack()
    {       
        EffectPreviewManager.Instance.StartTargeting(unit, attack, unit.groundTilemap);
        unit.Ui.ShowActionSelector(false);
    }
    public void ClassSkill()
    {
        EffectPreviewManager.Instance.StartTargeting(unit, skill, unit.groundTilemap);
        unit.Ui.ShowActionSelector(false);
    }
}
