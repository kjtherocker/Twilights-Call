using System.Collections;
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

    public Vector2Int m_Position;
    public Vector2Int m_InitalPosition;

    public CombatNode m_PreviousNode;
    public DomainState m_Domainstate;

    public Pathfinder _Pathfinder;
    public CombatNode Node_MovingTo;
    public CombatNode Node_ObjectIsOn;
    public Animator m_CreaturesAnimator;
    public Creatures m_Creature;

    public Transform m_AiModel;

    private Dictionary<CombatNode, List<CombatNode>> cachedPaths = null;

    public HealthBar m_Healthbar;

    protected HashSet<CombatNode> m_NodeInWalkableRange;
    protected HashSet<CombatNode> m_NodeInDevourRange;
    protected HashSet<CombatNode> m_NodeInDomainRange;


    public Vector3 CreatureOffset;

    public int m_Movement;
    public int m_Jump;

    public bool m_MovementHasStarted;
    public bool m_HasAttackedForThisTurn;
    public bool m_HasMovedForThisTurn;

    public delegate bool DelegateReturnNodeIndex(CombatNode node, Vector2Int Postion);

    public MovementType m_MovementType;

    // Use this for initialization
    public virtual void Start()
    {
        //m_Goal = new Vector2Int(9, 2);
        //m_Position = new Vector2Int(4, 4);
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        m_Jump = 2;
        m_HasMovedForThisTurn = false;
        m_MovementHasStarted = false;
        m_InitalPosition = m_Position;

        // m_Healthbar = gameObject.transform.parent.GetComponentInChildren<HealthBar>();
//
        // m_Healthbar.Partymember = m_Creature;

        Node_ObjectIsOn = GameManager.Instance.m_Grid.GetNode(m_Position);
        Node_MovingTo = Node_ObjectIsOn;


        if (m_AiModel == null)
        {
            m_AiModel = transform.GetChild(0);
        }


        if (m_CreaturesAnimator == null)
        {
            m_CreaturesAnimator = GetComponentInChildren<Animator>();
        }

        if (m_MovementType == null)
        {
            m_MovementType = GetComponent<MovementType>();
        }

        m_Grid = GameManager.Instance.m_Grid;




        _Pathfinder = new Pathfinder();

    }




    // Update is called once per frame
    public virtual void Update()
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
        m_Grid.GetNode(m_Goal.x, m_Goal.y).m_IsGoal = true;



    }

    public void FindAllPaths()
    {
        m_NodeInWalkableRange = GetAvailableDestinations(m_Grid.m_GridPathList, Node_ObjectIsOn, m_Movement);


        foreach (CombatNode node in m_NodeInWalkableRange)
        {
            node.CreateWalkableArea(CombatNode.CombatNodeAreaType.Walkable);
        }
    }

    public void DeselectAllPaths()
    {
        if (m_NodeInWalkableRange != null)
        {

            foreach (CombatNode node in m_NodeInWalkableRange)
            {
                node.m_Heuristic = 0;
                node.RemoveWalkableArea(CombatNode.CombatNodeAreaType.Walkable);
            }
        }
    }

    public HashSet<CombatNode> GetAvailableDestinations(List<CombatNode> cells, CombatNode NodeHeuristicIsBasedOff,
        int Range)
    {
        cachedPaths = new Dictionary<CombatNode, List<CombatNode>>();

        var paths = cachePaths(cells, NodeHeuristicIsBasedOff, m_MovementType.CheckIfNodeIsClearAndReturnNodeIndex);
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



    public HashSet<CombatNode> GetNodesInRange(List<CombatNode> aCells, CombatNode aNodeHeuristicIsBasedOff, int aRange)
    {
        cachedPaths = new Dictionary<CombatNode, List<CombatNode>>();

        var paths = cachePaths(aCells, aNodeHeuristicIsBasedOff,
            m_Creature.m_Domain.CheckIfNodeIsClearAndReturnNodeIndex);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];

            var pathCost = path.Sum(c => 1);
            key.m_Heuristic = pathCost;
            if (pathCost <= aRange)
            {
                cachedPaths.Add(key, path);
            }
        }

        return new HashSet<CombatNode>(cachedPaths.Keys);
    }


    public virtual void SetGoalPosition(Vector2Int m_Goal)
    {

        SetGoal(m_Goal);

        m_NodeInWalkableRange =
            GetAvailableDestinations(m_Grid.m_GridPathList, m_Grid.GetNode(m_Goal.x, m_Goal.y), 100);


        foreach (CombatNode node in m_NodeInWalkableRange)
        {
            node.m_IsWalkable = true;
        }



        List<CombatNode> TempList = m_Grid.GetTheLowestH(Node_ObjectIsOn.m_PositionInGrid, m_Movement);


        StartCoroutine(GetToGoal(TempList));

    }

    public void RemoveDomainArea()
    {
        if ( m_NodeInDomainRange != null)
        {
            if (m_NodeInDomainRange.Count > 0)
            {
                foreach (CombatNode node in m_NodeInDomainRange)
                {
                    node.RemoveWalkableArea(CombatNode.CombatNodeAreaType.Domainable);
                }
            }
        }
    }

    public void SetDomain(int aDomainRange)
    {

        RemoveDomainArea();
        
        m_NodeInDomainRange =
            GetNodesInRange(m_Grid.m_GridPathList, m_Grid.GetNode(m_Position.x, m_Position.y), aDomainRange);


        foreach (CombatNode node in m_NodeInDomainRange)
        {
            node.CreateWalkableArea(CombatNode.CombatNodeAreaType.Domainable);
        }

    }

    public void ActivateDomain()
    {

        foreach (CombatNode node in m_NodeInDomainRange)
        {
            node.m_DomainCombatNode = CombatNode.DomainCombatNode.Domain;
            node.DomainOnNode = m_Creature.m_Domain;
            if (node.m_CreatureOnGridPoint != null)
            {
                // node.m_CreatureOnGridPoint.StatsBeforeDomain();
                node.DomainOnNode.DomainEffect(ref node.m_CreatureOnGridPoint);
                node.m_CreatureOnGridPoint.DomainAffectingCreature = m_Creature.m_Domain.DomainName;
                node.RemoveWalkableArea(CombatNode.CombatNodeAreaType.Domainable);
            }


            node.DomainTransfer(m_Creature.m_Domain.m_DomainTexture);
        }

        RemoveDomainArea();
    }

    public void SetDevour(int DevourRange)
    {
        m_NodeInDevourRange =
            GetNodesInRange(m_Grid.m_GridPathList, m_Grid.GetNode(m_Position.x, m_Position.y), DevourRange);


        foreach (CombatNode node in m_NodeInDevourRange)
        {
            node.CreateWalkableArea(CombatNode.CombatNodeAreaType.Devourable);
        }

    }

    public void ActivateDevour()
    {
        
        foreach (CombatNode node in m_NodeInDevourRange)
        {
            node.RemoveWalkableArea(CombatNode.CombatNodeAreaType.Devourable);
            node.DomainRevert();
        }
    }



