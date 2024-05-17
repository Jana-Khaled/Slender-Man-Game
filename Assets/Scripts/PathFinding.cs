using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Pathfinding : MonoBehaviour
{
    public Transform seeker;
    // public Transform[] targets; // Array of targets
    public GameObject[] pages;
    public Transform parrot;
    public float speed = 5f;

    public static HashSet<Vector3> pagesTaken = new HashSet<Vector3>();


    Grid grid;
    List<Node> path;
    Transform[] pagesTransforms; // Array of Transform

    void Start()
    {
        // Initialize the array of Transforms with the same size as the array of GameObjects
    }


    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnSphereAndFollow();

            // Initialize a list to store active page transforms
            List<Transform> activePages = new List<Transform>();

            // Iterate over the pages array to find active pages
            for (int i = 0; i < pages.Length; i++)
            {
                // Check if the page is active
                if (pages[i].activeSelf)
                {
                    // Add the transform of the active page to the list
                    activePages.Add(pages[i].transform);
                }
            }

            // Convert the list of active page transforms to an array
            pagesTransforms = activePages.ToArray();

            FindPath(parrot.position, pagesTransforms);
        }

        if (path != null && path.Count > 0)
        {
            Vector3 nextWaypoint = path[0].worldPosition;
            Vector3 nextHorizontalPosition = new(nextWaypoint.x, parrot.position.y, nextWaypoint.z);
            parrot.position = Vector3.MoveTowards(parrot.position, nextHorizontalPosition, speed * Time.deltaTime);

            if (Vector3.Distance(parrot.position, nextHorizontalPosition) < 0.1f)
            {
                path.RemoveAt(0);
            }
            if (path.Count == 1)
            {
                pagesTaken.Add(path[0].worldPosition);
            }
        }
    }

    public void SpawnSphereAndFollow()
    {
        parrot.position = seeker.transform.position;
        parrot.position += Vector3.up * 1;
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

            // Check if the current node is one of the target nodes and if it hasn't been collected yet
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