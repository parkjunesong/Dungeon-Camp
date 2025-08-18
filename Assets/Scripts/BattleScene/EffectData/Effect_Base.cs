using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public enum EffectTarget { Ground, Player, Enemy };
public enum EffectType { SetTarget, SetArea, Projectile };
public enum EffectPriority { None, Strongest, Weakest, Nearest, Farest}

public abstract class Effect_Base
{
    protected bool isAction;
    public float Effect_Value;
    public int Effect_Range; // 효과 범위
    public int Attack_Range; // caster를 중심으로 반지름
    public EffectTarget Effect_Target;
    public EffectType Effect_Type;
    public EffectPriority Effect_Priority;

    public Effect_Base(float value, int attackrange, int effectrange, EffectTarget target, EffectType type, EffectPriority priority, bool isaction = false)
    {       
        Effect_Value = value;
        Effect_Range = effectrange;
        Attack_Range = attackrange;
        Effect_Target = target;
        Effect_Type = type;
        Effect_Priority = priority;
        isAction = isaction;
    }
    public abstract void Execute(Unit caster, List<Vector3Int> TargetTilePos);

    public List<Vector3Int> setPriority(List<Vector3Int> TargetTilePos)
    {
        switch (Effect_Priority)
        {
            case EffectPriority.None: // All
                {
                    return TargetTilePos;
                }
            case EffectPriority.Strongest:
                {
                    if (Effect_Target == EffectTarget.Ground) break;
                    break;
                }
            case EffectPriority.Weakest:
                {
                    if (Effect_Target == EffectTarget.Ground) break;
                    break;
                }
            case EffectPriority.Nearest:
                {
                    return new List<Vector3Int>() { TargetTilePos[0] };
                }
            case EffectPriority.Farest:
                {
                    return new List<Vector3Int>() { TargetTilePos[TargetTilePos.Count - 1] };
                }
        }
        return null;
    }
    public List<Unit> FindTarget(List<Vector3Int> TargetTilePos)
    {
        if (TargetTilePos.Count == 0 || Effect_Target == EffectTarget.Ground) return new List<Unit>();

        List<Unit> targets = new();

        if (Effect_Target == EffectTarget.Enemy)
        {
            foreach (var unit in BattleManager.Instance.EnemyUnits)
            {
                Vector3Int unitTile = unit.groundTilemap.WorldToCell(unit.transform.position);
                if (TargetTilePos.Contains(unitTile))
                    targets.Add(unit);
            }
        }
        else if (Effect_Target == EffectTarget.Player)
        {
            foreach (var unit in BattleManager.Instance.alivePlayerUnits)
            {
                Vector3Int unitTile = unit.groundTilemap.WorldToCell(unit.transform.position);
                if (TargetTilePos.Contains(unitTile))
                    targets.Add(unit);
            }
        }      
        return targets;
    }
}
