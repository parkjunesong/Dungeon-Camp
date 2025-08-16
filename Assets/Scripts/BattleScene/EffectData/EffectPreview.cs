using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EffectPreview : MonoBehaviour
{
    public static EffectPreview Instance;

    public GameObject attackHighlightPrefab;   // 공격 사거리용
    public GameObject effectHighlightPrefab;   // 효과 범위용
    public int poolSize = 50;

    private Queue<GameObject> attackPool = new();
    private Queue<GameObject> effectPool = new();
    private List<GameObject> activeAttackHighlights = new();
    private List<GameObject> activeEffectHighlights = new();

    private Coroutine targetingCoroutine;

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




    public void StartTargeting(Unit caster, Effect_Base effect, Camera cam, Tilemap groundTilemap)
    {
        if (targetingCoroutine != null) StopCoroutine(targetingCoroutine);
        targetingCoroutine = StartCoroutine(TargetingCoroutine(caster, effect, cam, groundTilemap));
    }

    IEnumerator TargetingCoroutine(Unit caster, Effect_Base effect, Camera cam, Tilemap groundTilemap)
    {
        Vector3Int casterTile = groundTilemap.WorldToCell(caster.transform.position);

        ShowAttackRange(casterTile, effect.Attack_Range, groundTilemap); // 유닛 사거리 표시

        while (true)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -cam.transform.position.z;
            Vector3Int mouseTile = groundTilemap.WorldToCell(cam.ScreenToWorldPoint(mouseScreenPos));

            if (Vector3Int.Distance(casterTile, mouseTile) <= effect.Attack_Range)
            {
                ShowEffectRange(mouseTile, effect.Effect_Range, groundTilemap);

                if (Input.GetMouseButtonDown(0))
                {
                    // skill.Execute(caster, mouseTile, groundTilemap);
                    ClearAll();
                    targetingCoroutine = null;
                    yield break;
                }
            }
            else
            {
                ShowAttackRange(casterTile, effect.Attack_Range, groundTilemap);
                ClearEffectRange();
            }

            yield return null;
        }
    }
    public void ShowAttackRange(Vector3Int centerTile, int range, Tilemap groundTilemap)
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
    public void ShowEffectRange(Vector3Int centerTile, int range, Tilemap groundTilemap)
    {
        ClearEffectRange();

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
                }
            }
        }
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