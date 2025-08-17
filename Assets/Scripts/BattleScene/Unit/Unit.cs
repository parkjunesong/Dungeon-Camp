using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Unit : MonoBehaviour
{
    public UnitData Data;
    public Unit_Ablity Ability;
    public Unit_Animation Animation;
    public Unit_Ui Ui;
    public Unit_Action ActionController;
    public Tilemap groundTilemap;

    public abstract void Init();
    public abstract void TurnStart();
    public abstract void TurnEnd();
    /*
    public void OnBuffGained(Buff_Base newBuff, int count)
    {
        Buff.OnBuffGained(newBuff, count);
        Passive.OnBuffGained(newBuff);
    }
    */
    public void OnAttacked()
    {

    }
    public void OnDamaged(float damage)
    {
        Ability.OnDamaged(this, damage);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
        Ui.UpdateShildBar(Ability.Shild, Ability.maxHP);

        if (Ability.HP <= 0)
            OnDied();
    }
    public void OnHealed(float heal)
    {
        Ability.OnHealed(this, heal);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void OnShieldGained(float shild)
    {
        Ability.OnShieldGained(this, shild);
        Ui.UpdateShildBar(Ability.Shild, Ability.maxHP);
    }
    public void OnDied()
    {
        //버프 삭제, 능력치 초기화, ui 원복
        //Passive.OnDied();
        //Animation.Dead();
        BattleManager.Instance.OnUnitDied(this);
    }
}
