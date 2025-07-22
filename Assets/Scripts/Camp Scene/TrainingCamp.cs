using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingCamp : MonoBehaviour
{
    public GameObject yellowPanel;

    void OnMouseDown()
    {
        if (CampUIManager.isUIPanelOpen)
        {
            return;
        }

        if (yellowPanel != null)
        {
            CampUIManager.OpenUIPanel(yellowPanel);
        }
    }
}
