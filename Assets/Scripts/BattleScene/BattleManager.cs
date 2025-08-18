using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();
    public List<Unit> alivePlayerUnits = new();
    public List<Unit> deadPlayerUnits = new();

    public int Turn;
    GameObject TurnUi;
    public int ActionPoint;
    public int MaxActionPoint;
    GameObject ActionPointUi;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void BattleStart()
    {
        Turn = 0;
        TurnUi = GameObject.Find("Turn");
        ActionPoint = 3;
        MaxActionPoint = 3;
        ActionPointUi = GameObject.Find("ActionPoint");
        MapData md = SystemManager.Instance.StageData.MapData.GetComponent<MapData>();
        DeckData dd = SystemManager.Instance.DeckData;

        for (int i = 0; i< dd.PlayerUnitData.Count; i++)
        {
            UnitSpawnManager.Instance.Spawn(dd.PlayerUnitData[i], md.Layers[0].Tilemap[0], md.PlayerSpawnPoint[i], "Player");
        }
        for (int i = 0; i < SystemManager.Instance.StageData.EnemyUnitData.Count; i++)
        {
            UnitSpawnManager.Instance.Spawn(SystemManager.Instance.StageData.EnemyUnitData[i], md.Layers[0].Tilemap[0], md.EnemySpawnPoint[i], "Enemy");
        }
        alivePlayerUnits = PlayerUnits;
    
        TurnStart();
    }

    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        //gameObject.GetComponent<SkillManager>().uiReset();

        if (Turn % 3 == 0) MaxActionPoint++;
        ActionPoint = MaxActionPoint;
        ActionPointUi.GetComponent<Text>().text = ActionPoint + " / "+ MaxActionPoint+" Point";

        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].TurnStart();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].TurnStart();
        }       
    }
    public void TurnEnd()
    {
        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].TurnEnd();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].TurnEnd();
        }
        TurnStart();
    }
    public void UseActionPoint()
    {
        ActionPoint--;
        ActionPointUi.GetComponent<Text>().text = ActionPoint + " / " + MaxActionPoint + " Point";
        if (ActionPoint <= 0) TurnEnd();
    }
    public void OnUnitDied(Unit unit)
    {      
        if (unit.Ability.Team == "Player")
        {            
            unit.Ability.State = UnitState.Dead;
            deadPlayerUnits.Add(unit);           
        }
        else if (unit.Ability.Team == "Enemy")
        {
            EnemyUnits.Remove(unit);           
            Destroy(unit.gameObject);
        }
    }   
}