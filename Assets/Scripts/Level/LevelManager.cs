using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<ObjectsPerFloor> lvlObjects = new List<ObjectsPerFloor>();

    public static LevelManager instance;
    public static LevelManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
}

[System.Serializable]
public class ObjectsPerFloor
{
    public int floorIndex;
    public List<GameObject> obj = new List<GameObject>();
}
