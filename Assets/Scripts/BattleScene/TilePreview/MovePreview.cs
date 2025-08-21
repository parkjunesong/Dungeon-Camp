using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovePreview : MonoBehaviour
{
    public static MovePreview Instance;
    private TilePreviewer RangeTilePreviewer;
    private TilePreviewer EffectTilePreviewer;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; } Instance = this;

        RangeTilePreviewer = Instantiate(Resources.Load<TilePreviewer>("TilePreviewer"));
        EffectTilePreviewer = Instantiate(Resources.Load<TilePreviewer>("TilePreviewer"));
        RangeTilePreviewer.highlightPrefab = Resources.Load<GameObject>("RangePreview");
        EffectTilePreviewer.highlightPrefab = Resources.Load<GameObject>("EffectPreview");
        RangeTilePreviewer.Init();
        EffectTilePreviewer.Init();
    }

    public IEnumerator SelectTile(Unit caster, int range)
    {
        Vector3Int startTile = caster.groundTilemap.WorldToCell(caster.transform.position);
        List<Vector3Int> selectableTiles = GetMovableTiles(caster);
        Vector3Int selectedTile = new();

        RangeTilePreviewer.HighlightTiles(selectableTiles, caster.groundTilemap);
        while (true)
        {
            Vector3Int mousetile = GetMouseTile(caster.groundTilemap);

            if (Input.GetMouseButtonDown(0)) break;                        
            if (selectableTiles.Contains(mousetile)) 
            { 
                selectedTile = mousetile;
                EffectTilePreviewer.ClearHighlights();
                EffectTilePreviewer.HighlightTiles(new List<Vector3Int> { selectedTile }, caster.groundTilemap);
            }
            yield return null;
        }
        EffectTilePreviewer.ClearHighlights();
        RangeTilePreviewer.ClearHighlights();
        yield return selectedTile;
    }
    public List<Vector3Int> GetMovableTiles(Unit unit)
    {
        Vector3Int startTile = unit.groundTilemap.WorldToCell(unit.transform.position);
        List<Vector3Int> movableTiles = new();
        Queue<Vector3Int> frontier = new();
        Dictionary<Vector3Int, int> costSoFar = new();

        frontier.Enqueue(startTile);
        costSoFar[startTile] = 0;

        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right
        };

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            int currentCost = costSoFar[current];

            foreach (Vector3Int dir in directions)
            {
                Vector3Int next = current + dir;

                // 타일이 없거나 유닛이 있으면 이동 불가
                if (!unit.groundTilemap.HasTile(next)) continue;
                if (Pathfinding.GetUnitOnTile(next) != null && Pathfinding.GetUnitOnTile(next) != unit) continue;

                int newCost = currentCost + 1;

                // 이동력이 초과하면 넘어감
                if (newCost > unit.Ability.MS) continue;

                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    frontier.Enqueue(next);
                    movableTiles.Add(next);
                }
            }
        }
        return movableTiles;
    }
    public Vector3Int GetMouseTile(Tilemap groundTilemap)
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -Camera.main.transform.position.z;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return groundTilemap.WorldToCell(mouseWorld);
    }
}
