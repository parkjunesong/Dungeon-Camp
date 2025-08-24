using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCamp : MonoBehaviour
{
    //public GameObject bluePanel;
    public CampUIManager.Panel bluePanel;

    void OnMouseDown()
    {
        if (CampUIManager.Instance.isUIPanelOpen(bluePanel.name))
        {
            return;
        }

        if (bluePanel != null)
        {
            CampUIManager.Instance.OpenUIPanel(bluePanel.name);
        }
    }
}
