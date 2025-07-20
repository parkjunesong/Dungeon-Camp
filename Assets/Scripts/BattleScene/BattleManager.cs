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

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void BattleStart()
    {
        Turn = 0;
        TurnUi = GameObject.Find("Turn");

        foreach (UnitData data in SystemManager.Instance.DeckData.PlayerUnitData)
        {
            UnitSpawnManager.Instance.Spawn(data, new Vector2(), "Player");
        }
        foreach (UnitData data in SystemManager.Instance.StageData.mData.EnemyUnitData)
        {
            UnitSpawnManager.Instance.Spawn(data, new Vector2(), "Enemy");
        }
        alivePlayerUnits = PlayerUnits;
    
        TurnStart();
    }

    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        //gameObject.GetComponent<SkillManager>().uiReset();

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