using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public List<GameObject> PanelList = new();

    void Start()
    {
        foreach (GameObject ob in PanelList)
            ob.SetActive(false);
    }

    public void OpenCampUIPanel(string targetName)
    {
        CampUIManager.Instance.OpenUIPanel(targetName);
    }
}
