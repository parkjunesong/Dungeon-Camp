using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnManager : MonoBehaviour
{
    public static UnitSpawnManager Instance { get; private set; }

    public GameObject origin;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Spawn(UnitData data, Vector2 xy, string team)
    {
        UnitData uData = Instantiate(data);
        GameObject UnitGameObject = Instantiate(origin, new Vector2(xy.x, xy.y), Quaternion.identity);

        if (team == "Player")
            UnitGameObject.AddComponent<PlayerUnit>();
        else if (team == "Enemy")
            UnitGameObject.AddComponent<EnemyUnit>();

        Unit unit = UnitGameObject.GetComponent<Unit>();
        unit.Data = uData;
        unit.Init();
        unit.Ability.Team = team;
        unit.name = unit.Ability.Name;

        if (team == "Player")
            BattleManager.Instance.PlayerUnits.Add(unit);
        else if (team == "Enemy")
            BattleManager.Instance.EnemyUnits.Add(unit);
    }
}
