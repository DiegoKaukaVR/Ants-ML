using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    public Transform Agent;
    public Transform Objective;

    public LayerMask obstacleMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
        FixGrid();
    }

  
    private void Update()
    {
        BlacklistCoordinates.Clear();
        UpdateGrid();
        FixGrid();
    }


    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottonLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottonLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacleMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y); 
            }
        }
    }

   

    void UpdateGrid()
    {
        Vector3 worldBottonLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottonLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacleMask));
                grid[x, y].walkable = walkable;
                grid[x, y].worldPosition = worldPoint;
               
            }
        }


    }
  
    Node[] neighborsNodes = new Node[4];

    List<Vector2> BlacklistCoordinates = new List<Vector2>();
    List<Vector2> WhitelistCoordinates = new List<Vector2>();

    bool nextObstacle;

    /// <summary>
    /// RELLENA DE ROJO LOS HUECOS ADYACENTES PARA SOLUCIONAR EL PROBLEMA DE LAS LINEAS DIAGONALES
    /// </summary>
    void FixGrid()
    {
        
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (!grid[x, y].walkable)
                {
                    BlacklistCoordinates.Add(new Vector2(x,y));
                    continue;
                }

                nextObstacle = false;
                Array.Clear(neighborsNodes, 0, 4);

                if (x+1 < gridSizeX)
                {
                    neighborsNodes[0] = grid[x + 1, y];
                }
                if (x - 1 >= 0)
                {
                    neighborsNodes[1] = grid[x - 1, y];
                }
                if (y+1 < gridSizeY)
                {
                    neighborsNodes[2] = grid[x, y + 1];
                }
                if (y - 1 >= 0)
                {
                    neighborsNodes[3] = grid[x, y - 1];
                }


                for (int i = 0; i < neighborsNodes.Length; i++)
                {
                    if (neighborsNodes[i] == null)
                    {
                        continue;
                    }
                    if (!neighborsNodes[i].walkable)
                    {
                        if (!nextObstacle)
                        {
                            nextObstacle = true;
                        }
                        else
                        {
                            BlacklistCoordinates.Add(new Vector2(x, y));
                            break;
                        }
                    }

                }
            }
        }

    
        for (int i = 0; i < BlacklistCoordinates.Count; i++)
        {
            int x = Mathf.RoundToInt(BlacklistCoordinates[i].x);
            int y = Mathf.RoundToInt(BlacklistCoordinates[i].y);
            grid[x, y].walkable = false;
        }
    }
    bool nextWalkable;
    void ReFixGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (grid[x, y].walkable)
                {
                    continue;
                }

                nextWalkable = false;
                Array.Clear(neighborsNodes, 0, 4);

                if (x + 1 < gridSizeX)
                {
                    neighborsNodes[0] = grid[x + 1, y];
                }
                if (x - 1 >= 0)
                {
                    neighborsNodes[1] = grid[x - 1, y];
                }
                if (y + 1 < gridSizeY)
                {
                    neighborsNodes[2] = grid[x, y + 1];
                }
                if (y - 1 >= 0)
                {
                    neighborsNodes[3] = grid[x, y - 1];
                }


                for (int i = 0; i < neighborsNodes.Length; i++)
                {
                    if (neighborsNodes[i] == null)
                    {
                        continue;
                    }
                    if (neighborsNodes[i].walkable)
                    {
                        if (!nextWalkable)
                        {
                            nextWalkable = true;
                        }
                        else
                        {
                            WhitelistCoordinates.Add(new Vector2(x, y));
                            break;
                        }
                    }

                }
            }
        }
        
        for (int i = 0; i < WhitelistCoordinates.Count; i++)
        {
            int x = Mathf.RoundToInt(WhitelistCoordinates[i].x);
            int y = Mathf.RoundToInt(WhitelistCoordinates[i].y);
            grid[x, y].walkable = true;
        }
        for (int i = 0; i < BlacklistCoordinates.Count; i++)
        {
            int x = Mathf.RoundToInt(BlacklistCoordinates[i].x);
            int y = Mathf.RoundToInt(BlacklistCoordinates[i].y);
            grid[x, y].walkable = false;
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x / gridWorldSize.x) + 0.5f;
        float percentY = (worldPos.y / gridWorldSize.y) + 0.5f;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.FloorToInt(Mathf.Clamp((gridSizeX) * percentX, 0, gridSizeX - 1));

        int y = Mathf.FloorToInt(Mathf.Clamp((gridSizeY) * percentY, 0, gridSizeY - 1));

        return grid[x, y];

    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x== 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY > 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }

            }
        }

        return neighbours;
    }

    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

        if (grid != null)
        {
            Node agentNode = NodeFromWorldPoint(Agent.position);
            Node objetiveNode = NodeFromWorldPoint(Objective.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;

                if (agentNode == n)
                {
                    Gizmos.color = Color.blue;
                }
                if (objetiveNode == n)
                {
                    Gizmos.color = Color.green;
                }

                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-0.1f));
            }
        }
    }
}