public virtual IEnumerator GetToGoal(List<CombatNode> aListOfNodes)
    {
        m_MovementHasStarted = true;
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
        GameManager.Instance.m_BattleCamera.m_cameraState = CombatCameraController.CameraState.PlayerMovement;
        Node_ObjectIsOn.m_CreatureOnGridPoint = null;
        Node_ObjectIsOn.m_IsCovered = false;
        for (int i = 0; i < aListOfNodes.Count;)
        {

                if (Node_MovingTo == Node_ObjectIsOn)
                {

                    Node_MovingTo = aListOfNodes[i];

                   


                    Vector3 relativePos = aListOfNodes[i].gameObject.transform.position - transform.position + CreatureOffset;


                    m_Position = Node_MovingTo.m_PositionInGrid;

                    GameManager.Instance.m_BattleCamera.m_CameraPositionInGrid = m_Position;

                    m_AiModel.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                    CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid + Node_MovingTo.m_NodeHeightOffset, 0);
                    i++;
                    yield return new WaitUntil(() => Node_MovingTo == Node_ObjectIsOn);
                }
            

        }

        
        //Camera no longer following the player;
        GameManager.Instance.m_BattleCamera.m_cameraState = CombatCameraController.CameraState.Normal;

        //Setting the Walk Animation
        m_CreaturesAnimator.SetBool("b_IsWalking", false);

        //The walk has been finished
       m_HasMovedForThisTurn = true;

       m_MovementHasStarted = false;
        //Changing the position from where the Creature was before
      

        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;
        m_PreviousNode = Node_ObjectIsOn;
        //Setting the node you are on to the new one
        Node_ObjectIsOn = GameManager.Instance.m_Grid.GetNode(m_Position);

        Node_ObjectIsOn.SetCreatureOnTopOfNode(m_Creature);
        Node_ObjectIsOn.m_IsCovered = true;
        
     //   m_PreviousNode.DomainOnNode.UndoDomainEffect(ref  Node_ObjectIsOn.m_CreatureOnGridPoint);
        
        
        for (int i = aListOfNodes.Count; i < 0; i--)
        {
            aListOfNodes.RemoveAt(i);
        }

        
    }
    


    public virtual Dictionary<CombatNode, List<CombatNode>> cachePaths(List<CombatNode> cells, CombatNode aNodeHeuristicIsBasedOn,DelegateReturnNodeIndex delegateReturnNodeIndex )
    {
        var edges = GetGraphEdges(cells,delegateReturnNodeIndex);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn,m_Movement);
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
