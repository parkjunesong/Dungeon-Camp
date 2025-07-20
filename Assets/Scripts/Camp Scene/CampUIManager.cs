using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampUIManager : MonoBehaviour
{
    public GameObject campFirePanel;

    public void CloseCampFirePanel()
    {
        if (campFirePanel != null)
        {
            campFirePanel.SetActive(false);
        }
    }
}
