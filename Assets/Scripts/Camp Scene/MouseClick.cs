using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    void OnMouseDown()
    {
        GameObject.Find("UIManager").GetComponent<UiManager>().OpenCampUIPanel(name);
    }
}
