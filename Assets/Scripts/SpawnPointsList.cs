using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsList : MonoBehaviour
{
    public static List<Transform> _spawnpointList;

    void Start()
    {
        _spawnpointList = new List<Transform>();
        _spawnpointList.AddRange(GetComponentsInChildren<Transform>());
        _spawnpointList.RemoveAt(0);
    }

    public static Vector3 GetRandomSpawnPoint()
    {
        return _spawnpointList[Random.Range(0, _spawnpointList.Count - 1)].localPosition;
    }
   
}
