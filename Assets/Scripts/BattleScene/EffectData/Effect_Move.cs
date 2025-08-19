using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Effect_Move : Effect_Base
{
    public float moveSpeed = 5f;
    private bool useMoveLimit;

    public Effect_Move(float value, int attackrange, int effectrange, EffectTarget target, EffectType type, EffectPriority priority = EffectPriority.None, bool isaction = false, bool useLimit = false) : base(value, attackrange, effectrange, target, type, priority, isaction)
    {
        useMoveLimit = useLimit;
    }

    public override void Execute(Unit caster, List<Vector3Int> TargetTilePos)
    {
        Vector3Int target = setPriority(TargetTilePos)[0];

        if (target == caster.groundTilemap.WorldToCell(caster.transform.position)) return; // 자신 위치로 이동 불가

        caster.Ui.ShowInfo(false);
        caster.StartCoroutine(MoveCoroutine(caster, caster.groundTilemap.GetCellCenterWorld(target)));
    }
    private IEnumerator MoveCoroutine(Unit unit, Vector3 targetPos)
    {
        Vector3Int startTile = unit.groundTilemap.WorldToCell(unit.transform.position);
        Vector3Int endTile = unit.groundTilemap.WorldToCell(targetPos);
        List<Vector3Int> path = Pathfinding.FindPath(unit.groundTilemap, startTile, endTile, unit);

        for(int i = 0; i< path.Count; i++)
        {
            if (useMoveLimit && i >= unit.Ability.MS) break;

            Vector3 worldPos = unit.groundTilemap.GetCellCenterWorld(path[i]);
            while ((unit.transform.position - worldPos).sqrMagnitude > 0.01f)
            {
                unit.transform.position = Vector3.MoveTowards(unit.transform.position, worldPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        unit.transform.position = targetPos;
        unit.Ui.ShowInfo(true);
        unit.Ui.UpdateUiPos();
        if (isAction) BattleManager.Instance.UseActionPoint();
    }
}
