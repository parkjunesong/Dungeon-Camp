using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    public void Spawn(GameObject target, Vector2 xy, string Team)
    {
        GameObject Unit = Instantiate(target, new Vector2(xy.x, xy.y), Quaternion.identity);
        Unit.GetComponent<Unit>().Ability.Team = Team;      
    }
}
