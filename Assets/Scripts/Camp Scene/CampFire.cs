using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public GameObject campFirePanel;

    void OnMouseDown()
    {
        if (campFirePanel != null)
        {
            campFirePanel.SetActive(true);
        }
    }
}
