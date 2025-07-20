using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireManager : MonoBehaviour
{
    public Transform unitListContainer;
    public GameObject unitCardPrefab;
    public List<UnitClassData> availableClasses; //현재 나올 수 있는 모든 클래스들

    public List<UnitData> playerUnitList = new(); //플레이어가 모집한 유닛 리스트
    public List<UnitData> recruitList = new(); //모집가능한 유닛 리스트

    void Start()
    {
        if (availableClasses == null || availableClasses.Count == 0)
        {
            Debug.LogError("No available classes!!");
            return;
        }
        GenerateRecruitList();
    }

    public void GenerateRecruitList()
    {
        recruitList.Clear();

        for (int i = 0; i < 3; i++) //임시로 3명 생성
        {
            UnitClassData randomClass = availableClasses[UnityEngine.Random.Range(0, availableClasses.Count)];

            UnitData newUnit = new UnitData
            {
                Name = "Hero_" + UnityEngine.Random.Range(1, 999), //임시 이름
                Class = randomClass.className,
                UT = UnitType.Unit_Alive,
                AT = 1,
                AS = 1,
                AR = 10,
                DF = 1,
                MS = 1, //임시 스텟들
                HP = 10,
                CR = 10f,
                CD = 10f
            };

            recruitList.Add(newUnit);
        }

        UpdateUnitListContainerUI();
    }

    void UpdateUnitListContainerUI() //UI에 유닛카드 표시/업데이트
    {
        foreach (Transform child in unitListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (UnitData unit in recruitList)
        {
            GameObject unitCard = Instantiate(unitCardPrefab, unitListContainer);
            unitCard.GetComponent<UnitCard>().Setup(unit, this); //유닛카드에 유닛정보 전달 + 싱글톤이 아니라서 매니저 자체도 전달
        }
    }

    public void RecruitUnit(UnitData unit) //유닛을 영입했을 시
    {
        playerUnitList.Add(unit);
        recruitList.Remove(unit);
        UpdateUnitListContainerUI();
    }
}
