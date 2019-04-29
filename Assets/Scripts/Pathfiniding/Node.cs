using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node 
{
    // Nodes position in the grid
    public int x;
    public int y;
    public int z;

    // node cost (pathfinding)
    public float hCost;
    public float gCost;

    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node parentNode;
    public bool isWalkable = true;

    public GameObject worldObject;


    // node types
    public NodeType nodeType;
    public enum NodeType
    {
        ground,
        air
    }
}
