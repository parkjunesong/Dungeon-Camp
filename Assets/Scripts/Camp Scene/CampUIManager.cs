using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampUIManager : MonoBehaviour
{
    public static CampUIManager Instance;

    [System.Serializable]
    public class Panel
    {
        public string name; //UI패널 이름
        public GameObject panelObject; //UI패널
    }

    public List<Panel> panels = new List<Panel>(); //존재하는 UI패널들 리스트
    private Panel currentOpenPanel = null; //현재 열려있는 UI패널

    private string panelName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool isUIPanelOpen(string targetPanelName) //지금은 열려고하는 UI패널과 똑같은 패널이 열려있을 때만 True
    {
        return currentOpenPanel != null && currentOpenPanel.name == targetPanelName;
    }

    public void OpenUIPanel(string targetPanelName)
    {
        if (currentOpenPanel != null)
        {
            return;
            //currentOpenPanel.panelObject.SetActive(false);
        }

        panelName = targetPanelName;
        currentOpenPanel = panels.Find(MatchPanelByName); //열려고하는 패널이 존재하는 패널인지 확인

        if (currentOpenPanel != null) //존재하면 열기
        {
            currentOpenPanel.panelObject.SetActive(true);
        }
    }

    private bool MatchPanelByName(Panel panel) //패널이 존재하는지 이름으로 확인
    {
        return panel.name == panelName;
    }

    public void CloseUIPanel()
    {
        if (currentOpenPanel != null)
        {
            currentOpenPanel.panelObject.SetActive(false);
            currentOpenPanel = null;
        }
    }
}
