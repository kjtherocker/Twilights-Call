using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatCameraController : MonoBehaviour
{
    public enum CameraState
    {
        Nothing,
        Normal,
        PlayerMovement,
        PlayerAttack,
        EnemyMovement,
        EnemyAttack


    }

    public enum CameraMovementDirections
    {
        Up,
        Down,
        Left,
        Right
    }

    public CameraState m_cameraState;

    public Grid m_Grid;
    public Creatures m_Creature;
    
    public Creatures m_ToFollowCreature;

    public Skills m_CreatureAttackingSkill;

    public HealthBar m_StatusSheet;
    public GameObject m_PartyStatus;

    public HealthBar m_EnemyStatusSheet;
    public GameObject m_EnemyStatus;

    public CombatNode m_NodeTheCameraIsOn;

    public Vector2Int m_CameraPositionInGrid;

    public TextMeshProUGUI m_NodePositionText;
    public TextMeshProUGUI m_NodeType;
    public TextMeshProUGUI m_NodeProp;
    public TextMeshProUGUI m_NodeHeuristic;


    public GameObject m_Selector;


    public List<Vector2Int> m_SpellAttackFormations;



    // Use this for initialization
    public bool m_CommandBoardExists;

    public bool m_MovementHasBeenCalculated;
    public bool m_PlayerIsMoving;
    public bool m_PlayerIsAttacking;

    void Start()
    {
        InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => DPadGridControls(movement.ReadValue<Vector2>());
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => CreateCommandBoard();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => PlayerWalk();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => AttackingIndividual();
        InputManager.Instance.m_MovementControls.Player.SquareButton.performed += SquareButton => ReturnToCommandboard();
        InputManager.Instance.m_MovementControls.Player.TriangleButton.performed += TriangleButton => EndTurn();

        m_CameraPositionInGrid = new Vector2Int(5, 5);
        GameManager.Instance.m_BattleCamera = this;

        if (m_NodeTheCameraIsOn != null)
        {
            m_Selector.gameObject.transform.position =
                new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, m_NodeTheCameraIsOn.transform.position.z);
            m_NodeTheCameraIsOn = m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y);
        }

        m_CommandBoardExists = false;
        m_PlayerIsAttacking = false;
        //m_Grid = GameManager.Instance.m_Grid;

        m_cameraState = CameraState.Normal;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_NodeTheCameraIsOn != null)
        {
            m_NodePositionText.text = "Postion " + m_NodeTheCameraIsOn.m_PositionInGrid.ToString();
            m_NodeType.text = "Type " + m_NodeTheCameraIsOn.m_CombatsNodeType.ToString();
            m_NodeProp.text = "Prop " + m_NodeTheCameraIsOn.m_PropOnNode.ToString();
            m_NodeHeuristic.text = "Heuristic " + m_NodeTheCameraIsOn.m_Heuristic.ToString();
           // m_NodeHeuristic.text = "IsWalkable " + m_NodeTheCameraIsOn.m_IsWalkable.ToString();
        }


        CameraMovement();
    }


    public void ReturnToCommandboard()
    {
        if (m_cameraState != CameraState.PlayerAttack)
        {
            return;
        }

        GameManager.Instance.m_UiManager.ReturnToLastScreen();
    }

    public void EndTurn()
    {
        if (m_cameraState != CameraState.Normal)
        {
            return;
        }

        StartCoroutine(GameManager.Instance.m_CombatManager.EnemyTurn());

    }


    public void CameraMovement()
    {



        switch (m_cameraState)
        {
            case CameraState.Nothing:


                break;

            case CameraState.Normal:
                if (m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y) != null)
                {
                    m_NodeTheCameraIsOn = m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y);
                    
                    transform.position = Vector3.Lerp(transform.position, new Vector3(
                        m_NodeTheCameraIsOn.transform.position.x + 18.5f,
                        m_NodeTheCameraIsOn.transform.position.y + 18.9f,
                        m_NodeTheCameraIsOn.transform.position.z - 18.5f), Time.deltaTime * 2);
                }
                //InputManager.Instance.m_MovementControls.Enable();
                

                break;

            case CameraState.PlayerMovement:

               if (m_Grid.m_GridPathArray != null)
               {
                   
                   //InputManager.Instance.m_MovementControls.Disable();
                   
                   m_NodeTheCameraIsOn = m_Grid.GetNode(m_ToFollowCreature.m_CreatureAi.m_Position.x, m_ToFollowCreature.m_CreatureAi.m_Position.y);
                   
                   transform.position = Vector3.Lerp(transform.position, new Vector3(
                           m_NodeTheCameraIsOn.gameObject.transform.position.x + 13.5f,
                           m_NodeTheCameraIsOn.gameObject.transform.position.y + 13.9f,
                           m_NodeTheCameraIsOn.gameObject.transform.position.z - 13.5f), Time.deltaTime * 2);
               }


                break;

            case CameraState.PlayerAttack:
               //
               // m_Grid.SetAttackingTileInGrid(m_CameraPositionInGrid);
               // m_Grid.SetAttackingTileInGrid(m_CameraPositionInGrid + new Vector2Int(1,0));

                transform.position = Vector3.Lerp(transform.position, new Vector3(
                        m_NodeTheCameraIsOn.transform.position.x + 13.5f,
                        m_NodeTheCameraIsOn.transform.position.y + 13.9f,
                        m_NodeTheCameraIsOn.transform.position.z - 13.5f), Time.deltaTime * 2);

               

                break;



            case CameraState.EnemyMovement:
                if (m_Grid.m_GridPathArray != null)
                {

                }

                break;

            case CameraState.EnemyAttack:


                break;
        }





       
    }

    public void DPadGridControls(Vector2 aMovement)
    {
        //Up
        if (aMovement == new Vector2(0.0f, 1.0f))
        {
            MoveCamera(CameraMovementDirections.Up);
        }

        //Down
        if (aMovement == new Vector2(0.0f, -1.0f))
        {
            MoveCamera(CameraMovementDirections.Down);
        }

        //Left
        if (aMovement == new Vector2(-1.0f, 0.0f))
        {
            MoveCamera(CameraMovementDirections.Left);
        }

        //Right
        if (aMovement == new Vector2(1.0f, 0.0f))
        {
            MoveCamera(CameraMovementDirections.Right);
        }
        
    }


    public void SetAttackPhase(Skills aSkill)
    {
        m_CreatureAttackingSkill = aSkill;
        m_SpellAttackFormations = GameManager.Instance.m_NodeFormation.NodeFormation();
        m_cameraState = CameraState.PlayerAttack;
        
        for (int i = 0; i < m_SpellAttackFormations.Count; i++)
        {
            Vector2Int TempSpellPosition = new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

            if (CheckingGridDimensionBoundrys(TempSpellPosition))
            {
                m_Grid.SetAttackingTileInGrid(new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y));
            }
        }
    }
    
    public void SetDomainPhase(Domain aDomain)
    {
        m_Creature.m_CreatureAi.Domain();
        m_cameraState = CameraState.PlayerAttack;
    }

    public void MoveCamera(CameraMovementDirections cameraMovementDirections )
    {

        Vector2Int TempInitalCameraPostion = m_CameraPositionInGrid;

        if (m_SpellAttackFormations != null)
        {
            for (int i = 0; i < m_SpellAttackFormations.Count; i++)
            {
                Vector2Int TempSpellPosition = new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

                if (CheckingGridDimensionBoundrys(TempSpellPosition))
                    {
                        m_Grid.DeselectAttackingTileingrid(new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                        m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y));
                    }
            }
        }

        if (cameraMovementDirections == CameraMovementDirections.Up)
        {
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x - 1, m_CameraPositionInGrid.y);
        }

        if (cameraMovementDirections == CameraMovementDirections.Down)
        {
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x + 1, m_CameraPositionInGrid.y);
        }


        if (cameraMovementDirections == CameraMovementDirections.Left)
        {
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y - 1);
        }


        if (cameraMovementDirections == CameraMovementDirections.Right)
        {
            m_CameraPositionInGrid = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y + 1);
        }



        if (CheckingGridDimensionBoundrys(m_CameraPositionInGrid))
        {
            m_NodeTheCameraIsOn = m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y);;
        }
        else
        {
            m_CameraPositionInGrid = TempInitalCameraPostion;
            m_NodeTheCameraIsOn = m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y);;
        }

        m_Selector.gameObject.transform.position =
            new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, m_NodeTheCameraIsOn.transform.position.z);

        if (m_SpellAttackFormations != null)
        {
            for (int i = 0; i < m_SpellAttackFormations.Count; i++)
            {
                Vector2Int TempSpellPosition = new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

                if (CheckingGridDimensionBoundrys(TempSpellPosition))
                {
                    m_Grid.SetAttackingTileInGrid(new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y));
                }
            }
        }
        
        if (m_NodeTheCameraIsOn != null)
        {
            if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
            {
                if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Ally)
                {

                    CalculateCreaturesPath(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    m_StatusSheet.gameObject.SetActive(true);
                    m_PartyStatus.gameObject.SetActive(true);
                    m_EnemyStatus.gameObject.SetActive(false);
                    if (m_StatusSheet.Partymember != m_NodeTheCameraIsOn.m_CreatureOnGridPoint)
                    {
                        m_StatusSheet.SetCharacter(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    }
                }
                else if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Enemy)
                {
                    CalculateCreaturesPath(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    m_PartyStatus.gameObject.SetActive(false);
                    m_EnemyStatus.gameObject.SetActive(true);
                    if (m_EnemyStatusSheet.Partymember != m_NodeTheCameraIsOn.m_CreatureOnGridPoint)
                    {
                        m_EnemyStatusSheet.SetCharacter(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    }
                }

            }
            else
            {
                
                
                
                m_PartyStatus.gameObject.SetActive(false);
                m_StatusSheet.gameObject.SetActive(false);
                m_EnemyStatus.gameObject.SetActive(false);

            }
        }
    }


    public bool CheckingGridDimensionBoundrys(Vector2Int aPositionInGrid)
    {
        if (aPositionInGrid.x < m_Grid.m_GridDimensions.x && aPositionInGrid.x >= 0 &&
            aPositionInGrid.y < m_Grid.m_GridDimensions.y && aPositionInGrid.y >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AttackingIndividual()
    {
        if (m_cameraState == CameraState.PlayerAttack)
        {
            //m_Creature.m_CreatureAi.m_CreaturesAnimator.SetTrigger("t_IsAttack");
            

            
            if (m_SpellAttackFormations != null)
            {
                for (int i = 0; i < m_SpellAttackFormations.Count; i++)
                {
                    Vector2Int TempSpellNodePosition = new Vector2Int(
                        m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                        m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

                    if (!CheckingGridDimensionBoundrys(TempSpellNodePosition))
                    {
                        continue;
                    }

                    if (m_Grid.GetNode(TempSpellNodePosition.x, TempSpellNodePosition.y).m_CreatureOnGridPoint != null)
                    {
                        continue;
                    }

                    SkillUsed(TempSpellNodePosition.x, TempSpellNodePosition.y);
                }
                    
                
            }

            

        }
    }


    public void SkillUsed(int ASpellNodePositionX, int ASpellNodePositiony)
    {
        if (m_CreatureAttackingSkill.GetSkillType() == Skills.SkillType.Attack)
        {
            if (m_Grid.GetNode(ASpellNodePositionX, ASpellNodePositiony).m_CreatureOnGridPoint != m_Creature)
            {
                StartCoroutine(m_Grid.GetNode(ASpellNodePositionX,ASpellNodePositiony).m_CreatureOnGridPoint.DecrementHealth
                (m_CreatureAttackingSkill.GetSkillDamage() + m_Creature.GetAllStrength(), m_CreatureAttackingSkill.GetElementalType(),
                    0.1f, 0.1f, 1));
            }
        }
        else if (m_CreatureAttackingSkill.GetSkillType() == Skills.SkillType.Heal)
        {
            StartCoroutine(m_Grid.GetNode(ASpellNodePositionX, ASpellNodePositiony)
                .m_CreatureOnGridPoint.IncrementHealth(m_CreatureAttackingSkill.GetSkillDamage()));
        }
        
        for (int i = 0; i < m_SpellAttackFormations.Count; i++)
        {
            Vector2Int TempSpellPosition = new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

            if (CheckingGridDimensionBoundrys(TempSpellPosition))
            {
                m_Grid.DeselectAttackingTileingrid(new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y));
            }
        }

        m_SpellAttackFormations = null;
        m_Creature.m_CreatureAi.m_HasAttackedForThisTurn = true;
        m_PlayerIsAttacking = false;
        m_cameraState = CameraState.Normal;
    }


    public void PlayerWalk()
    {

        if (m_Creature == null)
        {
            return;
        }

        if (m_Creature.m_CreatureAi.m_HasMovedForThisTurn == true)
        {
            return;
        }

        if (m_MovementHasBeenCalculated == false)
        {
            return;
        }

        if (m_NodeTheCameraIsOn.m_IsWalkable == true )
        {
            
           m_Creature.m_CreatureAi.SetGoalPosition(m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y).m_PositionInGrid);
           m_Grid.GetNode(m_Creature.m_CreatureAi.m_InitalPosition.x, m_Creature.m_CreatureAi.m_InitalPosition.y).m_CreatureOnGridPoint = null;
            m_CommandBoardExists = false;
            m_MovementHasBeenCalculated = false;
            m_ToFollowCreature = m_Creature;
            m_Creature = null;
        }
       
    }

    public void CalculateCreaturesPath(Creatures aCreature)
    {
        if (aCreature.m_CreatureAi.m_HasMovedForThisTurn == true)
        {
            return;
        }


        if (m_CommandBoardExists == false)
        {
            
            
            m_Grid.SetHeuristicToZero();
            m_Grid.RemoveWalkableArea();
            
            aCreature.m_CreatureAi.FindAllPaths();
        }
    }

    public void ReturnPlayerToInitalPosition()
    {
        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
        {
            AiController TempAiController = m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi;
            //Checking to see if he has moved and if he hasnt attacked yet
            if (TempAiController.m_HasMovedForThisTurn == true && TempAiController.m_HasAttackedForThisTurn == false
                && m_Grid.GetNode(TempAiController.m_InitalPosition.x, TempAiController.m_InitalPosition.y).m_CreatureOnGridPoint == null)
            {
                //return the player to the original position
                m_NodeTheCameraIsOn.
                     m_CreatureOnGridPoint.m_CreatureAi.ReturnToInitalPosition();
            }


        }
    }

    public void CreateCommandBoard()
    {
        if (m_cameraState != CameraState.Normal)
        {
            return;
        }

        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint == null)
        {
            return;
        }
        
        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi.m_HasAttackedForThisTurn == true 
            && m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi.m_HasMovedForThisTurn == true)
        {
            return;
        }

        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Enemy)
        {
            return;
        }
        //Get creature on that point on the grid
        
        
        m_Creature =
             m_NodeTheCameraIsOn.m_CreatureOnGridPoint;
         
         

         //Push Screen
         GameManager.Instance.UiManager.PushScreen(UiManager.Screen.CommandBoard);

         //Get Screen
         UiScreenCommandBoard ScreenTemp = GameManager.Instance.UiManager.GetScreen(UiManager.Screen.CommandBoard) 
             as UiScreenCommandBoard;

         //Set Screen Variables
         ScreenTemp.SetCreatureReference(m_Creature);
         m_CommandBoardExists = true;
    }
    
}



