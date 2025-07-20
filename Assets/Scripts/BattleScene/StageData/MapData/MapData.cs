using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int Size_X, Size_Y;
    public Map(int sizeX, int sizeY)
    {
        Size_X = sizeX;
        Size_Y = sizeY;
    }
}

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Object/Stage/MapData")]
public class MapData : ScriptableObject
{
    //bgm, image �� ���� �ٶ�, ��ֹ� ������, �� ũ�� ��... Ÿ�ϸ� �����Ҷ� �����ؼ� ������ �ϳ� �����ߵɵ�
    //�������� ���� �ʿ�
    public Map Map;
    public List<UnitData> EnemyUnitData = new();   
}
