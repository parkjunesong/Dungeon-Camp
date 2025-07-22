using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : MonoBehaviour
{
    public GameObject greenPanel;

    void OnMouseDown()
    {
        if (CampUIManager.isUIPanelOpen)
        {
            return;
        }

        if (greenPanel != null)
        {
            CampUIManager.OpenUIPanel(greenPanel);
        }
    }
}
