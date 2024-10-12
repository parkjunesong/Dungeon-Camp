using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    private Slider HpBar;
    void Start()
    {
        HpBar = transform.GetChild(0).GetComponent<Slider>();
    }

    public void UpdateHPBar(int inGame, int inData)
    {
        HpBar.value = (float)inGame / inData;
    }
}
