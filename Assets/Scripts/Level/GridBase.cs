﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBase : MonoBehaviour
{
    // set up the grid
    public int maxX;
    public int maxY;
    public int maxZ;

    // offsetz relates to the world positions only
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    // the grid
    public Node[,,] grid;

    public GameObject gridFloorPrefab;

    public Vector3 startNodePosition;
    public Vector3 endNodePosition;

    public int enabledY;
    List<GameObject> YCollisions = new List<GameObject>();

    public int agents;
    LevelManager lvlManager;

    public void InitGrid(LevelInitializer.GridStats gridStats)
    {
        maxX = gridStats.maxX;
        maxY = gridStats.maxY;
        maxZ = gridStats.maxZ;

        offsetX = gridStats.offsetX;
        offsetY = gridStats.offsetY;
        offsetZ = gridStats.offsetZ;

        lvlManager = LevelManager.GetInstance();

        //CreateGrid();
      //  CreateMouseCollisions();
      //  CloseAllMouseCollisions();

        YCollisions[enabledY].SetActive(true);
    }


   

    void Start()
    {
        grid = new Node[maxX, maxY, maxZ];

        for (int x = 0; x < maxX; x++)
        {
            for(int y = 0; y < maxY; y++)
            {
                for(int z = 0; z< maxZ; z++)
                {
                    // apply offset to create the world object for each node
                    float posX = x * offsetX;
                    float posY = y * offsetY;
                    float posZ = z * offsetZ;
                    GameObject go = Instantiate(gridFloorPrefab, new Vector3(posX, posY, posZ), Quaternion.identity) as GameObject;
                    go.transform.name = x.ToString() + " " + y.ToString() + " " + z.ToString();
                    go.transform.parent = transform;

                    // create new node and update its valuyes
                    Node node = new Node();
                    node.x = x;
                    node.y = y;
                    node.z = z;
                    node.worldObject = go;

                    // place on grid
                    grid[x, y, z] = node;
                }
            }
        }
    }

    // quick way to show path
    public bool start;
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;

            //Pathfinder path = new Pathfinder();

            // testing purposes
           // grid[1, 0, 1].isWalkable = false;

            // Pass the target nodes
            Node startNode = GetNodeFromVector3(startNodePosition);
            Node end = GetNodeFromVector3(endNodePosition);

            //path.startPosition = startNode;
            //path.endPosition = end;

            // find the path
          //  List<Node> p = path.FindPath();

            // disbale the world object for each node we agoing to pass from
            startNode.worldObject.SetActive(false);
          
            for(int i =0; i< agents; i++)
            {
                PathfinderMaster.GetInstance().RequestPathfind(startNode, end, ShowPath);
            }
        }
    }

    public void ShowPath(List<Node> path)
    {
        foreach(Node n in path)
        {
            n.worldObject.SetActive(false);
        }
    }

    public Node GetNode(int x, int y, int z)
    {
        Node returnVal = null;

        if (x < maxX && x >= 0 &&
            y >= 0 && y < maxY &&
            z >= 0 && z < maxZ)
        {
            returnVal = grid[x, y, z];
        }

        return returnVal;
    }

    public Node GetNodeFromVector3(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        int z = Mathf.RoundToInt(pos.z);

        Node result = GetNode(x, y, z);
        return result;
    }


    // singleton
    public static GridBase instance;
    public static GridBase GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }
}
