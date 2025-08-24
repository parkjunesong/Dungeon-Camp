using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : MonoBehaviour
{
    //public GameObject greenPanel;
    public CampUIManager.Panel greenPanel;

    void OnMouseDown()
    {
        if (CampUIManager.Instance.isUIPanelOpen(greenPanel.name))
        {
            return;
        }

        if (greenPanel != null)
        {
            CampUIManager.Instance.OpenUIPanel(greenPanel.name);
        }
    }
}
