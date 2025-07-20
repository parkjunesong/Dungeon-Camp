using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType { OnlyText, OnlyBattle, BattleFirst, BattleLast };

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Object/Stage/StageData")]
public class StageData : ScriptableObject
{
    public StageType Type;
    public string Name;
    public string Description;
    public MapData mData;
    //public ScenarioData sData;
}