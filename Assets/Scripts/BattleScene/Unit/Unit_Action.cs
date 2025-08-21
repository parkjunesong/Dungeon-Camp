using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;

public class Unit_Action : MonoBehaviour
{
    private Unit unit;
    private Effect_Base attack;
    private Effect_Base skill;

    public void Initialize()
    {
        unit = transform.GetComponent<Unit>();

        switch (unit.Ability.Class)
        {
            case UnitClass.Novice:
                {
                    attack = new Effect_Damage(0.4f, unit.Ability.AR, 0, EffectTarget.Enemy, EffectType.SetTarget, EffectPriority.None, true);
                    skill = null;
                    break;
                }
            case UnitClass.Swordsman:
                {
                    attack = new Effect_Damage(1f, unit.Ability.AR, 0, EffectTarget.Enemy, EffectType.SetTarget, EffectPriority.None, true);
                    skill = null;
                    break;
                }
            case UnitClass.Archer:
                {
                    attack = new Effect_Damage(0.8f, unit.Ability.AR, 0, EffectTarget.Enemy, EffectType.Projectile, EffectPriority.Nearest, true);
                    skill = null;
                    break;
                }
            case UnitClass.Rogue:
                {
                    attack = new Effect_Damage(0.8f, unit.Ability.AR, 0, EffectTarget.Enemy, EffectType.SetTarget, EffectPriority.None, true);
                    skill = null;
                    break;
                }
            case UnitClass.Wizard:
                {
                    attack = new Effect_Damage(0.75f, unit.Ability.AR, 1, EffectTarget.Enemy, EffectType.SetTarget, EffectPriority.None, true);
                    skill = new Effect_Teleport(0, 2, 0, EffectTarget.Ground, EffectType.SetTarget, EffectPriority.None, true);
                    break;
                }
            case UnitClass.Shaman:
                {
                    attack = new Effect_Damage(0.6f, unit.Ability.AR, 1, EffectTarget.Enemy, EffectType.SetTarget, EffectPriority.None, true);
                    skill = new Effect_Teleport(0, 2, 0, EffectTarget.Ground, EffectType.SetTarget, EffectPriority.None, true);
                    break;
                }
        }
    }

    public void Move()
    {
        unit.Ui.ShowInfo(false);
        unit.Ui.ShowActionSelector(false);
        StartCoroutine(PlayerMove());
    }
    public void NormalAttack()
    {       
        unit.Ui.ShowActionSelector(false);
        StartCoroutine(PlayerAttack());
    }
    public void ClassSkill()
    {
        unit.Ui.ShowActionSelector(false);
        StartCoroutine(PlayerSkill());
    }




    public IEnumerator PlayerMove()
    {
        Vector3Int playerTile = unit.groundTilemap.WorldToCell(unit.transform.position);

        IEnumerator moveSelect = MovePreview.Instance.SelectTile(unit, unit.Ability.MS);
        while (moveSelect.MoveNext()) yield return null;
        Vector3Int selectedMoveTile = (Vector3Int)moveSelect.Current;
        List<Vector3Int> path = Pathfinding.FindPath(unit.groundTilemap, playerTile, selectedMoveTile, unit);

        StartCoroutine(MoveCoroutine(unit, path));
    }
    public IEnumerator MoveCoroutine(Unit unit, List<Vector3Int> path)
    {
        foreach (var tile in path)
        {
            Vector3 targetWorldPos = unit.groundTilemap.GetCellCenterWorld(tile);

            while (Vector3.Distance(unit.transform.position, targetWorldPos) > 0.01f)
            {
                unit.transform.position = Vector3.MoveTowards(unit.transform.position, targetWorldPos, 5f * Time.deltaTime);
                yield return null;
            }
        }
        unit.Ui.ShowInfo(true);
        unit.Ui.UpdateUiPos();
        BattleManager.Instance.UseActionPoint(unit);
    }
    private IEnumerator PlayerAttack()
    {
        Vector3Int playerTile = unit.groundTilemap.WorldToCell(unit.transform.position);
        IEnumerator attackSelect = EffectPreview.Instance.SelectTile(unit, attack);      
        while (attackSelect.MoveNext()) yield return null;
        List<Vector3Int> selectedAttackTile = (List<Vector3Int>)attackSelect.Current;

        attack.Execute(unit, selectedAttackTile);
    }
    private IEnumerator PlayerSkill()
    {
        Vector3Int playerTile = unit.groundTilemap.WorldToCell(unit.transform.position);
        IEnumerator attackSelect = EffectPreview.Instance.SelectTile(unit, skill);
        while (attackSelect.MoveNext()) yield return null;
        List<Vector3Int> selectedAttackTile = (List<Vector3Int>)attackSelect.Current;

        skill.Execute(unit, selectedAttackTile);
    }
}
