using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Effect_Teleport : Effect_Base
{
    public Effect_Teleport(float value, int attackrange, int effectrange, EffectTarget target, EffectType type, EffectPriority priority = EffectPriority.None, bool isaction = false) : base(value, attackrange, effectrange, target, type, priority, isaction) { }

    public override void Execute(Unit caster, List<Vector3Int> TargetTilePos)
    {
        Vector3Int target = setPriority(TargetTilePos)[0];

        if (Effect_Target != EffectTarget.Ground) return;
        if (target == caster.groundTilemap.WorldToCell(caster.transform.position)) return; // �ڽ� ��ġ�� �̵� �Ұ�
        if(Pathfinding.GetUnitOnTile(target) != null) return; // ���� ��ġ�� �̵� �Ұ�

        caster.transform.position = caster.groundTilemap.GetCellCenterWorld(target);
        caster.Ui.UpdateUiPos();

        if (isAction) BattleManager.Instance.UseActionPoint();
    }
}
