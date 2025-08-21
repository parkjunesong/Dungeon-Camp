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

        bool reachedGoal = false;

        while (frontier.Count > 0)
        {
            Vector3Int current = frontier.Dequeue();
            if (current == goal)
            {
                reachedGoal = true;
                break;
            }

            foreach (Vector3Int dir in directions)
            {
                Vector3Int next = current + dir;

                if (!groundTilemap.HasTile(next)) continue;
                if (GetUnitOnTile(next) != null && next != goal) continue; // ������ ������ �� ��

                int newCost = costSoFar[current] + 1;
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    int priority = newCost + Mathf.Abs(next.x - goal.x) + Mathf.Abs(next.y - goal.y);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        }

        // ���� �Ұ����ϸ� goal ��� "goal�� ���� ����� reachable ���" ã��
        Vector3Int target = goal;
        if (!reachedGoal)
        {
            int bestDist = int.MaxValue;
            Vector3Int bestReachable = start;

            foreach (var kvp in costSoFar)
            {
                int dist = Mathf.Abs(kvp.Key.x - goal.x) + Mathf.Abs(kvp.Key.y - goal.y);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    bestReachable = kvp.Key;
                }
            }

            target = bestReachable;
        }

        // ��� ����
        Vector3Int cur = target;
        while (cur != start)
        {
            path.Insert(0, cur);
            if (cameFrom.ContainsKey(cur))
                cur = cameFrom[cur];
            else break;
        }

        return path;
    }
    
    public static Unit GetUnitOnTile(Vector3Int tile)
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

    public static List<Vector3Int> GetPlayerUnitTiles()
    {
        List<Vector3Int> tiles = new();
        foreach (var unit in BattleManager.Instance.alivePlayerUnits)
        {
            Vector3Int pos = unit.groundTilemap.WorldToCell(unit.transform.position);
            tiles.Add(pos);
        }
        return tiles;
    }
}