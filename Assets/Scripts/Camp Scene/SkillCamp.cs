using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCamp : MonoBehaviour
{
    public GameObject bluePanel;

    void OnMouseDown()
    {
        if (CampUIManager.isUIPanelOpen)
        {
            return;
        }

        if (bluePanel != null)
        {
            CampUIManager.OpenUIPanel(bluePanel);
        }
    }
}
