using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class EnemyUnit : Unit
{
    public EffectPriority MovePriority = EffectPriority.Nearest;
    public bool useFixedTarget = false;
    public Vector3Int fixedTargetTile;

    public override void Init()
    {
     
    }
    public override void TurnStart()
    {       
        
    }   
    public override void TurnEnd()
    {
             
    }
    public IEnumerator MoveCoroutine() 
    {
        Ui.ShowInfo(false);

        Vector3Int startTile = groundTilemap.WorldToCell(transform.position);
        Vector3Int endTile = useFixedTarget ? fixedTargetTile : SelectTarget(Pathfinding.GetPlayerUnitTiles());
        List<Vector3Int> path = Pathfinding.FindPath(groundTilemap, startTile, endTile, this);

        for (int i = 0; i < path.Count; i++)
        {
            if (i >= Ability.MS) break;

            Vector3 worldPos = groundTilemap.GetCellCenterWorld(path[i]);
            while (Vector3.Distance(transform.position, worldPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, worldPos, 5f * Time.deltaTime);
                yield return null;
            }
        }
        //transform.position = 
        Ui.UpdateUiPos();
        Ui.ShowInfo(true);
    }

    private Vector3Int SelectTarget(List<Vector3Int> TargetTiles)
    {
        Vector3Int TargetTile = new();
        switch (MovePriority)
        {
            case EffectPriority.Nearest:
                {
                    Vector3Int startTile = groundTilemap.WorldToCell(transform.position);
                    int minDist = int.MaxValue;

                    foreach (var tile in TargetTiles)
                    {
                        int dist = Mathf.Abs(startTile.x - tile.x) + Mathf.Abs(startTile.y - tile.y);
                        if (dist < minDist)
                        {
                            minDist = dist;
                            TargetTile = tile;
                        }
                    }
                    break;
                }
            case EffectPriority.Farest:
                {
                    Vector3Int startTile = groundTilemap.WorldToCell(transform.position);
                    int maxDist = 0;

                    foreach (var tile in TargetTiles)
                    {
                        int dist = Mathf.Abs(startTile.x - tile.x) + Mathf.Abs(startTile.y - tile.y);
                        if (dist >= maxDist)
                        {
                            maxDist = dist;
                            TargetTile = tile;
                        }
                    }
                    break;
                }
        }
        Vector3Int[] adj = { TargetTile + Vector3Int.up, TargetTile + Vector3Int.down, TargetTile + Vector3Int.left, TargetTile + Vector3Int.right };
        foreach (var adjTile in adj) // 사면이 다 막힐 경우 목적지에서 패싱되는 문제
        {
            if (!groundTilemap.HasTile(adjTile)) continue; // ground 타일이 아니거나     
            if (Pathfinding.GetUnitOnTile(adjTile) != null) { Debug.Log("d"); continue; } // 유닛이 위치할 경우
            return adjTile;
        }
        return TargetTile;
    }
}