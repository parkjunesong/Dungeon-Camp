using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitSpawnManager : MonoBehaviour
{
    public static UnitSpawnManager Instance { get; private set; }

    public GameObject origin;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Spawn(UnitData data, Tilemap groundTilemap, Vector3Int spawnTilePos, string team)
    {   
        Vector3 worldPos = groundTilemap.GetCellCenterWorld(spawnTilePos);
        GameObject UnitGameObject = Instantiate(origin, worldPos, Quaternion.identity);

        SpriteRenderer sr = UnitGameObject.GetComponentInChildren<SpriteRenderer>();
        sr.sortingLayerName = groundTilemap.GetComponent<TilemapRenderer>().sortingLayerName;
        sr.sortingOrder = groundTilemap.GetComponent<TilemapRenderer>().sortingOrder + 10;

        if (team == "Player")
            UnitGameObject.AddComponent<PlayerUnit>();
        else if (team == "Enemy")
            UnitGameObject.AddComponent<EnemyUnit>();

        Unit unit = UnitGameObject.GetComponent<Unit>();
        unit.Data = Instantiate(data);
        unit.Init();
        unit.Ability.Team = team;
        unit.name = unit.Ability.Name;

        if (team == "Player")
            BattleManager.Instance.PlayerUnits.Add(unit);
        else if (team == "Enemy")
            BattleManager.Instance.EnemyUnits.Add(unit);
    }
    
}
