using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData Data;
    public UnitAblity Ability;
    public UnitUi Ui;
    public int GroupNo; // Enemy 사망시 GroupNo 갱신 필요

    void Awake()
    {       
        Ability = new UnitAblity(Data);                     
        Ui = transform.GetChild(1).GetComponent<UnitUi>();
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
