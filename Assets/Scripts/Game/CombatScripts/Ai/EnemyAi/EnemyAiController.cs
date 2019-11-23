using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAiController : AiController
{
    // Start is called before the first frame update

    public AiController m_Target;
    public bool m_AiFinished;
    public bool m_EndMovement;
    public int m_EnemyRange;
    private Dictionary<CombatNode, List<CombatNode>> cacheRangePath;

    public override void Start()
    {
        base.Start();
        m_EnemyRange = 6;
    }

    public HashSet<CombatNode> GetAvailableEnemysInRange(List<CombatNode> cells, CombatNode NodeHeuristicIsBasedOff)
    {
        cacheRangePath = new Dictionary<CombatNode, List<CombatNode>>();

        var paths = cacheRangePaths(cells, NodeHeuristicIsBasedOff);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];

            var pathCost = path.Sum(c => 1);
            key.m_Heuristic = pathCost;
            if (pathCost <= m_EnemyRange)
            {
                key.m_IsWalkable = true;
                cacheRangePath.Add(key, path);
            }
        }
        return new HashSet<CombatNode>(cacheRangePath.Keys);
    }

    public  bool CheckIfNodeIsClearForRange(CombatNode aNode)
    {
        // if the node is out of bounds, return -1 (an invalid tile index)

        if (aNode == null)
        {
            Debug.Log("YOU BROKE " + aNode.m_PositionInGrid.ToString());
        }

        CombatNode nodeIndex = aNode;

        // if the node is already closed, return -1 (an invalid tile index)
        if (nodeIndex.m_HeuristicCalculated == true)
        {
            return false;
        }
        // if the node can't be walked on, return -1 (an invalid tile index)

        if (nodeIndex.m_CombatsNodeType == CombatNode.CombatNodeTypes.Wall)
        {
            return false;
        }
        if (nodeIndex.m_NodeHeight > 0)
        {
            return false;
        }
        // return a valid tile index
        return true;
    }

    protected virtual Dictionary<CombatNode, Dictionary<CombatNode, int>> GetGraphRangeEdges(List<CombatNode> NodeList)
    {
        Dictionary<CombatNode, Dictionary<CombatNode, int>> ret = new Dictionary<CombatNode, Dictionary<CombatNode, int>>();

        foreach (CombatNode Node in NodeList)
        {
            if (CheckIfNodeIsClearForRange(Node) == true || Node.Equals(Node_ObjectIsOn))
            {
                ret[Node] = new Dictionary<CombatNode, int>();
                foreach (CombatNode neighbour in Node.GetNeighbours(NodeList))
                {
                    if (CheckIfNodeIsClearForRange(neighbour) == true)
                    {
                        ret[Node][neighbour] = neighbour.m_MovementCost;
                    }
                }
            }
        }
        return ret;
    }

        public override IEnumerator GetToGoal(List<CombatNode> aListOfNodes)
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
        
        GameManager.Instance.m_CombatManager.EnemyMovement();
        
    }
    
    
    public Dictionary<CombatNode, List<CombatNode>> cacheRangePaths(List<CombatNode> cells, CombatNode aNodeHeuristicIsBasedOn)
    {
        var edges = GetGraphRangeEdges(cells);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn);
        return paths;
    }

    public void EnemyMovement()
    {

        _pathsInRange = GetAvailableEnemysInRange(m_Grid.m_GridPathList, Node_ObjectIsOn);

        List<Creatures> m_AllysInRange = new List<Creatures>();
        foreach (CombatNode node in _pathsInRange)
        {
            if (node.m_CreatureOnGridPoint != null && m_Creature != node.m_CreatureOnGridPoint)
            {
                if (node.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Ally)
                {
                    m_AllysInRange.Add(node.m_CreatureOnGridPoint);
                }
            }
        }

        if (m_AllysInRange.Count > 0)
        {

            Creatures CharacterInRange = CharacterToFollowAndAttack(m_AllysInRange);

            CombatNode NodeNeightboringAlly = 
                GameManager.Instance.m_Grid.CheckNeighborsForLowestNumber(CharacterInRange.m_CreatureAi.m_Position);

            SetGoalPosition(NodeNeightboringAlly.m_PositionInGrid);

        }

    }

    public Creatures CharacterToFollowAndAttack(List<Creatures> aCharacterList)
    {

//        for (int i = 0; i < aCharacterList.Count; i++)
//        {
//            for (int j = 0; j < aCharacterList.Count; j++)
//            {
//                if (aCharacterList[j].CurrentHealth < aCharacterList[j + 1].CurrentHealth)
//                {
//                    Creatures tempA = aCharacterList[j];
//                    Creatures tempB = aCharacterList[j + 1];
//                    swap(ref tempA, ref tempB);
//                }
//            }
//        }

        return aCharacterList[0];
    }

    void swap(ref Creatures xp, ref Creatures yp)
    {
        Creatures temp = xp;
        xp = yp;
        yp = temp;
    }

}
