using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    //public GameObject campFirePanel;
    public CampUIManager.Panel campFirePanel;

    void OnMouseDown()
    {
        if (CampUIManager.Instance.isUIPanelOpen(campFirePanel.name))
        {
            return;
        }

        if (campFirePanel != null)
        {
            CampUIManager.Instance.OpenUIPanel(campFirePanel.name);
        }
    }
}
