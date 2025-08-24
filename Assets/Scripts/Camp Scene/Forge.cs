using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    //public GameObject redPanel;
    public CampUIManager.Panel redPanel;

    void OnMouseDown()
    {
        if (CampUIManager.Instance.isUIPanelOpen(redPanel.name))
        {
            return;
        }

        if (redPanel != null)
        {
            CampUIManager.Instance.OpenUIPanel(redPanel.name);
        }
    }
}
