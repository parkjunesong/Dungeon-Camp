using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Animation
{ 
    public Unit_Animation(Unit unit)
    {
        Default(unit);
    }

    public void Default(Unit unit)
    {
        unit.GetComponent<SpriteRenderer>().sprite = unit.Data.Standing;
    }
    public void Dead(Unit unit)
    {
        //unit.GetComponent<SpriteRenderer>().sprite = unit.Data.Dead;
    }
}
