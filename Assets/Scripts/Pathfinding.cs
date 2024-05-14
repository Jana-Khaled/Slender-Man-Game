using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Pathfinding : MonoBehaviour
{
    public Transform seeker;
    public GameObject[] pages;
    public Transform ghost;
    public float speed = 5f;

    public static HashSet<Vector3> pagesTaken = new HashSet<Vector3>();

    public static int ghostCount = 420;

    public Transform[] ghosts;
    public GameObject ghosts_number_text;
    Grid grid;
    List<Node> path;
    Transform[] pagesTransforms;
    bool isGhostFlying = false;
    private GameObject gameLogic;


    void Start()
    {
        gameLogic = GameObject.FindWithTag("GameLogic");
    }


    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        if (path != null && path.Count > 0)
        {
            isGhostFlying = true;
            Vector3 nextWaypoint = path[0].worldPosition;
            Vector3 nextHorizontalPosition = new(nextWaypoint.x, ghost.position.y, nextWaypoint.z);
            ghost.position = Vector3.MoveTowards(ghost.position, nextHorizontalPosition, speed * Time.deltaTime);

            if (Vector3.Distance(ghost.position, nextHorizontalPosition) < 0.1f)
            {
                path.RemoveAt(0);
            }
            if (path.Count == 1)
            {
                pagesTaken.Add(path[0].worldPosition);
            }
        }
        else
        {
            isGhostFlying = false;
        }

        if (Input.GetKeyDown(KeyCode.E) && !isGhostFlying && ghostCount > 0)
        {
            gameLogic.GetComponent<GameLogic>().ghostCounts += 1;
            ghostCount--;
            SpawnGhostAndFollow();

            List<Transform> activePages = new List<Transform>();

            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i].activeSelf)
                {
                    activePages.Add(pages[i].transform);
                }
            }

            pagesTransforms = activePages.ToArray();

            FindPath(ghost.position, pagesTransforms);
        }
    }

    void SpawnGhostAndFollow()
    {
        ghost.position = seeker.transform.position;
        ghost.position += Vector3.up * 2;
    }

    void FindPath(Vector3 startPos, Transform[] pagesTransforms)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node[] targetNodes = new Node[pagesTransforms.Length];

        for (int i = 0; i < pagesTransforms.Length; i++)
        {
            targetNodes[i] = grid.NodeFromWorldPoint(pagesTransforms[i].position);
        }

        Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (Array.Exists(targetNodes, node => node == currentNode) && !pagesTaken.Contains(currentNode.worldPosition))
            {
                RetracePath(startNode, currentNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    Debug.Log("not walkable");
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = targetNodes.Min(node => GetDistance(neighbour, node));
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void FindPathBFS(Vector3 startPos, Transform[] pagesTransforms)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node[] targetNodes = new Node[pagesTransforms.Length];
        HashSet<Node>[] closedSets = new HashSet<Node>[pagesTransforms.Length];

        for (int i = 0; i < pagesTransforms.Length; i++)
        {
            targetNodes[i] = grid.NodeFromWorldPoint(pagesTransforms[i].position);
            closedSets[i] = new HashSet<Node>();
        }

        Queue<Node> openSet = new Queue<Node>();
        openSet.Enqueue(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.Dequeue();

            for (int i = 0; i < targetNodes.Length; i++)
            {
                if (targetNodes[i] == currentNode && !pagesTaken.Contains(currentNode.worldPosition))
                {
                    RetracePath(startNode, currentNode);
                    pagesTaken.Add(currentNode.worldPosition);
                    break;
                }
            }

            if (pagesTaken.Count == pagesTransforms.Length)
                break;

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSets.Any(set => set.Contains(neighbour)))
                {
                    continue;
                }

                openSet.Enqueue(neighbour);

                foreach (var set in closedSets)
                {
                    set.Add(neighbour);
                }
            }
        }
    }




    void RetracePath(Node startNode, Node endNode)
    {
        path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
