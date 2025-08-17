using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClickHandler : MonoBehaviour
{
    private Unit selectedUnit;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = -Camera.main.transform.position.z;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mouseScreenPos), Vector2.zero); 

            if (hit.collider != null)
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null && unit.Ability.Team == "Player")
                {
                    if (selectedUnit == unit)
                    {
                        // 같은 유닛 클릭 시 토글
                        bool isActive = unit.Ui.ActionSelector.gameObject.activeSelf;
                        unit.Ui.ShowActionSelector(!isActive);

                        // 꺼졌다면 선택 해제
                        if (isActive)
                            selectedUnit = null;
                    }
                    else
                    {
                        // 다른 유닛 클릭 시 이전 선택 해제
                        if (selectedUnit != null)
                            selectedUnit.Ui.ShowActionSelector(false);

                        unit.Ui.ShowActionSelector(true);
                        selectedUnit = unit;
                    }
                }
            }           
        }       
    }
}