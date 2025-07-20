using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Effect_Summon : Effect_Base
{
    /*
    public UnitData Data;
    public Position Position;
    public Effect_Summon(float value, int range, AType aimType, ATarget aimTarget, UnitData data, Position position) : base(value, range, aimType, aimTarget) 
    {
        Data = data;
        Position = position;
    }

    public override void Execute(Unit caster)
    {
        int p = 0;
        if (AimTarget == ATarget.Player)
        {
            /*
            if (Position == Position.Front) p = 0;
            else if (Position == Position.Back) p = 2;

            BattleManager.Instance.UnitSpawn(Data, new Vector2(-200 - 280 * p, 1100), "Player");
        }
        else if (AimTarget == ATarget.Enemy)
        {
            if (Position == Position.Front) p = 0;
            else if (Position == Position.Back) p = BattleManager.Instance.EnemyUnits.Count;

            BattleManager.Instance.UnitSpawn(Data, new Vector2(200 + 280 * p, 1100), "Enemy");
        }
    }
        */
}
