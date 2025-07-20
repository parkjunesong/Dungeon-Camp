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
    //bgm, image 등 구현 바람, 장애물 데이터, 맵 크기 등... 타일맵 구현할때 연동해서 에디터 하나 만들어야될듯
    //스폰지점 구현 필요
    public Map Map;
    public List<UnitData> EnemyUnitData = new();   
}
