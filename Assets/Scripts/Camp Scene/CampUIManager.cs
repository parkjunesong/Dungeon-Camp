using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UiPanel
{
    public string name; 
    public GameObject panelObject;
}

public class CampUIManager : MonoBehaviour
{
    public static CampUIManager Instance;

    public List<UiPanel> panels = new List<UiPanel>(); 
    private UiPanel currentOpenPanel = null; //현재 열려있는 UI패널

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void OpenUIPanel(string targetPanelName)
    {
        var panel = panels.Find(n => n.name == targetPanelName);
        if (panel == null) return;

        currentOpenPanel = panel;
        currentOpenPanel.panelObject.SetActive(true);
    }   
    public void CloseUIPanel()
    {
        currentOpenPanel.panelObject.SetActive(false);
        currentOpenPanel = null;
    }
}
