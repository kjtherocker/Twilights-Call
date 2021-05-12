using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAiController : AiController
{
    // Start is called before the first frame update

    private Creatures m_EnemyCreature;
    public AiController m_Target;
    public bool m_AiFinished;
    public bool m_EndMovement;
    public int m_EnemyRange;
    public Behaviour m_Behaviour;
    public Dictionary<CombatNode, List<CombatNode>> cacheRangePath;

    public bool DoNothing;
    
    public override void Initialize()
    {
        base.Initialize();
        m_EnemyRange = 6;


        m_Behaviour = new Behaviour_WalkClose();
        if (m_AiModel == null)
        {
            m_AiModel = transform.GetChild(0);
        }

        m_EnemyCreature = GetComponent<Creatures>();
        
        if (m_CreaturesAnimator == null)
        {
            m_CreaturesAnimator = GetComponent<Animator>();
        }

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

        if (aListOfNodes == null)
        {
            Debug.Log("GetToPositionBroke");
            yield break;
        }

        m_MovementHasStarted = true; 
        
        m_CreaturesAnimator.SetBool("b_IsWalking", true);
        GameManager.Instance.m_TacticsCameraController.m_cameraState = TacticsCameraController.CameraState.PlayerMovement;
        Node_ObjectIsOn.m_CreatureOnGridPoint = null;
        Node_ObjectIsOn.m_IsCovered = false;
        for (int i = 0; i < aListOfNodes.Count;)
        {

            if (Node_MovingTo == Node_ObjectIsOn)
            {

                Node_MovingTo = aListOfNodes[i];




                Vector3 relativePos =
                    aListOfNodes[i].gameObject.transform.position - transform.position + CreatureOffset;


                m_Position = Node_MovingTo.m_PositionInGrid;

                GameManager.Instance.m_TacticsCameraController.m_CameraPositionInGrid = m_Position;


                m_AiModel.rotation = Quaternion.LookRotation(relativePos, Vector3.up);

                CreatureOffset = new Vector3(0,
                    Constants.Helpers.m_HeightOffTheGrid + Node_MovingTo.m_NodeHeightOffset, 0);
                i++;
                yield return new WaitUntil(() => Node_MovingTo == Node_ObjectIsOn);
            }


        }
        
        //Camera no longer following the player;
        GameManager.Instance.m_TacticsCameraController.m_cameraState = TacticsCameraController.CameraState.Normal;

        //Setting the Walk Animation
        m_CreaturesAnimator.SetBool("b_IsWalking", false);

        //The walk has been finished
        m_HasMovedForThisTurn = true;

        m_MovementHasStarted = false;
        //Changing the position from where the Creature was before


        m_Position = aListOfNodes[aListOfNodes.Count - 1].m_PositionInGrid;

        //Setting the node you are on to the new one
        Node_ObjectIsOn = Grid.instance.GetNode(m_Position);


        Node_ObjectIsOn.m_IsGoal = false;
        Node_ObjectIsOn.m_IsWalkable = false;
        Node_ObjectIsOn.SetCreatureOnTopOfNode(m_EnemyCreature);
        Node_ObjectIsOn.m_IsCovered = true;

         m_Grid.RemoveWalkableArea();

       //Action_Move MoveAction = new Action_Move();
       //MoveAction.SetupAction(m_Creature, Node_ObjectIsOn);
       //CommandProcessor.Instance.m_ActionsStack.Add(MoveAction);

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
        var paths = _Pathfinder.findAllPaths(edges, aNodeHeuristicIsBasedOn,m_Movement);
        return paths;
    }

    public void EnemyMovement()
    {
        m_Grid.RemoveWalkableArea();
        FindAllPaths();
        m_NodeInWalkableRange = GetAvailableEnemysInRange(m_Grid.m_GridPathList, Node_ObjectIsOn, m_EnemyRange);

        List<Creatures> m_AllysInRange = new List<Creatures>();
        foreach (CombatNode node in m_NodeInWalkableRange)
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
                Grid.instance.CheckNeighborsForLowestNumber(CharacterInRange.m_CreatureAi.m_Position);


            if (NodeNeightboringAlly != null)
            {
                SetGoalPosition(NodeNeightboringAlly.m_PositionInGrid);
                return;
            }
            else
            {
                Debug.Log(gameObject.name +" Failed to be able to get to node ");
                return;
            }
        }

            EnemyAttack();
    }

    public void EnemyAttack()
    {
        m_NodeInWalkableRange =
            GetAvailableEnemysInRange(m_Grid.m_GridPathList, Node_ObjectIsOn, m_EnemyCreature.m_Attack.m_SkillRange);
        

        List<Creatures> m_AllysInRange = new List<Creatures>();
        foreach (CombatNode node in m_NodeInWalkableRange)
        {
            if (CheckIfAllyIsOnNode(node))
            {
                m_AllysInRange.Add(node.m_CreatureOnGridPoint);
            }
        }

        if (m_AllysInRange.Count > 0)
        {
            Creatures CharacterInRange = m_Behaviour.AllyToAttack(m_AllysInRange);

            Skills m_SkillToUse = m_EnemyCreature.m_Attack;

            
            StartCoroutine(m_SkillToUse.UseSkill(CharacterInRange,m_EnemyCreature ));
            
          //  List<Creatures> CharacterInRange= new List<Creatures>(); 
//
          //  CharacterInRange.Add(m_Behaviour.AllyToAttack(m_AllysInRange));
          //  
          //  Skills m_SkillToUse = m_EnemyCreature.m_Attack;
//
//
          //  
          //  
          //  StartCoroutine(
          //      TacticsManager.instance.ActivateSkill(m_SkillToUse.UseSkill(CharacterInRange[0], m_EnemyCreature),CharacterInRange ));
        }
        else
        {
            Debug.Log(m_EnemyCreature.m_Name + " waited");
        }

        TacticsManager.instance.EnemyMovement();
        return;
    }

    public bool CheckIfAllyIsOnNode(CombatNode aNode)
    {
        if (aNode.m_CreatureOnGridPoint != null && m_EnemyCreature != aNode.m_CreatureOnGridPoint)
        {
            if (aNode.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Ally)
            {
                return true;
            }
        }

        return false;
    }
}
