using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Layer
{
    public List<Tilemap> Tilemap = new();
    //public List<GameObject> Object;
    //public List<GameObject> Deco;
}

public class MapData : MonoBehaviour
{
    public List<Layer> Layers = new();
    public List<Vector3Int> PlayerSpawnPoint = new();
    public List<Vector3Int> EnemySpawnPoint = new();

    void Awake()
    {
        LoadMapData();
    }

    void LoadMapData()
    {
        foreach (Transform layerTransform in transform)
        {
            Layer layer = new ();

            foreach (Transform child in layerTransform)
            {
                if (child.name == "Tilemap")
                {
                    Tilemap[] tilemaps = child.GetComponentsInChildren<Tilemap>();
                    foreach (Tilemap tm in tilemaps)
                    {
                        layer.Tilemap.Add(tm);
                    }
                }
            }
            Layers.Add(layer);
        }
    }
}
