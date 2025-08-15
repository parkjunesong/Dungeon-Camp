using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

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
        StageData = Instantiate(StageManager.Instance.StageData);
        DeckData = Instantiate(StageManager.Instance.DeckData);   
        StageData.MapData = Instantiate(StageData.MapData, new Vector2(0, 0), Quaternion.identity);

        BattleManager.Instance.BattleStart();           
    }  
}
