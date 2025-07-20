using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireManager : MonoBehaviour
{
    public Transform unitListContainer;
    public GameObject unitCardPrefab;
    public List<UnitClassData> availableClasses; //���� ���� �� �ִ� ��� Ŭ������

    public List<UnitData> playerUnitList = new(); //�÷��̾ ������ ���� ����Ʈ
    public List<UnitData> recruitList = new(); //���������� ���� ����Ʈ

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

        for (int i = 0; i < 3; i++) //�ӽ÷� 3�� ����
        {
            UnitClassData randomClass = availableClasses[UnityEngine.Random.Range(0, availableClasses.Count)];

            UnitData newUnit = new UnitData
            {
                Name = "Hero_" + UnityEngine.Random.Range(1, 999), //�ӽ� �̸�
                Class = randomClass.className,
                UT = UnitType.Unit_Alive,
                AT = 1,
                AS = 1,
                AR = 10,
                DF = 1,
                MS = 1, //�ӽ� ���ݵ�
                HP = 10,
                CR = 10f,
                CD = 10f
            };

            recruitList.Add(newUnit);
        }

        UpdateUnitListContainerUI();
    }

    void UpdateUnitListContainerUI() //UI�� ����ī�� ǥ��/������Ʈ
    {
        foreach (Transform child in unitListContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (UnitData unit in recruitList)
        {
            GameObject unitCard = Instantiate(unitCardPrefab, unitListContainer);
            unitCard.GetComponent<UnitCard>().Setup(unit, this); //����ī�忡 �������� ���� + �̱����� �ƴ϶� �Ŵ��� ��ü�� ����
        }
    }

    public void RecruitUnit(UnitData unit) //������ �������� ��
    {
        playerUnitList.Add(unit);
        recruitList.Remove(unit);
        UpdateUnitListContainerUI();
    }
}
