using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]

public class CombatNode : Cell
{
    public enum CombatNodeTypes
    {
        Normal,
        Idontwannaremoveitfonowsochangeitlaterkylealsofuckyou,
        Wall,
        Empty
    }

    public enum CombatNodeAreaType
    {
        Walkable,
        Domainable,
        Devourable
    }

    public enum WalkOntopTriggerTypes
    {
        None,
        RelicTower,
        DialoguePrompt,
        Items,
        Memoria
        
    }
    
    public enum DomainCombatNode
    {
        None,
        Domain
    }

    public int m_Heuristic;
    public Vector2Int m_PositionInGrid;

    public int m_MovementCost;

    public int m_NodeRotation;

    public int m_NodeHeight;

    public float m_NodeHeightOffset;
    
    public bool m_IsGoal;
    public bool m_HeuristicCalculated;
    public bool m_IsSelector;
    public bool m_IsWalkable;
    public bool m_IsCovered;
    public DomainCombatNode m_DomainCombatNode;
    public WalkOntopTriggerTypes m_WalkOnTopTriggerTypes;
    public Domain DomainOnNode;
    
    public NodeReplacement m_NodeReplacement;
    

    public Creatures m_CreatureOnGridPoint;

    List<CombatNode> neighbours = null;

    public GameObject m_WalkablePlane;
    public GameObject m_CurrentWalkablePlaneBeingUsed;
    public GameObject m_AttackingPlane;
    public GameObject m_Cube;
    public GameObject m_Prop;
    

    public Renderer m_Renderer;

    public Material m_WhiteNode;

    public GameObject m_InitalNode;

    public Vector3 m_NodesInitalVector3Coordinates;


    public Grid m_Grid;


    public CombatNodeTypes m_CombatsNodeType;

    public RelicTower m_RelicTower;
    
    public PropList.Props m_PropOnNode;
    PropList.Props m_PropOnNodeTemp;

    public PropList.NodeReplacements m_NodeReplacementOnNode;
    PropList.NodeReplacements m_NodeReplacementTemp;

    public GridFormations NodesGridFormation;

    public Material m_IniitalMaterial;
    private MeshRenderer m_MeshRenderer;

    public Memoria m_MemoriaOnTop;
    
    private float m_DomainSwapAmount;
    // Use this for initialization
    public void Initialize()
    {
        m_MovementCost = 1;

        m_Grid = Grid.Instance;
        
        if (m_NodeReplacement == null)
        {
            m_CurrentWalkablePlaneBeingUsed = m_WalkablePlane;
        }

        m_DomainCombatNode = CombatNode.DomainCombatNode.None;

        m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(false);
        m_AttackingPlane.gameObject.SetActive(false);
        m_Cube.gameObject.SetActive(true);
        m_IsSelector = false;

        
        m_MeshRenderer = m_Cube.GetComponent<MeshRenderer>();
        m_IniitalMaterial = m_MeshRenderer.materials[1];
        
        m_PropOnNodeTemp = m_PropOnNode;

        m_NodesInitalVector3Coordinates = gameObject.transform.position;
        SetPropState();
        

    }

    private void OnEnable()
    {
        m_PropOnNodeTemp = m_PropOnNode;
    }

