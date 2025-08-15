using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int money = 10000;

    private void Awake() { if (instance != null) { Destroy(gameObject); return; } instance = this; }

    public void GameStart() 
    {
        LoadCampScene();
    }

    private void LoadCampScene() // scene load manager를 만들어 기능 분리 권장
    {
        SceneManager.LoadScene(1);
    }
}
