using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Unit_Action : MonoBehaviour
{
    public void Initialize()
    {

    }

    public void Move()
    {
        Unit unit = transform.GetComponent<Unit>();
        Effect_Base effect = new Effect_Move(0, unit.Ability.MS, 0, EffectTarget.Ground, EffectType.SetTarget);

        EffectPreviewManager.Instance.StartTargeting(unit, effect, unit.groundTilemap);
    }  
    public void NormalAttack()
    {

    }
    public void Skill()
    {

    }   
}
