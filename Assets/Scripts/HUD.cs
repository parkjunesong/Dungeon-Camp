using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Money, UnitExp, UnitHealth, UnitLevel, UnitPrice }
    public InfoType type;

    Text text;
    Slider slider;

    private void Awake()
    {
        text = GetComponent<Text>();
        slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Money:
                //text.text = string.Format("GOLD: {0:F0}", GameManager.instance.money);
                break;

            case InfoType.UnitExp:
                break;

            case InfoType.UnitHealth:
                break;

            case InfoType.UnitLevel:
                break;

            case InfoType.UnitPrice:
                break;
        }        
    }

}
