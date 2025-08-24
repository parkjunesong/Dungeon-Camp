using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireManager : MonoBehaviour
{
    public Transform unitListContainer;
    public GameObject unitCardPrefab;

    public List<UnitData> availableUnits; //���� ���� �� �ִ� ��� ���ֵ�
    public List<UnitData> recruitList = new(); //���������� ���� ����Ʈ
    public List<UnitData> playerUnitList = new(); //�÷��̾ ������ ���� ����Ʈ

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

        for (int i = 0; i < 3; i++) //�ӽ÷� 3�� ����
        {
            UnitData newUnit = availableUnits[UnityEngine.Random.Range(0, availableUnits.Count)];
            recruitList.Add(newUnit);
        }

        UpdateUnitListContainerUI();
    }

    void UpdateUnitListContainerUI() //UI�� ����ī�� ǥ��/������Ʈ | ���� ��ũ��Ʈ�� ���� ������
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
