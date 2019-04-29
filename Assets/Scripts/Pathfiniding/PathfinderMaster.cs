using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PathfinderMaster : MonoBehaviour
{
    private static PathfinderMaster instance;

    private void Awake()
    {
        instance = this;
    }

    public static PathfinderMaster GetInstance()
    {
        return instance;
    }

    public int MaxJobs = 3;

    public delegate void PathfindingJobComplete(List<Node> path);

    private List<Pathfinder> currentJobs;
    private List<Pathfinder> todoJobs;

    private void Start()
    {
        currentJobs = new List<Pathfinder>();
        todoJobs = new List<Pathfinder>();
    }

    private void Update()
    {
        int i = 0;

        while(i < currentJobs.Count)
        {
            if(currentJobs[i].jobDone)
            {
                currentJobs[i].NotifyComplete();
                currentJobs.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }

        if(todoJobs.Count>0 && currentJobs.Count < MaxJobs)
        {
            Pathfinder job = todoJobs[0];
            todoJobs.RemoveAt(0);
            currentJobs.Add(job);

            // start new thread
            Thread jobThread = new Thread(job.FindPath);
            jobThread.Start(); //automatic garbage collection will deal with closing threads
        }
    }

    public void RequestPathfind(Node start, Node target, PathfindingJobComplete completeCallback)
    {
        Pathfinder newJob = new Pathfinder(start, target, completeCallback);
        todoJobs.Add(newJob);
    }
}
