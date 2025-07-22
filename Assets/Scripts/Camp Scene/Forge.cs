using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour
{
    public GameObject redPanel;

    void OnMouseDown()
    {
        if (CampUIManager.isUIPanelOpen)
        {
            return;
        }

        if (redPanel != null)
        {
            CampUIManager.OpenUIPanel(redPanel);
        }
    }
}
