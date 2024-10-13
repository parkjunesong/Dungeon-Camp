using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int money = 10000;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        LoadCampScene();
    }

    private void LoadCampScene()
    {
        SceneManager.LoadScene(1);
    }
}