    public void SetPropState()
    {
        if (m_CombatsNodeType == CombatNodeTypes.Empty)
        {
            m_Cube.gameObject.SetActive(false);
        }

        if (m_CombatsNodeType != CombatNodeTypes.Empty)
        {
            m_Cube.gameObject.SetActive(true);
        }

        if (m_CreatureOnGridPoint == null || m_Prop == null)
        {
            //SpawnProp();
        }

        if (m_PropOnNodeTemp != m_PropOnNode)
        {
            DestroyProp();
            SpawnProp();
        }


        if (m_PropOnNode == PropList.Props.None)
        {
            if (m_Prop != null)
            {
                DestroyProp();
            }
        }

        if (m_NodeReplacementTemp != m_NodeReplacementOnNode)
        {
            if (m_NodeReplacement != null)
            {
                DestroyNodeReplacement();
            }
            SpawnNodeReplacement();
        }


        if (m_NodeReplacementOnNode == PropList.NodeReplacements.None)
        {
            if (m_NodeReplacement != null)
            {
                DestroyNodeReplacement();
            }
        }

        if (m_NodeRotation <= 0)
        {
            m_NodeRotation = 4;
        }
        if (m_NodeRotation > 4)
        {
            m_NodeRotation = 1;
        }


        if (m_Prop != null)
        {
            if (m_NodeRotation == 1)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
            if (m_NodeRotation == 2)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            if (m_NodeRotation == 3)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
            }
            if (m_NodeRotation == 4)
            {
                m_Prop.transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));
            }
        }


        if (m_NodeReplacement != null)
        {
            if (m_NodeRotation == 1)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 90, m_NodeReplacement.transform.rotation.y));
            }
            if (m_NodeRotation == 2)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 180, m_NodeReplacement.transform.rotation.y));
            }
            if (m_NodeRotation == 3)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 270, m_NodeReplacement.transform.rotation.y));
            }
            if (m_NodeRotation == 4)
            {
                m_NodeReplacement.transform.rotation = Quaternion.Euler(new Vector3(m_NodeReplacement.transform.rotation.x, 360, m_NodeReplacement.transform.rotation.y));
            }
        }

        if (m_NodeHeight == 0)
        {
            gameObject.transform.position = m_NodesInitalVector3Coordinates;
        }

        if (m_NodeHeight == 1)
        {
            // gameObject.transform.position = gameObject.transform.position + new Vector3(0, 2, 0);
        }
    }


    public void DestroyNodeReplacement()
    {
       // DestroyImmediate(m_NodeReplacement.gameObject);
       // m_CurrentWalkablePlaneBeingUsed = m_WalkablePlane;
    }

    public void SetCombatNode(CombatNode aCombatnode)
    {
        m_CombatsNodeType = aCombatnode.m_CombatsNodeType;
   
        m_PropOnNode = aCombatnode.m_PropOnNode;
        m_PropOnNodeTemp = m_PropOnNode;
   
        m_NodeReplacementOnNode = aCombatnode.m_NodeReplacementOnNode;
        m_NodeReplacementTemp = m_NodeReplacementOnNode;
        SetPropState();
    }

    public void DestroyProp()
    {

        DestroyImmediate(m_Prop);
        m_CombatsNodeType = CombatNodeTypes.Normal;

      //  GridFormations.RespawnCube(m_PositionInGrid);

    }

    public void SpawnProp()
    {
        m_PropOnNodeTemp = m_PropOnNode;
        m_Prop = Instantiate<GameObject>(GameManager.Instance.m_PropList.ReturnPropData(m_PropOnNode));
        m_Prop.transform.parent = transform;
        Vector3 CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_Prop.gameObject.transform.position = gameObject.transform.position + CreatureOffset;

        Creatures CreatureTemp = m_Prop.GetComponent<Creatures>();
        if (CreatureTemp != null)
        {
            m_CreatureOnGridPoint = CreatureTemp;
        }
        m_CombatsNodeType = CombatNodeTypes.Wall;

    }

    public void SpawnNodeReplacement()
    {
        
        if (m_NodeReplacementOnNode != PropList.NodeReplacements.None)
        {
            m_NodeReplacementTemp = m_NodeReplacementOnNode;
            m_NodeReplacement = Instantiate(GameManager.Instance.m_PropList.NodeReplacementData(m_NodeReplacementOnNode), this.gameObject.transform);
            Vector3 CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
            m_NodeReplacement.gameObject.transform.position =  gameObject.transform.position + m_NodeReplacement.m_NodeSpawnOffSet + CreatureOffset;
            m_NodeHeightOffset = m_NodeReplacement.m_NodeHeightOffset;
            m_CurrentWalkablePlaneBeingUsed = m_NodeReplacement.m_Walkable;

            if (m_NodeReplacement.m_NodeReplacementType == NodeReplacement.NodeReplacementType.RemoveInitalNode)
            {
                m_InitalNode.gameObject.SetActive(false);
            }

        }
    }

    public void DomainClashing()
    {
        foreach (CombatNode neighbour in GetNeighbours(m_Grid.m_GridPathList))
        {
            if (neighbour.DomainOnNode == null)
            {
                continue;
            }

            if (neighbour.DomainOnNode.DomainUser == "")
            {
                continue;
            }

            if (neighbour.DomainOnNode.DomainUser != DomainOnNode.DomainUser)
            {
               CombatManager.Instance.SetDomainClash(
                    DomainOnNode.m_Creature, neighbour.DomainOnNode.m_Creature);

            }
        }
    }



    public void SpawnMemoria(List<Skills> a_Skills)
    {
        m_MemoriaOnTop = CombatManager.Instance.ReturnMemoria();
        
        m_MemoriaOnTop.transform.position =
            new Vector3( transform.position.x , transform.position.y  + Constants.Constants.m_HeightOffTheGrid , transform.position.z);

        m_MemoriaOnTop.AttachSkills(a_Skills);

        m_MemoriaOnTop.m_NodePosition = m_PositionInGrid;

        m_WalkOnTopTriggerTypes = WalkOntopTriggerTypes.Memoria;

    }

    public void DomainTransfer(Material aDomainMaterial)
    {
        m_DomainSwapAmount = 0;

        

        
        if (aDomainMaterial != null)
        {
            m_MeshRenderer.materials[1].SetTexture("_SecTex", aDomainMaterial.mainTexture); 
            m_MeshRenderer.materials[1].SetColor("_SecColor", aDomainMaterial.color);
        }

        StartCoroutine(DomainChangeAnimation(true));
    }
    
    public void DomainRevert()
    {
        m_DomainSwapAmount = 1;

        m_DomainCombatNode = CombatNode.DomainCombatNode.None;
        DomainOnNode = null;
        StartCoroutine(DomainChangeAnimation(false));
    }


    public IEnumerator DomainChangeAnimation(bool aIsChangingToNewTexture)
    {
        if (m_DomainSwapAmount > 1.1 || m_DomainSwapAmount < -0.1)
        {
           
            yield break;
        }


        if (aIsChangingToNewTexture)
        {
            m_DomainSwapAmount += 0.01f;
        }
        else
        {
            m_DomainSwapAmount -= 0.01f;
        }


        yield return new WaitForSeconds(0.01f);
        m_MeshRenderer.materials[1].SetFloat("_SliceAmount", m_DomainSwapAmount);
        StartCoroutine(DomainChangeAnimation(aIsChangingToNewTexture));
    }



   public void SetCreatureOnTopOfNode(Creatures aCreatures)
   {
       m_CreatureOnGridPoint = aCreatures;

       ActivateWalkOnTopTrigger();

   }


   public void ActivateWalkOnTopTrigger()
   {
       switch (m_WalkOnTopTriggerTypes)
       {
           case WalkOntopTriggerTypes.None:

               break;
           case WalkOntopTriggerTypes.RelicTower:
               m_RelicTower.ActivateRelicTower(m_CreatureOnGridPoint.m_Domain);
               break ;
           case WalkOntopTriggerTypes.Items:
               
               break ;
           case WalkOntopTriggerTypes.DialoguePrompt:
               
               break ;
           case  WalkOntopTriggerTypes.Memoria:
               
               UiManager.Instance.PushScreen(UiManager.Screen.Memoria);
               
               UiMemoria ScreenTemp =
                   UiManager.Instance.GetScreen(UiManager.Screen.Memoria) as UiMemoria;

               ScreenTemp.SetMemoriaScreen(m_CreatureOnGridPoint,m_MemoriaOnTop);
               
               break;
               
       }
   }
   
   
   

   public void CreateWalkableArea(CombatNodeAreaType aAreaType)
    {

        if (aAreaType == CombatNodeAreaType.Walkable)
        {
           // m_WalkablePlane.GetComponent<Renderer>().material = m_Grid.m_WalkableTile;
            m_WalkablePlane.GetComponent<Animator>().SetBool("b_TileGrow", true);
            m_IsWalkable = true;
        }
        else if (aAreaType == CombatNodeAreaType.Devourable)
        {
           // m_WalkablePlane.GetComponent<Renderer>().material = m_Grid.m_DevourTile;
            m_WalkablePlane.GetComponent<Animator>().SetBool("b_TileGrow", true);
            m_IsWalkable = true;
        }
        else if (aAreaType == CombatNodeAreaType.Domainable)
        {
            //m_WalkablePlane.GetComponent<Renderer>().material = m_Grid.m_DomainTile;
            m_WalkablePlane.GetComponent<Animator>().SetBool("b_TileGrow", true);
            m_IsWalkable = true;
        }

        m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(true);

        
    }

    public void RemoveWalkableArea(CombatNodeAreaType aAreaType)
    {
        if (aAreaType == CombatNodeAreaType.Walkable)
        {
            m_IsWalkable = false;
            m_Heuristic = 0;
        }
        
        m_CurrentWalkablePlaneBeingUsed.gameObject.SetActive(false);

        
    }

    
    protected static readonly Vector2[] _directions =
    {
        new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1)
    };

    public int GetDistance(CombatNode other)
    {
        return (int)(Mathf.Abs(m_PositionInGrid.x - other.m_PositionInGrid.x) + Mathf.Abs(m_PositionInGrid.y - other.m_PositionInGrid.y));
    }
    
    //Distance is given using Manhattan Norm.


    public override List<CombatNode> GetNeighbours(List<CombatNode> cells)
    {
        if (neighbours == null)
        {
            neighbours = new List<CombatNode>(4);
            foreach (var direction in _directions)
            {
                var neighbour = cells.Find(c => c.m_PositionInGrid == m_PositionInGrid + direction);
                if (neighbour == null) continue;

                neighbours.Add(neighbour);
            }
        }

        return neighbours;
    }

    public void SpawnEnemy()
    {
        #if UNITY_EDITOR
        Vector3 CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        

      //  GameObject Enemy = PrefabUtility.
      //      InstantiatePrefab(EnemyList.Instance.ReturnEnemyData(EnemyList.EnemyEnum.RedKnight1)) as GameObject;
//

      //  NodesGridFormation.m_EnemysInGrid.Add(Enemy.GetComponent<Creatures>());


     //  Enemy.transform.parent = NodesGridFormation.Enemy.transform;
     //  Enemy.transform.position = gameObject.transform.position + CreatureOffset;
     //  Enemy.transform.rotation = Quaternion.Euler(0.0f, 180, 0.0f);
     //  EnemyAiController m_CreatureAi = Enemy.GetComponent<EnemyAiController>();
     //  m_CreatureAi.m_Position = m_PositionInGrid;

     //  m_CreatureAi.m_Grid = m_Grid;
     //  
     // m_CreatureOnGridPoint = Enemy.GetComponent<Creatures>();
     // m_IsCovered = true;
       #endif
    }

    public void EditorSelector()
    {
        
        Vector3 NodeTransform = gameObject.transform.position;
#if UNITY_EDITOR
        GridFormationEditor.Instance.m_Selector.gameObject.transform.position = new Vector3(NodeTransform.x,NodeTransform.y + Constants.Constants.m_HeightOffTheGrid + 0.4f,NodeTransform.z );
#endif
    }
}



