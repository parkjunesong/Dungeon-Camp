using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamSelect : MonoBehaviour
{
    public GameObject[] Member = new GameObject[4];
    public GameObject[] MemberUi = new GameObject[4];
    GameObject SelectedUnit;
    int num; // ���� ���õ� �ڸ� ��ȣ
    public GameObject UnitList;
    public GameObject JoinButton;
    public Image UnitImage;
    public Text Unitname;
    
    public void ChoseUnit(int i) // i�� �ڸ��� ���� ���� ����
    {
        num = i;
        UnitList.SetActive(true);
        if (Member[i] == null) // ���� �� ��
        {
            JoinButton.SetActive(false);
        }
        else // �̹� ���� ������ ������ ��
        {
            JoinButton.SetActive(true);
            UnitImage.sprite = Member[i].GetComponent<Unit>().Data.Standing;
            Unitname.text = Member[i].GetComponent<Unit>().Data.Name;
        }
    }
    public void getInfo(GameObject SelectUnit)
    {
        JoinButton.SetActive(true);
        UnitImage.sprite = SelectUnit.GetComponent<Unit>().Data.Standing;
        Unitname.text = SelectUnit.GetComponent<Unit>().Data.Name;
        SelectedUnit = SelectUnit;
    }
    public void getTeam()
    {
        Member[num] = SelectedUnit;
        MemberUi[num].transform.GetChild(0).GetComponent<Image>().sprite = Member[num].GetComponent<Unit>().Data.Standing;
        UnitList.SetActive(false);
        num = -1;
        SelectedUnit = null;
    }
}
