using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSelector : MonoBehaviour
{
    public static TileSelector Instance;
    void Awake() { if (Instance != null) { Destroy(gameObject); return; } Instance = this; }

    public Vector3Int GetClickedTile(Tilemap groundTilemap)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return groundTilemap.WorldToCell(mouseWorldPos);
    }
    public Unit GetUnitOnTile(Tilemap groundTilemap, Vector3Int tilePos, string team)
    {
        if(team == "Player")
        {
            foreach (Unit u in BattleManager.Instance.alivePlayerUnits)
            {
                Vector3Int unitTile = groundTilemap.WorldToCell(u.transform.position);
                if (unitTile == tilePos) return u;
            }
        }
        if(team == "Enemy")
        {
            foreach (Unit u in BattleManager.Instance.EnemyUnits)
            {
                Vector3Int unitTile = groundTilemap.WorldToCell(u.transform.position);
                if (unitTile == tilePos) return u;
            }
        }
        return null;
    }
}