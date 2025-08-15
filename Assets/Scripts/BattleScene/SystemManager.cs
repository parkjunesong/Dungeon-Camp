using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager Instance { get; private set; }

    public StageData StageData;
    public DeckData DeckData;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }

        Instance = this;
    }

    void Start()
    {
        //StageData = Instantiate(StageManager.Instance.StageData);
        //DeckData = Instantiate(StageManager.Instance.DeckData);   
        BattleManager.Instance.BattleStart();           
    }  
}
