using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectUi : MonoBehaviour
{
    public GameObject Unit;

    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Unit.GetComponent<Unit>().Data.Face;
    }
    public void Select()
    {
        GameObject.Find("GameManager").GetComponent<TeamSelect>().getInfo(Unit);
    }
}
