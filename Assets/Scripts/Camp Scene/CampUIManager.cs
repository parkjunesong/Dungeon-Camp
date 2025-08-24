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
        public string name; //UI�г� �̸�
        public GameObject panelObject; //UI�г�
    }

    public List<Panel> panels = new List<Panel>(); //�����ϴ� UI�гε� ����Ʈ
    private Panel currentOpenPanel = null; //���� �����ִ� UI�г�

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

    public bool isUIPanelOpen(string targetPanelName) //������ �������ϴ� UI�гΰ� �Ȱ��� �г��� �������� ���� True
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
        currentOpenPanel = panels.Find(MatchPanelByName); //�������ϴ� �г��� �����ϴ� �г����� Ȯ��

        if (currentOpenPanel != null) //�����ϸ� ����
        {
            currentOpenPanel.panelObject.SetActive(true);
        }
    }

    private bool MatchPanelByName(Panel panel) //�г��� �����ϴ��� �̸����� Ȯ��
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
