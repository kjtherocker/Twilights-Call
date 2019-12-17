﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum DomainState
{
    None,
    Emenating
}


public class AiController : MonoBehaviour
{
    public Grid m_Grid;
    public CombatNode[,] m_GridPathArray;
    public List<CombatNode> m_GridPath;
    public Vector2Int m_Goal;
    public Vector2Int m_Position;
    public Vector2Int m_InitalPosition;

    public DomainState m_Domainstate;
    
    public Pathfinder _Pathfinder;
    public CombatNode Node_MovingTo;
    public CombatNode Node_ObjectIsOn;
    public Animator m_CreaturesAnimator;
    public Creatures m_Creature;

    private Dictionary<CombatNode, List<CombatNode>> cachedPaths = null;

    protected HashSet<CombatNode> _pathsInRange;

    public Vector3 CreatureOffset;

    public int m_Movement;
    public int m_Jump;

    public bool m_MovementHasStarted;
    public bool m_HasAttackedForThisTurn;
    public bool m_HasMovedForThisTurn;

    public delegate bool DelegateReturnNodeIndex(CombatNode node, Vector2Int Postion);

    public MovementType m_MovementType;
    // Use this for initialization
    public virtual void Start ()
    {
        //m_Goal = new Vector2Int(9, 2);
        //m_Position = new Vector2Int(4, 4);
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_Jump = 2;
        m_HasMovedForThisTurn = false;
        m_MovementHasStarted = false;
        m_InitalPosition = m_Position;

        Node_ObjectIsOn = GameManager.Instance.m_Grid.GetNode(m_Position);
        Node_MovingTo = Node_ObjectIsOn;


        if (m_CreaturesAnimator == null)
        {
            m_CreaturesAnimator = GetComponentInChildren<Animator>();
        }

        if (m_MovementType == null)
        {
            m_MovementType = GetComponent<MovementType>();
        }

        m_Grid = GameManager.Instance.m_Grid;

        m_GridPathArray = m_Grid.m_GridPathArray;


        _Pathfinder = new Pathfinder();

    }

    // Update is called once per frame
    public virtual void Update ()
    {

        if (Node_ObjectIsOn != Node_MovingTo)
        {
            transform.position = Vector3.MoveTowards
                    (transform.position, Node_MovingTo.gameObject.transform.position + CreatureOffset,
                    8 * Time.deltaTime);
        }

        if (m_MovementHasStarted == true)
        {
            if (transform.position == Node_MovingTo.transform.position + CreatureOffset)
            {
                Node_ObjectIsOn = Node_MovingTo;
            }
        }
    }

    public void SetGoal(Vector2Int m_Goal)
    {
      m_Grid.SetHeuristicToZero();
      m_Grid.m_GridPathToGoal.Clear();
      m_Grid.RemoveWalkableArea();
      m_Grid.m_GridPathArray[m_Goal.x, m_Goal.y].m_IsGoal = true;
      


    }

    public void FindAllPaths()
    {
        _pathsInRange = GetAvailableDestinations(m_Grid.m_GridPathList, Node_ObjectIsOn,m_Movement);


        foreach (CombatNode node in _pathsInRange)
        {
            node.CreateWalkableArea();
        }
    }

    public void DeselectAllPaths()
    {
        if (_pathsInRange != null)
        {

            foreach (CombatNode node in _pathsInRange)
            {
                node.m_Heuristic = 0;
                node.RemoveWalkableArea();
            }
        }
    }

    public HashSet<CombatNode> GetAvailableDestinations(List<CombatNode> cells, CombatNode NodeHeuristicIsBasedOff, int Range)
    {
        cachedPaths = new Dictionary<CombatNode, List<CombatNode>>();

        var paths = cachePaths(cells, NodeHeuristicIsBasedOff,m_MovementType.CheckIfNodeIsClearAndReturnNodeIndex);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];
            
