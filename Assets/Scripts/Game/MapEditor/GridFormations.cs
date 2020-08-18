﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[ExecuteInEditMode]
[System.Serializable]
public class GridFormations : MonoBehaviour
{
    [TextArea]
    public string m_ArenaName;
    [TextArea]
    public string m_MissionTag;
    [TextArea]
    public string m_Description;
    
    public Vector2Int m_GridDimensions;

    public GameObject m_PrefabNode;
    public CombatNode[,] m_GridPathArray;

    public List<CombatNode> m_ListToConvert;
    public Material m_SelectedMaterial;
    public Grid m_Grid;

    public int PlayerX;
    public int PlayerY;

    public bool m_GotPathNodes;

    public GameObject Node;
    public GameObject Enemy;
    
    public List<Creatures> m_EnemysInGrid;
    public List<Relic> m_RelicsInGrid;

    public DialogueTrigger m_StartDialogueTrigger;

    public Material m_WallMaterial;
    public Material m_TopMaterial;
    
    public void Start()
    {
        m_Grid = Grid.Instance;
        //m_EditorCamera = GameManager.Instance.m_EditorCamera;

        m_StartDialogueTrigger = GetComponent<DialogueTrigger>();
    }
    
    public void CreateGrid(Vector2Int grid)
    {

        m_GridPathArray = new CombatNode[m_GridDimensions.x, m_GridDimensions.y];

        for (int x = 0; x < grid.x; x++)
        {
            for (int y = 0; y < grid.y; y++)
            {


                GameObject tempCombatnode = Instantiate(m_PrefabNode); //PrefabUtility.InstantiatePrefab(m_PrefabNode) as GameObject;

                tempCombatnode.gameObject.transform.parent = Node.transform;
                
                m_GridPathArray[x, y] = tempCombatnode.GetComponent<CombatNode>();

                m_GridPathArray[x, y].gameObject.name  = x + " " + y;
                
                m_ListToConvert.Add(m_GridPathArray[x, y]);
              //  m_ListToConvert[m_ListToConvert.Count - 1].m_PositionInList = m_ListToConvert.Count - 1;
                m_GridPathArray[x, y].NodesGridFormation = this;

                m_GridPathArray[x, y].transform.position = new Vector3(2 * x, 0.5f, 2 * y);



                m_GridPathArray[x, y].m_PositionInGrid = new Vector2Int(x, y);

                m_GridPathArray[x, y].m_Grid = m_Grid;
            }
        }
    }

    public void RespawnCube(Vector2Int Postion, int aPositionInList)
    {
     //  GameObject tempCombatnode = PrefabUtility.InstantiatePrefab(m_PrefabNode) as GameObject;

     //  tempCombatnode.gameObject.transform.parent = Node.transform;
     //  tempCombatnode.GetComponent<CombatNode>().SetCombatNode(m_ListToConvert[aPositionInList]);
     //  PrefabUtility.UnpackPrefabInstance(m_ListToConvert[aPositionInList].gameObject,PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
     //  DestroyImmediate(m_ListToConvert[aPositionInList].gameObject);
     //  
     //  m_ListToConvert[aPositionInList] = tempCombatnode.GetComponent<CombatNode>();

     //  m_ListToConvert[aPositionInList].gameObject.name  = Postion.x + " " + Postion.y;
     //          
     //  m_ListToConvert[aPositionInList] = tempCombatnode.GetComponent<CombatNode>();
     //  m_ListToConvert[aPositionInList].NodesGridFormation = this;

     //  m_ListToConvert[aPositionInList].transform.position = new Vector3(2 * Postion.x, 0.5f, 2 * Postion.y);



     //  m_ListToConvert[aPositionInList].m_PositionInGrid = new Vector2Int(Postion.x, Postion.y);

     //  m_ListToConvert[aPositionInList].m_Grid = m_Grid;
    }


    public void InitializeEnemys()
    {
        foreach (Creatures AEnemy in m_EnemysInGrid)
        {
            AEnemy.Initialize();
            AEnemy.m_CreatureAi.Initialize();
        }
    }

    public void RemoveEnemyFromList()
    {
        for (int i = m_EnemysInGrid.Count - 1; i >= 0; i--)
        {
            if (m_EnemysInGrid[i] == null)
            {
                m_EnemysInGrid.RemoveAt(i);
            }
        }
    }

    public void DeleteGrid()
    {
        if (m_GridPathArray.Length > 0)
        {
            for (int x = 0; x < m_GridDimensions.x; x++)
            {
                for (int y = 0; y < m_GridDimensions.y; y++)
                {

                    DestroyImmediate(m_GridPathArray[x, y]);

                }
            }
        }

    }

    public void ColorAllNodes()
    {
        for (int i = 0; i < m_ListToConvert.Count; i++)
        {
            Debug.Log("Issue");
            Material[] matArray = m_ListToConvert[i].m_Cube.GetComponent<MeshRenderer>().materials;
            matArray[0] = m_WallMaterial;
            matArray[1] = m_TopMaterial;
            m_ListToConvert[i].m_Cube.GetComponent<MeshRenderer>().materials = matArray;

        }
    
    }




}

