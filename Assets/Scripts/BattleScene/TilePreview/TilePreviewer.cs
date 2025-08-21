using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePreviewer : MonoBehaviour
{
    public GameObject highlightPrefab = null;
    private int poolSize = 50;
    private Queue<GameObject> pool = new();
    private List<GameObject> activeHighlights = new();

    public void Init()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(highlightPrefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    private GameObject GetFromPool()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(highlightPrefab, transform);
            return obj;
        }
    }
    private void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
    public void HighlightTiles(List<Vector3Int> tiles, Tilemap tilemap)
    {
        foreach (var tile in tiles)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(tile);
            GameObject obj = GetFromPool();
            obj.transform.position = worldPos;

            SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
            sr.sortingLayerName = tilemap.GetComponent<TilemapRenderer>().sortingLayerName;
            sr.sortingOrder = tilemap.GetComponent<TilemapRenderer>().sortingOrder + 1;

            activeHighlights.Add(obj);
        }
    }
    public void ClearHighlights()
    {
        foreach (var obj in activeHighlights) ReturnToPool(obj);
        activeHighlights.Clear();
    }
}