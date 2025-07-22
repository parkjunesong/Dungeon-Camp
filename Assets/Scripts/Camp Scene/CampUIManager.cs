using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampUIManager : MonoBehaviour
{
    private static GameObject currentOpenPanel = null;

    public static bool isUIPanelOpen
    {
        get
        {
            return currentOpenPanel != null;
        }
    }

    public static void OpenUIPanel(GameObject panel)
    {
        if (currentOpenPanel != null)
        {
            return;
        }

        currentOpenPanel = panel;
        currentOpenPanel.SetActive(true);
    }

    public static void CloseUIPanel()
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.SetActive(false);
            currentOpenPanel = null;
        }
    }

    //ºÒÇÊ¿ä
    public void OnCloseButtonClicked()
    {
        CloseUIPanel();
    }
}
