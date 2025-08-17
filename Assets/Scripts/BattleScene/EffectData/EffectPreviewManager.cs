using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static UnityEngine.GraphicsBuffer;

public class EffectPreviewManager : MonoBehaviour
{
    public static EffectPreviewManager Instance;

    public GameObject attackHighlightPrefab;   // 공격 사거리용
    public GameObject effectHighlightPrefab;   // 효과 범위용
    public int poolSize = 50;

    private Queue<GameObject> attackPool = new();
    private Queue<GameObject> effectPool = new();
    private List<GameObject> activeAttackHighlights = new();
    private List<GameObject> activeEffectHighlights = new();

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;

        InitPool(attackHighlightPrefab, attackPool);
        InitPool(effectHighlightPrefab, effectPool);
    }
    void InitPool(GameObject prefab, Queue<GameObject> pool)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    GameObject GetFromPool(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return Instantiate(prefab, transform);
        }
    }
    void ReturnToPool(GameObject obj, Queue<GameObject> pool)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void StartTargeting(Unit caster, Effect_Base effect, Tilemap groundTilemap)
    {
        StartCoroutine(TargetingRoutine(caster, effect, groundTilemap));
    }

    private IEnumerator TargetingRoutine(Unit caster, Effect_Base effect, Tilemap groundTilemap)
    {
        List<Unit> units = new();
        Vector3Int casterTile = groundTilemap.WorldToCell(caster.transform.position);
        Vector3Int lastmouseTile = groundTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                effect.Execute(caster, units);
                ClearAll();
                yield break;
            }
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -Camera.main.transform.position.z;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector3Int mouseTile = groundTilemap.WorldToCell(mouseWorldPos);

            if (mouseTile != lastmouseTile)
            {
                lastmouseTile = mouseTile;

                switch (effect.Effect_Type)
                {
                    case EffectType.SetUnit:
                        {
                            ShowAttackRange(casterTile, effect.Attack_Range, groundTilemap);
                            if (Vector3Int.Distance(casterTile, mouseTile) <= effect.Attack_Range)
                            {
                                units = ShowEffectRange(mouseTile, effect.Effect_Range, groundTilemap, effect.Effect_Target);
                            }
                            break;
                        }
                    case EffectType.SetArea:
                        {
                            break;
                        }
                    case EffectType.Projectile:
                        {
                            Vector3 diff = mouseWorldPos - caster.transform.position;
                            Vector3Int dir;
                            if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y)) dir = (diff.x > 0) ? Vector3Int.right : Vector3Int.left;
                            else dir = (diff.y > 0) ? Vector3Int.up : Vector3Int.down;

                            units = ShowProjectilePath(casterTile, dir, effect.Attack_Range, groundTilemap, effect.Effect_Target);
                            break;
                        }
                }
            }    
            yield return null;
        }          
    }

    private void ShowAttackRange(Vector3Int centerTile, int range, Tilemap groundTilemap)
    {
        ClearAttackRange();

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                Vector3Int checkTile = centerTile + new Vector3Int(x, y, 0);

                if (Vector3Int.Distance(centerTile, checkTile) <= range)
                {
                    Vector3 worldPos = groundTilemap.GetCellCenterWorld(checkTile);

                    GameObject obj = GetFromPool(attackPool, attackHighlightPrefab);
                    SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
                    obj.transform.position = worldPos;
                    sr.sortingLayerName = groundTilemap.GetComponent<TilemapRenderer>().sortingLayerName;
                    sr.sortingOrder = groundTilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;
                    activeAttackHighlights.Add(obj);
                }
            }
        }
    }
    private List<Unit> ShowEffectRange(Vector3Int centerTile, int range, Tilemap groundTilemap, EffectTarget target)
    {
        ClearEffectRange();
        List<Unit> units = new();

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                Vector3Int checkTile = centerTile + new Vector3Int(x, y, 0);

                if (Vector3Int.Distance(centerTile, checkTile) <= range)
                {
                    Vector3 worldPos = groundTilemap.GetCellCenterWorld(checkTile);

                    GameObject obj = GetFromPool(effectPool, effectHighlightPrefab);
                    SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
                    obj.transform.position = worldPos;
                    sr.sortingLayerName = groundTilemap.GetComponent<TilemapRenderer>().sortingLayerName;
                    sr.sortingOrder = groundTilemap.GetComponent<TilemapRenderer>().sortingOrder + 2;
                    activeEffectHighlights.Add(obj);

                    if (target == EffectTarget.Enemy)
                    {
                        foreach (var unit in BattleManager.Instance.EnemyUnits)
                            if (groundTilemap.WorldToCell(unit.transform.position) == checkTile) units.Add(unit);
                    }
                    else if (target == EffectTarget.Player)
                    {
                        foreach (var unit in BattleManager.Instance.alivePlayerUnits)
                            if (groundTilemap.WorldToCell(unit.transform.position) == checkTile) units.Add(unit);
                    }
                }
            }
        }
        return units;
    }
    private List<Unit> ShowProjectilePath(Vector3Int centerTile, Vector3Int dir, int range, Tilemap groundTilemap, EffectTarget target)
    {
        ClearEffectRange();
        List<Unit> units = new ();

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

            if(target == EffectTarget.Enemy)
            {
                foreach (var unit in BattleManager.Instance.EnemyUnits)
                    if (groundTilemap.WorldToCell(unit.transform.position) == checkTile) units.Add(unit);
            }
            else if(target == EffectTarget.Player)
            {
                foreach (var unit in BattleManager.Instance.alivePlayerUnits)
                    if (groundTilemap.WorldToCell(unit.transform.position) == checkTile) units.Add(unit);                                
            }     
        }
        return units;
    }

    public void ClearAttackRange()
    {
        foreach (var obj in activeAttackHighlights)
            ReturnToPool(obj, attackPool);
        activeAttackHighlights.Clear();
    }
    public void ClearEffectRange()
    {
        foreach (var obj in activeEffectHighlights)
            ReturnToPool(obj, effectPool);
        activeEffectHighlights.Clear();
    }
    public void ClearAll()
    {
        ClearAttackRange();
        ClearEffectRange();
    }
}