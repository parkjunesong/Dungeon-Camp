using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCamp : MonoBehaviour
{
    //public GameObject yellowPanel;
    public CampUIManager.Panel yellowPanel;

    void OnMouseDown()
    {
        if (CampUIManager.Instance.isUIPanelOpen(yellowPanel.name))
        {
            return;
        }

        if (yellowPanel != null)
        {
            CampUIManager.Instance.OpenUIPanel(yellowPanel.name);
        }
    }
}
