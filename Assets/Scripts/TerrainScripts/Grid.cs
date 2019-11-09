﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Vector2Int m_GridDimensions;

    public List<CombatNode> m_GridPathToGoal;

    public GameObject m_PrefabNode;
    public CombatNode[,] m_GridPathArray;
    public List<CombatNode> m_GridPathList;
    public CombatNode[] m_Test;
    public List<CombatNode> m_OpenNodeList;
    public Material m_SelectedMaterial;

    public int PlayerX;
    public int PlayerY;

    public int m_Movement;

    public bool m_WalkingHasStarted;

    public bool m_GotPathNodes;


    // Use this for initialization
    void Start ()
    {
        GameManager.Instance.m_Grid = this;

        PlayerX = 10;
        PlayerY = 10;


        m_WalkingHasStarted = false;

        //SetGoal(new Vector2Int(9,2));

        m_Movement = 4;
        m_GotPathNodes = false;
        
        //FindPointInGrid(new Vector2Int(1, 1));
        //CalculateHeuristic(new Vector2Int(2, 2));
    }

    public void StartGridCreation()
    {
        CreateGrid(m_GridDimensions);
    }

    private bool IsAllNodesHeuristicCalculated()
    {
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                if (m_GridPathArray[x, y].m_HeuristicCalculated == false && 
                    m_GridPathArray[x,y].m_CombatsNodeType == CombatNode.CombatNodeTypes.Normal)
                {
                    return false;
                }
            }
        }

        return true;
    }



    public void Convert1DArrayto2D(List<CombatNode> aNodeGroup, Vector2Int grid)
    {

        m_GridDimensions = grid;
        m_GridPathArray = new CombatNode[m_GridDimensions.x, m_GridDimensions.y];
        
        for (int i = 0; i < m_GridDimensions.x * m_GridDimensions.y; i++)
        {

           m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y] = aNodeGroup[i];
           m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y].m_Grid = this;
            //m_GridPathArray[aNodeGroup[i].m_PositionInGrid.x, aNodeGroup[i].m_PositionInGrid.y].SetPropState();




        }
        m_GridPathList = aNodeGroup;
    }

    public void CreateGrid(Vector2Int grid)
    {
        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {
                int RandomNumber = Random.Range(0, 2);
                m_GridPathArray[x,y] =  Instantiate<CombatNode>(m_PrefabNode.GetComponent<CombatNode>(), transform);

     

                    m_GridPathArray[x, y].transform.position = new Vector3(2 * x, 0.5f, 2 * y );
       

         
                m_GridPathArray[x,y].m_PositionInGrid = new Vector2Int(x, y);


            }
        }

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

    public void SetWalkableArea()
    { 
        for (int x = 0; x < m_GridDimensions.x; x++)
        {
            for (int y = 0; y < m_GridDimensions.y; y++)
            {
                m_GridPathArray[x, y].CreateWalkableArea();
            }
        }
      
        GameManager.Instance.m_BattleCamera.m_MovementHasBeenCalculated = true;
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
                && grid.x + direction.x > 0 && (grid.y + direction.y) > 0)
            {
                CombatNode neighbour = m_GridPathArray[grid.x + direction.x, grid.y + direction.y];
                if (neighbour == null)
                {
                    continue;
                }

                if (neighbour.m_IsWalkable == false)
                {
                    continue;
                }

                if (neighbour.m_Heuristic < TempHeuristic)
                {
                    TempHeuristic = neighbour.m_Heuristic;
                    TempNode = neighbour;
                }
            }
        }

        return TempNode;
    }

  



    public List<CombatNode> GetTheLowestH(Vector2Int grid)
    {
       Debug.Log( m_GridPathList[0].m_Heuristic);

        m_GridPathToGoal.Add(CheckNeighborsForLowestNumber(grid));

        for (int i = m_Movement; i > 0; i-- )
         {
            if (m_GridPathToGoal[m_GridPathToGoal.Count - 1].m_IsGoal == true)
            {
                break;
            }
            
            m_GridPathToGoal.Add(CheckNeighborsForLowestNumber(m_GridPathToGoal[m_GridPathToGoal.Count - 1].m_PositionInGrid));
        }
      
       m_GotPathNodes = true;
        return m_GridPathToGoal;

    }


    public void SetAttackingTile(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y + 1].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
        m_GridPathArray[grid.x, grid.y - 1 ].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
        m_GridPathArray[grid.x + 1, grid.y].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
        m_GridPathArray[grid.x - 1, grid.y].m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);
    }

    public void SetSelectoringrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_IsSelector = true;
    }

    public void SetAttackingTileInGrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_AttackingPlane.SetActive(true);
    }

    public void DeselectAttackingTileingrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_AttackingPlane.SetActive(false);
    }

    public void DeSelectSelectoringrid(Vector2Int grid)
    {
        m_GridPathArray[grid.x, grid.y].m_IsSelector = false;
    }



}
