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
    public Behaviour m_Behaviour;
    public Dictionary<CombatNode, List<CombatNode>> cacheRangePath;

    public override void Start()
    {
        base.Start();
        m_EnemyRange = 6;
    }

    public HashSet<CombatNode> GetAvailableEnemysInRange(List<CombatNode> Acells, CombatNode ANodeHeuristicIsBasedOff,
        int ARange)
    {
        cacheRangePath = new Dictionary<CombatNode, List<CombatNode>>();

        var paths = cacheRangePaths(Acells, ANodeHeuristicIsBasedOff);
        foreach (var key in paths.Keys)
        {
            var path = paths[key];

            var pathCost = path.Sum(c => 1);
            key.m_Heuristic = pathCost;
            if (pathCost <= ARange)
            {
                key.m_IsWalkable = true;
                cacheRangePath.Add(key, path);
            }
        }

        return new HashSet<CombatNode>(cacheRangePath.Keys);
    }

    public override IEnumerator GetToGoal(List<CombatNode> aListOfNodes)
    {
        m_MovementHasStarted = true;
        //  m_Grid.RemoveWalkableArea();
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
        GameManager.Instance.m_BattleCamera.m_cameraState = CombatCameraController.CameraState.PlayerMovement;
        Node_ObjectIsOn.m_CreatureOnGridPoint = null;
        Node_ObjectIsOn.m_CombatsNodeType = CombatNode.CombatNodeTypes.Normal;
        for (int i = 0; i < aListOfNodes.Count;)
        {

            if (Node_MovingTo == Node_ObjectIsOn)
            {

                Node_MovingTo = aListOfNodes[i];




                Vector3 relativePos =
                    aListOfNodes[i].gameObject.transform.position - transform.position + CreatureOffset;


                m_Position = Node_MovingTo.m_PositionInGrid;

                GameManager.Instance.m_BattleCamera.m_CameraPositionInGrid = m_Position;


                transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                CreatureOffset = new Vector3(0,
                    Constants.Constants.m_HeightOffTheGrid + Node_MovingTo.m_NodeHeightOffset, 0);
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

        Action_Move MoveAction = new Action_Move();
        MoveAction.SetupAction(m_Creature, Node_ObjectIsOn);
        CommandProcessor.Instance.m_ActionsStack.Add(MoveAction);

        for (int i = aListOfNodes.Count; i < 0; i--)
        {
            aListOfNodes.RemoveAt(i);
        }

        EnemyAttack();
    }


    public Dictionary<CombatNode, List<CombatNode>> cacheRangePaths(List<CombatNode> cells,
        CombatNode aNodeHeuristicIsBasedOn)
    {
        var edges = m_Behaviour.GetGraphRangeEdges(cells, Node_ObjectIsOn);
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn);
        return paths;
    }

    public void EnemyMovement()
    {
        //   m_Grid.RemoveWalkableArea();
        _pathsInRange = GetAvailableEnemysInRange(m_Grid.m_GridPathList, Node_ObjectIsOn, m_EnemyRange);

        List<Creatures> m_AllysInRange = new List<Creatures>();
        foreach (CombatNode node in _pathsInRange)
        {
            if (CheckIfAllyIsOnNode(node))
            {
                m_AllysInRange.Add(node.m_CreatureOnGridPoint);
            }
        }

        if (m_AllysInRange.Count > 0)
        {

            Creatures CharacterInRange = m_Behaviour.AllyToAttack(m_AllysInRange);

            CombatNode NodeNeightboringAlly =
                GameManager.Instance.m_Grid.CheckNeighborsForLowestNumber(CharacterInRange.m_CreatureAi.m_Position);

            SetGoalPosition(NodeNeightboringAlly.m_PositionInGrid);
            return;
        }
        else
        {
            EnemyAttack();
        }


    }

    public void EnemyAttack()
    {
        _pathsInRange =
            GetAvailableEnemysInRange(m_Grid.m_GridPathList, Node_ObjectIsOn, m_Creature.m_Attack.m_SkillRange);

        List<Creatures> m_AllysInRange = new List<Creatures>();
        foreach (CombatNode node in _pathsInRange)
        {
            if (CheckIfAllyIsOnNode(node))
            {
                m_AllysInRange.Add(node.m_CreatureOnGridPoint);
            }
        }

        if (m_AllysInRange.Count > 0)
        {
            Creatures CharacterInRange = m_Behaviour.AllyToAttack(m_AllysInRange);

            Skills m_SkillToUse = m_Creature.m_Attack;

            StartCoroutine(CharacterInRange.DecrementHealth
                (m_SkillToUse.m_Damage, m_SkillToUse.m_ElementalType, 2.0f, 2.0f, 2.0f));
        }
        else
        {
            Debug.Log(m_Creature.Name + " waited");
        }

        GameManager.Instance.m_CombatManager.EnemyMovement();
        return;
    }

    public bool CheckIfAllyIsOnNode(CombatNode aNode)
    {
        if (aNode.m_CreatureOnGridPoint != null && m_Creature != aNode.m_CreatureOnGridPoint)
        {
            if (aNode.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Ally)
            {
                return true;
            }
        }

        return false;
    }
}
