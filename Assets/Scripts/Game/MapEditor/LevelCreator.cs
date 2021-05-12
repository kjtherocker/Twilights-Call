using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class LevelCreator : Singleton<LevelCreator>
{
    // Start is called before the first frame update

    public enum MapEditorMode
    {
        None,
        Prop,
        Enemy,
        Terrain,
        NodeReplacement
    }
    
    public PropList m_PropList;
    public EnemyList m_EnemyList;

   public EnemyList.EnemyTypes m_EnemyTypes;
   public Node.NodeTypes m_NodeType;
   public CombatNode.CombatNodeTypes m_CombatNodeTypes;
   public PropList.NodeReplacements m_NodeReplacements;
   public PropList.Props m_PropIndex;
   
   
   public MapEditorMode m_LeftClickState;
   public MapEditorMode m_RightClickState;
   
   
   public  GameObject m_Selector;
   private CombatNode m_CurrentNode;
   private int layerMask;

   protected override void OnAwake()
   {
       m_Selector.gameObject.SetActive(false);
   }

   public void StartEditor()
    {
        m_PropList = new PropList();
        m_PropList.Initialize();
        
        m_EnemyList = new EnemyList();
        m_EnemyList.Initialize();
    }

    public void EditorSelector(Vector3 aPosition)
    {
        m_Selector.gameObject.transform.position = new Vector3(aPosition.x,aPosition.y + Constants.Helpers.m_HeightOffTheGrid + 0.4f,aPosition.z );
    }
    
    
    public void SpawnEnemy(CombatNode aCombatNode)
    {

        CombatNode NodeToSpawnOn = aCombatNode;
        
        if (NodeToSpawnOn.m_EnemyOnNodeTemp == NodeToSpawnOn.m_EnemyOnNode)
        {
            return;
        }
        else
        { 
            DestroyEnemy(NodeToSpawnOn);
        }
        
        if (NodeToSpawnOn.m_EnemyOnNode == EnemyList.EnemyTypes.None)
        {
            return;
        }

        if (m_EnemyList == null)
        {
            StartEditor();
        }

        NodeToSpawnOn.m_EnemyOnNodeTemp = NodeToSpawnOn.m_EnemyOnNode;
        
        Vector3 CreatureOffset = new Vector3(0, Constants.Helpers.m_HeightOffTheGrid, 0);
        

        GameObject Enemy = PrefabUtility.
            InstantiatePrefab(m_EnemyList.ReturnEnemyData(NodeToSpawnOn.m_EnemyOnNode)) as GameObject;


        Creatures m_EnemysCreature = Enemy.GetComponent<Creatures>();
       
        NodeToSpawnOn.m_CreatureOnGridPoint = m_EnemysCreature;
        NodeToSpawnOn.NodesGridFormation.m_EnemysInGrid.Add(m_EnemysCreature);
        
        Enemy.transform.parent = NodeToSpawnOn.NodesGridFormation.Enemy.transform;
        Enemy.transform.position = gameObject.transform.position + CreatureOffset;
        Enemy.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);

        EnemyAiController m_CreatureAi = (EnemyAiController)m_EnemysCreature.m_CreatureAi;

        m_CreatureAi.Node_ObjectIsOn = NodeToSpawnOn;
        m_CreatureAi.Node_MovingTo = NodeToSpawnOn;
        m_CreatureAi.m_Position = NodeToSpawnOn.m_PositionInGrid;
        m_CreatureAi.m_Grid = NodeToSpawnOn.m_Grid;
            
        
        NodeToSpawnOn.m_IsCovered = true;

    }
    
    public void DestroyEnemy(CombatNode aCombatNode)
    {
        if (aCombatNode.m_CreatureOnGridPoint == null)
        {
            return;
        }

        DestroyImmediate(aCombatNode.m_CreatureOnGridPoint.gameObject);
        aCombatNode.m_CreatureOnGridPoint = null;
        aCombatNode.m_IsCovered = false;
        aCombatNode.NodesGridFormation.RemoveEnemyFromList();

    }
    
    
    
    
    public void DestroyProp(CombatNode aCombatNode)
    {

        CombatNode NodeToDestroyOn = aCombatNode;
        
        DestroyImmediate(NodeToDestroyOn.m_Prop);
        NodeToDestroyOn.m_CombatsNodeType = CombatNode.CombatNodeTypes.Normal;

        //  GridFormations.RespawnCube(m_PositionInGrid);

    }

    public void SpawnProp(CombatNode aCombatNode)
    {
        CombatNode NodeToSpawnOn = aCombatNode;
        
        NodeToSpawnOn.m_PropOnNodeTemp = NodeToSpawnOn.m_PropOnNode;
        NodeToSpawnOn.m_Prop = Instantiate(m_PropList.ReturnPropData( NodeToSpawnOn.m_PropOnNode));
        NodeToSpawnOn.m_Prop.transform.parent = transform;
        Vector3 CreatureOffset = new Vector3(0, Constants.Helpers.m_HeightOffTheGrid, 0);
        NodeToSpawnOn.m_Prop.gameObject.transform.position = gameObject.transform.position + CreatureOffset;

        Creatures CreatureTemp =  NodeToSpawnOn.m_Prop.GetComponent<Creatures>();
        if (CreatureTemp != null)
        {
            NodeToSpawnOn.m_CreatureOnGridPoint = CreatureTemp;
        }
        NodeToSpawnOn.m_CombatsNodeType = CombatNode.CombatNodeTypes.Wall;

    }
    
    public void SpawnNodeReplacement(CombatNode aCombatNode)
    {
        
        CombatNode NodeToSpawnOn = aCombatNode;
        
        if (NodeToSpawnOn.m_NodeReplacementOnNode != PropList.NodeReplacements.None)
        {
            NodeToSpawnOn.m_NodeReplacementTemp = NodeToSpawnOn.m_NodeReplacementOnNode;
            NodeToSpawnOn.m_NodeReplacement = Instantiate(GameManager.Instance.m_PropList.NodeReplacementData(NodeToSpawnOn.m_NodeReplacementOnNode), this.gameObject.transform);
            Vector3 CreatureOffset = new Vector3(0, Constants.Helpers.m_HeightOffTheGrid, 0);
            NodeToSpawnOn.m_NodeReplacement.gameObject.transform.position =  gameObject.transform.position + NodeToSpawnOn.m_NodeReplacement.m_NodeSpawnOffSet + CreatureOffset;
            NodeToSpawnOn.m_NodeHeightOffset = NodeToSpawnOn.m_NodeReplacement.m_NodeHeightOffset;
            NodeToSpawnOn.m_CurrentWalkablePlaneBeingUsed = NodeToSpawnOn.m_NodeReplacement.m_Walkable;

            if (NodeToSpawnOn.m_NodeReplacement.m_NodeReplacementType == NodeReplacement.NodeReplacementType.RemoveInitalNode)
            {
                NodeToSpawnOn.m_InitalNode.gameObject.SetActive(false);
            }

        }
    }




    public void CheckNodeStatus(CombatNode aCombatNode)
    {
        CombatNode NodeToSpawnOn = aCombatNode;
        
        if (NodeToSpawnOn.m_CombatsNodeType == CombatNode.CombatNodeTypes.Empty)
        {
            NodeToSpawnOn.m_Cube.gameObject.SetActive(false);
        }

        if (NodeToSpawnOn.m_CombatsNodeType != CombatNode.CombatNodeTypes.Empty)
        {
            NodeToSpawnOn.m_Cube.gameObject.SetActive(true);
        }
        

        if (NodeToSpawnOn.m_PropOnNodeTemp != NodeToSpawnOn.m_PropOnNode)
        {
            DestroyProp(NodeToSpawnOn);
            SpawnProp(NodeToSpawnOn);
        }


        if (NodeToSpawnOn.m_PropOnNode == PropList.Props.None)
        {
            if (NodeToSpawnOn.m_Prop != null)
            {
                DestroyProp(NodeToSpawnOn);
            }
        }

        if (NodeToSpawnOn.m_NodeReplacementTemp != NodeToSpawnOn.m_NodeReplacementOnNode)
        {
            if (NodeToSpawnOn.m_NodeReplacement != null)
            {
                DestroyNodeReplacement(NodeToSpawnOn);
            }
            SpawnNodeReplacement(NodeToSpawnOn);
        }


        if (NodeToSpawnOn.m_NodeReplacementOnNode == PropList.NodeReplacements.None)
        {
            if (NodeToSpawnOn.m_NodeReplacement != null)
            {
                DestroyNodeReplacement(NodeToSpawnOn);
            }
        }

    }


    public void PropRotation(CombatNode aCombatNode)
    {
        CombatNode NodeToSpawnOn = aCombatNode;
        if (NodeToSpawnOn.m_Prop == null)
        {
            return;
        }

        NodeToSpawnOn.m_NodeRotation = Math.Max(NodeToSpawnOn.m_NodeRotation, 4);
        NodeToSpawnOn.m_NodeRotation = Math.Min(NodeToSpawnOn.m_NodeRotation, 1);

        if (NodeToSpawnOn.m_NodeRotation == 1)
        {
            NodeToSpawnOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        if (NodeToSpawnOn.m_NodeRotation == 2)
        {
            NodeToSpawnOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (NodeToSpawnOn.m_NodeRotation == 3)
        {
            NodeToSpawnOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        }
        if (NodeToSpawnOn.m_NodeRotation == 4)
        {
            NodeToSpawnOn.m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));
        }
        
    }

    public void NodeReplacementRotation(CombatNode aCombatNode)
    {
        CombatNode NodeToSpawnOn = aCombatNode;
        
        if (NodeToSpawnOn.m_NodeReplacement == null)
        {
            return;
        }
        
        if (NodeToSpawnOn.m_NodeRotation == 1)
        {
            NodeToSpawnOn.m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(NodeToSpawnOn.m_NodeReplacement.transform.rotation.x, 90, NodeToSpawnOn.m_NodeReplacement.transform.rotation.y));
        }
        if (NodeToSpawnOn.m_NodeRotation == 2)
        {
            NodeToSpawnOn.m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(NodeToSpawnOn.m_NodeReplacement.transform.rotation.x, 180, NodeToSpawnOn.m_NodeReplacement.transform.rotation.y));
        }
        if (NodeToSpawnOn.m_NodeRotation == 3)
        {
            NodeToSpawnOn.m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(NodeToSpawnOn.m_NodeReplacement.transform.rotation.x, 270, NodeToSpawnOn.m_NodeReplacement.transform.rotation.y));
        }
        if (NodeToSpawnOn.m_NodeRotation == 4)
        {
            NodeToSpawnOn.m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(NodeToSpawnOn.m_NodeReplacement.transform.rotation.x, 360, NodeToSpawnOn.m_NodeReplacement.transform.rotation.y));
        }
    }


    public void NodeHeight(CombatNode aCombatNode)
    {
        CombatNode NodeToSpawnOn = aCombatNode;
        
        if (NodeToSpawnOn.m_NodeHeight == 0)
        {
            //  gameObject.transform.position = m_NodesInitalVector3Coordinates;
        }

        if (NodeToSpawnOn.m_NodeHeight == 1)
        {
            // gameObject.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
        }
    }

    public void DestroyNodeReplacement(CombatNode aCombatNode)
    {
        CombatNode NodeToSpawnOn = aCombatNode; 
        
        DestroyImmediate(NodeToSpawnOn.m_NodeReplacement.gameObject);
        NodeToSpawnOn.m_CurrentWalkablePlaneBeingUsed = NodeToSpawnOn.m_WalkablePlane;
    }


    
    public void SetPropState(CombatNode aCombatNode)
    {
        CombatNode NodeToSpawnOn = aCombatNode;

        CheckNodeStatus(NodeToSpawnOn);
        
        PropRotation(NodeToSpawnOn);
        NodeReplacementRotation(NodeToSpawnOn);

        NodeHeight(NodeToSpawnOn);
    }
}
