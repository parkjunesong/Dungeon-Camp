using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireManager : MonoBehaviour
{
    public Transform unitListContainer;
    public GameObject unitCardPrefab;

    public List<UnitData> availableUnits; //현재 나올 수 있는 모든 유닛들
    public List<UnitData> recruitList = new(); //모집가능한 유닛 리스트
    public List<UnitData> playerUnitList = new(); //플레이어가 모집한 유닛 리스트

    void Start()
    {
        if (availableUnits == null || availableUnits.Count == 0)
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
            UnitData newUnit = availableUnits[UnityEngine.Random.Range(0, availableUnits.Count)];
            recruitList.Add(newUnit);
        }

        UpdateUnitListContainerUI();
    }

    void UpdateUnitListContainerUI() //UI에 유닛카드 표시/업데이트 | 별도 스크립트로 빼도 좋을듯
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
