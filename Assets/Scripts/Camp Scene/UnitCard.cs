using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour
{
    public Text nameText;
    public Text classText;
    private UnitData unit;
    private CampFireManager manager;

    public void Setup(UnitData unitData, CampFireManager campFireManager)
    {
        unit = unitData;
        manager = campFireManager;

        nameText.text = unit.Name;
        classText.text = unit.Class.ToString();
    }

    public void OnRecruitButtonClicked()
    {
        manager.RecruitUnit(unit);
    }
}
