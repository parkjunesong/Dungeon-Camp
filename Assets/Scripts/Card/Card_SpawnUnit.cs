using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_SpawnUnit : Card_Base
{
    private GameObject GM;
    public GameObject target;
    public override void execute()
    {
        GameObject.Find("GameManager").GetComponent<UnitSpawn>().Spawn(target, new Vector2(0, 0), "Chara");
        // mouse manager 만들어서 클릭한 타일 정보 받아오기
        // 좋지 않은 호출 방식. 테스트 코드
    }
}
