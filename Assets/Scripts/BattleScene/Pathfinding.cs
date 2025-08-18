using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PriorityQueue<T>
{
    private List<(T item, int priority)> elements = new List<(T, int)>();

    public int Count => elements.Count;

    public void Enqueue(T item, int priority)
    {
        elements.Add((item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;
        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].priority < elements[bestIndex].priority)
                bestIndex = i;
        }

        T bestItem = elements[bestIndex].item;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }
}

public static class Pathfinding
{
    public static List<Vector3Int> FindPath(Tilemap groundTilemap, Vector3Int start, Vector3Int goal, Unit movingUnit)
    { 
        List<Vector3Int> path = new();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new();
        Dictionary<Vector3Int, int> costSoFar = new();

        PriorityQueue<Vector3Int> frontier = new();
        frontier.Enqueue(start, 0);

        cameFrom[start] = start;
        costSoFar[start] = 0;

        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right
        };

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            if (current == goal) break;

            foreach (Vector3Int dir in directions)
            {
                Vector3Int next = current + dir;
               
                if (!groundTilemap.HasTile(next)) continue; // 오직 Ground 타일만 통과      
                if (GetUnitOnTile(next) != null) continue; // 이동 위치에 유닛이 존재하면 차단

                int newCost = costSoFar[current] + 1;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    int priority = newCost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        }

        Vector3Int cur = goal;
        while (cur != start)
        {
            path.Insert(0, cur);
            if (cameFrom.ContainsKey(cur))
                cur = cameFrom[cur];
            else break; 
        }
        return path;
    }

    private static int Heuristic(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
    public static Unit GetUnitOnTile(Vector3Int tile) // tile 좌표 위에 위치한 유닛 반환
    {
        foreach (var unit in BattleManager.Instance.alivePlayerUnits)
        {
            if (unit.groundTilemap.WorldToCell(unit.transform.position) == tile)
                return unit;
        }
        foreach (var unit in BattleManager.Instance.EnemyUnits)
        {
            if (unit.groundTilemap.WorldToCell(unit.transform.position) == tile)
                return unit;
        }
        return null;
    }
}