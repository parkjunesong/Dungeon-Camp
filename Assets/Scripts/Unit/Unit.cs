using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class Unit : MonoBehaviour
{
    public UnitData Data;
    public UnitAblity Ability;
    public UnitUi Ui;

    void Awake()
    {       
        Ability = new UnitAblity(Data);                     
        Ui = transform.GetChild(0).GetComponent<UnitUi>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Data.Standing;
    }
    public void Attack(int i)
    {
        //SkillManager.skill.UseSkill(Skills[i], Ability); 
    }
    public void Damaged(float damage, int ignore)
    {
        Ability.Damaged(damage, ignore);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void TurnStart()
    {
    }
    public void TurnEnd()
    {
    }
}