            var pathCost = path.Sum(c => c.m_MovementCost);
            key.m_Heuristic = pathCost;
            if (pathCost <= Range)
            {
                cachedPaths.Add(key, path);
            }
        }
        return new HashSet<CombatNode>(cachedPaths.Keys);
    }


    public virtual void SetGoalPosition(Vector2Int m_Goal)
    {
        
        Debug.Log(m_Goal);
        SetGoal(m_Goal);
        m_Grid.m_Movement = m_Movement;

        _pathsInRange = GetAvailableDestinations(m_Grid.m_GridPathList, m_Grid.m_GridPathArray[m_Goal.x, m_Goal.y],100);


        foreach (CombatNode node in _pathsInRange)
        {
            node.m_IsWalkable = true;
        }



        List<CombatNode> TempList = m_Grid.GetTheLowestH(Node_ObjectIsOn.m_PositionInGrid,m_Movement);


        StartCoroutine(GetToGoal(TempList));

    }

    public void Domain()
    {
        _pathsInRange = GetNodesInRange(m_Grid.m_GridPathList, m_Grid.m_GridPathArray[m_Position.x, m_Position.y],3);


        foreach (CombatNode node in _pathsInRange)
        {
            node.m_DomainCombatNode = CombatNode.DomainCombatNode.Domain;
            node.DomainOnNode = m_Creature.m_Domain;
            if (node.m_CreatureOnGridPoint != null)
            {
                node.DomainOnNode.DomainEffect(ref node.m_CreatureOnGridPoint);
            }


            node.DomainTransfer();
        }

    }
    
    public void Devour(int DevourRange)
    {
        _pathsInRange = GetNodesInRange(m_Grid.m_GridPathList, m_Grid.m_GridPathArray[m_Position.x, m_Position.y],DevourRange);


        foreach (CombatNode node in _pathsInRange)
        {
            MeshRenderer meshRenderer = node.m_Cube.GetComponent<MeshRenderer>();
            meshRenderer.material = meshRenderer.materials[1];
        }

    }
    
    public HashSet<CombatNode> GetNodesInRange(List<CombatNode> cells, CombatNode NodeHeuristicIsBasedOff, int Range)
    {
     //  cachedPaths = new Dictionary<CombatNode, List<CombatNode>>();

     //  var paths = cachePaths(cells, NodeHeuristicIsBasedOff,m_Creature.m_Domain.CheckifNodeCanBeDomained);
     //  foreach (var key in paths.Keys)
     //  {
     //      var path = paths[key];
     //      
     //      var pathCost = path.Sum(c => c.m_MovementCost);
     //      if (pathCost <= Range)
     //      {
     //          cachedPaths.Add(key, path);
     //      }
     //  }
       return new HashSet<CombatNode>(cachedPaths.Keys);
    }


    public virtual IEnumerator GetToGoal(List<CombatNode> aListOfNodes)
    {
        m_MovementHasStarted = true;
        m_Grid.RemoveWalkableArea();
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
        GameManager.Instance.m_BattleCamera.m_cameraState = CombatCameraController.CameraState.PlayerMovement;
        Node_ObjectIsOn.m_CreatureOnGridPoint = null;
        Node_ObjectIsOn.m_CombatsNodeType = CombatNode.CombatNodeTypes.Normal;
        for (int i = 0; i < aListOfNodes.Count;)
        {

                if (Node_MovingTo == Node_ObjectIsOn)
                {

                    Node_MovingTo = aListOfNodes[i];

                   


                    Vector3 relativePos = aListOfNodes[i].gameObject.transform.position - transform.position + CreatureOffset;


                    m_Position = Node_MovingTo.m_PositionInGrid;

                    GameManager.Instance.m_BattleCamera.m_CameraPositionInGrid = m_Position;


                    transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                    CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid + Node_MovingTo.m_NodeHeightOffset, 0);
                    i++;
                    yield return new WaitUntil(() => Node_MovingTo == Node_ObjectIsOn);
                }
            

        }

        m_Grid.RemoveWalkableArea();
        //Camera no longer following the player;
        GameManager.Instance.m_BattleCamera.m_cameraState = CombatCameraController.CameraState.Normal;

        //Setting the Walk Animation
        m_CreaturesAnimator.SetBool("b_IsWalking", false);

        //The walk has been finished
       m_HasMovedForThisTurn = true;

       m_MovementHasStarted = false;
        //Changing the position from where the Creature was before
      

        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;

        //Setting the node you are on to the new one
        Node_ObjectIsOn = GameManager.Instance.m_Grid.GetNode(m_Position);

        Node_ObjectIsOn.m_CreatureOnGridPoint = m_Creature;
        Node_ObjectIsOn.m_CombatsNodeType = CombatNode.CombatNodeTypes.Covered;

       // m_Grid.RemoveWalkableArea();

        for (int i = aListOfNodes.Count; i < 0; i--)
        {
            aListOfNodes.RemoveAt(i);
        }

        
    }

    public virtual Dictionary<CombatNode, List<CombatNode>> cachePaths(List<CombatNode> cells, CombatNode aNodeHeuristicIsBasedOn,DelegateReturnNodeIndex delegateReturnNodeIndex )
    {
        var edges = GetGraphEdges(cells,delegateReturnNodeIndex);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn);
        return paths;
    }

    protected virtual Dictionary<CombatNode, Dictionary<CombatNode, int>> GetGraphEdges(List<CombatNode> NodeList,DelegateReturnNodeIndex delegateReturnNodeIndex)
    {
        Dictionary<CombatNode, Dictionary<CombatNode, int>> ret = new Dictionary<CombatNode, Dictionary<CombatNode, int>>();

        foreach (CombatNode Node in NodeList)
        {
            if (delegateReturnNodeIndex(Node,m_Position) == true|| Node.Equals(Node_ObjectIsOn))
            {
                ret[Node] = new Dictionary<CombatNode, int>();
                foreach (CombatNode neighbour in Node.GetNeighbours(NodeList))
                {
                    if (delegateReturnNodeIndex(neighbour,m_Position) == true)
                    {
                        ret[Node][neighbour] = neighbour.m_MovementCost;
                    }
                }
            }
        }
        return ret;
    }

    public virtual void ReturnToInitalPosition()
    {
        if (m_MovementHasStarted == false)
        {
            GameManager.Instance.m_Grid.GetNode(m_Position).m_CreatureOnGridPoint = null;
            GameManager.Instance.m_Grid.GetNode(m_Position).m_CombatsNodeType = CombatNode.CombatNodeTypes.Normal;

            m_Position = m_InitalPosition;

            CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid + GameManager.Instance.m_Grid.GetNode(m_Position).m_NodeHeightOffset, 0);

            GameManager.Instance.m_Grid.GetNode(m_Position).m_CreatureOnGridPoint = m_Creature;
            gameObject.transform.position = GameManager.Instance.m_Grid.GetNode(m_Position).transform.position + CreatureOffset;

            m_HasMovedForThisTurn = false;
        }

    }
}
