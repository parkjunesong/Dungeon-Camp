using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class EffectPreview : MonoBehaviour
{
    public static EffectPreview Instance;
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

    public IEnumerator SelectTile(Unit caster, Effect_Base effect)
    {
        Vector3Int startTile = caster.groundTilemap.WorldToCell(caster.transform.position);
        List<Vector3Int> selectableTiles = GetAttackRange(startTile, caster.Ability.AR, caster.groundTilemap);
        List<Vector3Int> selectedTiles = new();

        RangeTilePreviewer.HighlightTiles(selectableTiles, caster.groundTilemap);
        while (true)
        {
            Vector3Int mousetile = GetMouseTile(caster.groundTilemap);

            if (Input.GetMouseButtonDown(0)) break;
            if (selectableTiles.Contains(mousetile)) 
                selectedTiles = ShowEffectRange(effect, mousetile, caster.groundTilemap);
            yield return null;
        }
        EffectTilePreviewer.ClearHighlights();
        RangeTilePreviewer.ClearHighlights();
        yield return selectedTiles;
        
    }

    private List<Vector3Int> GetAttackRange(Vector3Int center, int range, Tilemap groundTilemap)
    {
        List<Vector3Int> tiles = new();
        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                Vector3Int tile = center + new Vector3Int(x, y, 0);

                if (Vector3Int.Distance(center, tile) <= range) tiles.Add(tile);
            }
        }
        return tiles;
    }

    public List<Vector3Int> ShowEffectRange(Effect_Base effect, Vector3Int centerTile, Tilemap groundTilemap)
    {
        EffectTilePreviewer.ClearHighlights();
        List<Vector3Int> EffectPreviewTile = new();
        switch (effect.Effect_Type)
        {
            case EffectType.SetTarget:
                EffectPreviewTile = SetTargetEffect(centerTile, effect.Effect_Range);               
                break;
            case EffectType.SetArea:
                
                break;
            case EffectType.Projectile:
                
                break;
        }
        EffectTilePreviewer.HighlightTiles(EffectPreviewTile, groundTilemap);
        return EffectPreviewTile;
    }

    private List<Vector3Int> SetTargetEffect(Vector3Int centerTile, int range)
    {      
        List<Vector3Int> TargetTilePos = new();

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                Vector3Int checkTile = centerTile + new Vector3Int(x, y, 0);

                if (Vector3Int.Distance(centerTile, checkTile) <= range)
                {
                    TargetTilePos.Add(checkTile);                   
                }
            }
        }
        return TargetTilePos;
    }
    public Vector3Int GetMouseTile(Tilemap groundTilemap)
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -Camera.main.transform.position.z;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        return groundTilemap.WorldToCell(mouseWorld);
    }
    /*
    private List<Vector3Int> ProjectileEffect(Vector3Int centerTile, Vector3Int dir, int range, Tilemap groundTilemap)
    {
        highlighter.ClearHighlights();
        List<Vector3Int> TargetTilePos = new();

        for (int i = 1; i <= range; i++)
        {
            Vector3Int checkTile = centerTile + dir * i;
            Vector3 worldPos = groundTilemap.GetCellCenterWorld(checkTile);
            GameObject obj = GetFromPool(effectPool, effectHighlightPrefab);
            SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
            obj.transform.position = worldPos;
            sr.sortingLayerName = groundTilemap.GetComponent<TilemapRenderer>().sortingLayerName;
            sr.sortingOrder = groundTilemap.GetComponent<TilemapRenderer>().sortingOrder + 2;
            activeEffectHighlights.Add(obj);

            TargetTilePos.Add(checkTile);
        }
        return TargetTilePos;
    } 
    */
}