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
        // mouse manager ���� Ŭ���� Ÿ�� ���� �޾ƿ���
        // ���� ���� ȣ�� ���. �׽�Ʈ �ڵ�
    }
}
