﻿using System.Collections;
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
        GameManager.Instance.m_InputManager.m_MovementControls.Player.Movement.performed += movement => DPadGridControls(movement.ReadValue<Vector2>());
        GameManager.Instance.m_InputManager.m_MovementControls.Player.XButton.performed += XButton => CreateCommandBoard();
        GameManager.Instance.m_InputManager.m_MovementControls.Player.XButton.performed += XButton => PlayerWalk();

        m_CameraPositionInGrid = new Vector2Int(5, 5);
        GameManager.Instance.m_BattleCamera = this;

        if (m_NodeTheCameraIsOn != null)
        {
            m_Selector.gameObject.transform.position =
                new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f, m_NodeTheCameraIsOn.transform.position.z);
            m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
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




    public void CameraMovement()
    {



        switch (m_cameraState)
        {
            case CameraState.Nothing:


                break;

            case CameraState.Normal:
                if (m_Grid.m_GridPathArray != null)
                {
                    m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
                    
                    transform.position = Vector3.Lerp(transform.position, new Vector3(
                        m_NodeTheCameraIsOn.transform.position.x + 18.5f,
                        m_NodeTheCameraIsOn.transform.position.y + 18.9f,
                        m_NodeTheCameraIsOn.transform.position.z - 18.5f), Time.deltaTime * 2);
                }

                if (GameManager.Instance.UiManager.GetScreen(UiManager.Screen.CommandBoard) == null)
                {
                   // GameManager.Instance.m_Controls.Player.Movement.performed += movement => DPadGridControls(movement.ReadValue<Vector2>()); 
                }
             


                PlayerUiSelection();

                break;

            case CameraState.PlayerMovement:
                //isPlayersDoneMoving();
               if (m_Grid.m_GridPathArray != null)
               {
                   m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
               
                   transform.position = Vector3.Lerp(transform.position, new Vector3(
                       m_Creature.ModelInGame.transform.position.x + 13.5f,
                       m_Creature.ModelInGame.transform.position.y + 13.9f,
                       m_Creature.ModelInGame.transform.position.z - 13.5f), Time.deltaTime * 2);
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

                

                if (Input.GetButtonDown("Ps4_Cross"))
                {
                    AttackingIndividual();
                }


                break;



            case CameraState.EnemyMovement:
                if (m_Grid.m_GridPathArray != null)
                {
                    m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];

                    transform.position = Vector3.Lerp(transform.position, new Vector3(
                        m_Creature.ModelInGame.transform.position.x + 13.5f,
                        m_Creature.ModelInGame.transform.position.y + 13.9f,
                        m_Creature.ModelInGame.transform.position.z - 13.5f), Time.deltaTime * 2);
                }

                break;

            case CameraState.EnemyAttack:


                break;
        }


      
      

        if (m_NodeTheCameraIsOn != null)
        {
            if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
            {
                if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Ally)
                {
                    m_StatusSheet.gameObject.SetActive(true);
                    m_PartyStatus.gameObject.SetActive(true);
                    if (m_StatusSheet.Partymember != m_NodeTheCameraIsOn.m_CreatureOnGridPoint)
                    {
                        m_StatusSheet.SetCharacter(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    }
                }
                else if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Enemy)
                {
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
            m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
        }
        else
        {
            m_CameraPositionInGrid = TempInitalCameraPostion;
            m_NodeTheCameraIsOn = m_Grid.m_GridPathArray[m_CameraPositionInGrid.x, m_CameraPositionInGrid.y];
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
        m_Creature.m_CreatureAi.m_CreaturesAnimator.SetTrigger("t_IsAttack");
        if (m_SpellAttackFormations != null)
        {
            for (int i = 0; i < m_SpellAttackFormations.Count; i++)
            {
                Vector2Int TempSpellNodePosition = new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

                if (CheckingGridDimensionBoundrys(TempSpellNodePosition))
                {
                    if (m_Grid.m_GridPathArray[TempSpellNodePosition.x, TempSpellNodePosition.y].m_CreatureOnGridPoint != null)
                    {
                        StartCoroutine(m_Grid.m_GridPathArray[TempSpellNodePosition.x, TempSpellNodePosition.y].m_CreatureOnGridPoint.DecrementHealth
                           (m_CreatureAttackingSkill.GetSkillDamage() + m_Creature.GetAllStrength(), m_CreatureAttackingSkill.GetElementalType(), 0.1f, 0.1f, 1));
                    }
                }
            }
        }
        m_PlayerIsAttacking = false;
        GameManager.Instance.UiManager.PopAllScreens();
    }

    public void PlayerUiSelection()
    {
    }

    public void PlayerWalk()
    {
        if (m_NodeTheCameraIsOn.m_IsWalkable == true )
        {

           StartCoroutine(m_Creature.m_CreatureAi.SetGoalPosition(m_Grid.m_GridPathArray[m_CameraPositionInGrid.x,m_CameraPositionInGrid.y].m_PositionInGrid));
            m_Grid.m_GridPathArray[m_Creature.m_CreatureAi.m_InitalPosition.x, m_Creature.m_CreatureAi.m_InitalPosition.y].m_CreatureOnGridPoint = null;
            m_CommandBoardExists = false;
            m_MovementHasBeenCalculated = false;
        }
       
    }

    public void ReturnPlayerToInitalPosition()
    {
        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
        {
            AiController TempAiController = m_NodeTheCameraIsOn.m_CreatureOnGridPoint.m_CreatureAi;
            //Checking to see if he has moved and if he hasnt attacked yet
            if (TempAiController.m_HasMovedForThisTurn == true && TempAiController.m_HasAttackedForThisTurn == false
                && m_Grid.m_GridPathArray[TempAiController.m_InitalPosition.x, TempAiController.m_InitalPosition.y].m_CreatureOnGridPoint == null)
            {
                //return the player to the original position
                m_NodeTheCameraIsOn.
                     m_CreatureOnGridPoint.m_CreatureAi.ReturnToInitalPosition();
            }


        }
    }

    public void CreateCommandBoard()
    {

        if (m_MovementHasBeenCalculated == false)
        {
            if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
            {

                //Get creature on that point on the grid
                m_Creature =
                    m_NodeTheCameraIsOn.m_CreatureOnGridPoint;

                //Push Screen
                GameManager.Instance.UiManager.PushScreen(UiManager.Screen.CommandBoard);

                //Get Screen
                UiScreenCommandBoard ScreenTemp =
                    GameManager.Instance.UiManager.GetScreen(UiManager.Screen.CommandBoard) as UiScreenCommandBoard;

                //Set Screen Variables
                ScreenTemp.SetCreatureReference(m_Creature);
                m_CommandBoardExists = true;
            }
        }
    }
    
}


