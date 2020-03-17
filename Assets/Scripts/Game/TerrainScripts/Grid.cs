using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : Singleton<Grid>
{

    public Vector2Int m_GridDimensions;

    public List<CombatNode> m_GridPathToGoal;

    public GameObject m_PrefabNode;
    public CombatNode[,] m_GridPathArray;
    public List<CombatNode> m_GridPathList;
    
    // Use this for initialization
    void Start ()
    {
        GameManager.Instance.m_Grid = this;
    }


    public void Convert1DArrayto2D(List<CombatNode> aNodeGroup, Vector2Int grid)
    {

        m_GridDimensions = grid;
        m_GridPathArray = new CombatNode[m_GridDimensions.x, m_GridDimensions.y];
        
        for (int i = 0; i < m_GridDimensions.x * m_GridDimensions.y; i++)
        {

           m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y] = aNodeGroup[i];
           m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y].m_Grid = this;
          
        }
        m_GridPathList = aNodeGroup;
    }

    public CombatNode GetNode(Vector2Int grid)
    {
        if (m_GridPathArray != null)
        {
            return m_GridPathArray[grid.x, grid.y];
        }
        else
        {
            return null;
        }
        
    }
    
    public CombatNode GetNode(int gridX, int gridY)
    {
        if (m_GridPathArray != null)
        {
            return m_GridPathArray[gridX, gridY];
        }
        else
        {
            return null;
        }
        
    }

    public void SetHeuristicToZero()
    {
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                m_GridPathArray[x, y].m_Heuristic = 0;
                m_GridPathArray[x, y].m_IsGoal = false;
            }
        }
    }
    

    public void RemoveWalkableArea()
    {
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                m_GridPathArray[x, y].RemoveWalkableArea();
            }
        }
    }

    protected static readonly Vector2Int[] _directions =
    {
        new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1)
    };

    public CombatNode CheckNeighborsForLowestNumber(Vector2Int grid)
    {
        float TempHeuristic = 100;
        CombatNode TempNode = null;


        foreach (Vector2Int direction in _directions)
        {
            if ((grid.x + direction.x) < m_GridDimensions.x &&  (grid.y + direction.y)  < m_GridDimensions.y 
                && grid.x + direction.x > -1 && (grid.y + direction.y) > -1)
            {
                CombatNode neighbour = m_GridPathArray[grid.x + direction.x, grid.y + direction.y];
                if (neighbour == null)
                {
                    Debug.Log("neighbour literally doesnt exist");
                    continue;
                }

                if (neighbour.m_IsWalkable == false)
                {
                    Debug.Log("Walkable was false");
                    continue;
                }
                
                if (neighbour.m_IsCovered == true)
                {
                    Debug.Log("Node Was covered by someone else");
                    continue;
                }

                if (neighbour.m_Heuristic == -1)
                {
                    Debug.Log("Neighbour heuristic is  -1");
                    continue;
                }

                if (neighbour.m_Heuristic < TempHeuristic)
                {
                    TempHeuristic = neighbour.m_Heuristic;
                    TempNode = neighbour;
                }
            }
        }

        if(TempNode == null)
        {
            Debug.Log("Check Neightbor is null");
            Debug.Break();
        }
        return TempNode;
    }

  

    public bool CheckingGridDimensionBoundrys(Vector2Int aPositionInGrid)
    {
        if (aPositionInGrid.x < m_GridDimensions.x && aPositionInGrid.x >= 0 &&
            aPositionInGrid.y < m_GridDimensions.y && aPositionInGrid.y >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    public List<CombatNode> GetTheLowestH(Vector2Int grid, int aMovement)
    {


        m_GridPathToGoal.Add(CheckNeighborsForLowestNumber(grid));

        for (int i = aMovement; i > 0; i-- )
         {
             if (m_GridPathToGoal[m_GridPathToGoal.Count - 1] == null)
             {
                 Debug.Break();
                 Debug.Log("IT WAS IMPOSSIBLE FOR THIS CHARACTER TO REACH THE POSITION " + grid);
                 break;
             }

             if (m_GridPathToGoal[m_GridPathToGoal.Count - 1].m_IsGoal == true)
            {
                break;
            }
            
            m_GridPathToGoal.Add(CheckNeighborsForLowestNumber(m_GridPathToGoal[m_GridPathToGoal.Count - 1].m_PositionInGrid));
        }
        
        return m_GridPathToGoal;

    }
    

    public void SetAttackingTileInGrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_AttackingPlane.SetActive(true);
    }

    public void DeselectAttackingTileingrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_AttackingPlane.SetActive(false);
    }

}
