using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public StageData StageData;
    public DeckData DeckData;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }

        Instance = this;
        DontDestroyOnLoad(this); // �� ��ȯ �� ����
    }

    public void SetStage(StageData stage, DeckData deck)
    {
        StageData = stage;
        DeckData = deck;
    }
}