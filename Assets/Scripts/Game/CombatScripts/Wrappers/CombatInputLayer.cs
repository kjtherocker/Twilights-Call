using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInputLayer
{

    public enum CombatInputState
    {
        Default,
        Commandboard,
        Walk,
        Attack,
        Domain,
        Devour
    }

    private List<Vector2Int> m_SpellAttackFormations;
    
    private Grid m_Grid;
    private Creatures m_Creature;
    public CombatNode m_NodeTheCameraIsOn;
    
    private Skills m_CreatureAttackingSkill;

    
    public Vector2Int m_CameraPositionInGrid;


    private CameraUiLayer m_CameraUiLayer;
    private CombatCameraController m_CombatCameraController;
    
    private bool m_CommandBoardExists;
    private bool m_MovementHasBeenCalculated;

    public CombatInputState m_CombatInputState;
    
    public void Initialize(CameraUiLayer aCameraUiLayer, CombatCameraController aCombatCameraController)
    {
        m_CameraUiLayer = aCameraUiLayer;
        m_CombatCameraController = aCombatCameraController;

        m_Grid = Grid.Instance;

        m_CombatInputState = CombatInputState.Default;
        
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => CreateCommandBoard();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => PlayerWalk();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => AttackingIndividual();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => ActivatedDevour();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => ActivatedDomain();
        
        InputManager.Instance.m_MovementControls.Player.SquareButton.performed += SquareButton => ReturnToCommandboard();
    }

    public void ReturnToCommandboard()
    {
      //  if (m_CombatInputState != CombatInputState.Attack)
      //  {
      //      return;
      //  }
//
      //  m_CombatInputState = CombatInputState.Commandboard;
      //  GameManager.Instance.m_UiManager.ReturnToLastScreen();
    }
    
    public void CreateCommandBoard()
    {
        if (m_CombatInputState != CombatInputState.Default)
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
        UiManager.Instance.PushScreen(UiManager.Screen.CommandBoard);

        m_CombatInputState = CombatInputState.Commandboard;
        
        //Get Screen
        UiScreenCommandBoard ScreenTemp = UiManager.Instance.GetScreen(UiManager.Screen.CommandBoard) 
            as UiScreenCommandBoard;

        //Set Screen Variables
        ScreenTemp.SetCreatureReference(m_Creature);
        m_CommandBoardExists = true;
    }

    public void CameraStateChanged(CombatNode aCombatnode)
    {
        
        m_NodeTheCameraIsOn = aCombatnode;

        CalculateCreaturesWalkableTerrain();

    }

    public void CalculateCreaturesWalkableTerrain()
    {
        if (m_CombatInputState != CombatInputState.Default)
        {
            return;
        }
        
        
        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint == null)
        {
            m_Grid.SetHeuristicToZero();
            m_Grid.RemoveWalkableArea();
            return;
        }

        
        
   
        if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.GetCharactertype() == Creatures.Charactertype.Ally)
        {
            CalculateCreaturesPath(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
        }
        else if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.GetCharactertype() == Creatures.Charactertype.Enemy)
        {
            CalculateCreaturesPath(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
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
    
    public void PlayerWalk()
    {

        if (m_CombatInputState != CombatInputState.Walk)
        {
            return;
        }

        if (m_NodeTheCameraIsOn.m_IsWalkable == false)
        {
            return;
        }

        m_CombatCameraController.m_ToFollowCreature = m_Creature;
        m_Creature.m_CreatureAi.SetGoalPosition(m_NodeTheCameraIsOn.m_PositionInGrid);
        m_Grid.GetNode(m_Creature.m_CreatureAi.m_InitalPosition.x, m_Creature.m_CreatureAi.m_InitalPosition.y).m_CreatureOnGridPoint = null;
        m_CommandBoardExists = false;
        m_CombatInputState = CombatInputState.Default;
        m_Creature = null;
        
       
    }

    public void SkillUsed(int ASpellNodePositionX, int ASpellNodePositionY)
    {
        TacticsManager.Instance.InvokeSkill
            (m_CreatureAttackingSkill.UseSkill(m_Grid.GetNode(ASpellNodePositionX, ASpellNodePositionY).m_CreatureOnGridPoint ,m_Creature));
    }

    public void AttackingIndividual()
    {
        if (m_CombatInputState != CombatInputState.Attack)
        {
            return;
        }

        if (m_SpellAttackFormations != null)
        {
            for (int i = 0; i < m_SpellAttackFormations.Count; i++)
            {
                Vector2Int TempSpellNodePosition = new Vector2Int(
                    m_NodeTheCameraIsOn.m_PositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_NodeTheCameraIsOn.m_PositionInGrid.y + m_SpellAttackFormations[i].y);

                if (!m_CombatCameraController.CheckingGridDimensionBoundrys(TempSpellNodePosition))
                {
                    continue;
                }

                if (m_Grid.GetNode(TempSpellNodePosition.x, TempSpellNodePosition.y).m_CreatureOnGridPoint == null)
                {
                    continue;
                }

                    
                SkillUsed(TempSpellNodePosition.x, TempSpellNodePosition.y);
            }
        }

        DesetAttackTile();

        m_SpellAttackFormations = null;
        m_Creature.m_CreatureAi.m_HasAttackedForThisTurn = true;
        m_CombatInputState = CombatInputState.Default;
        m_CommandBoardExists = false;
    }

    
    public void SetAttackTile()
    {
        if (m_SpellAttackFormations == null)
        {
            return;
        }

        for (int i = 0; i < m_SpellAttackFormations.Count; i++)
        {
            Vector2Int TempSpellPosition = new Vector2Int(m_NodeTheCameraIsOn.m_PositionInGrid.x + m_SpellAttackFormations[i].x,
                m_NodeTheCameraIsOn.m_PositionInGrid.y + m_SpellAttackFormations[i].y);

            if (m_Grid.CheckingGridDimensionBoundrys(TempSpellPosition))
            {
                m_Grid.SetAttackingTileInGrid(new Vector2Int(m_NodeTheCameraIsOn.m_PositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_NodeTheCameraIsOn.m_PositionInGrid.y + m_SpellAttackFormations[i].y));
            }
        }
    }

    
    
    public void DesetAttackTile()
    {
        if (m_SpellAttackFormations == null)
        {
            return;
        }

        if (m_SpellAttackFormations != null)
        {
            for (int i = 0; i < m_SpellAttackFormations.Count; i++)
            {
                Vector2Int TempSpellPosition = new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

                if (m_Grid.CheckingGridDimensionBoundrys(TempSpellPosition))
                {
                    m_Grid.DeselectAttackingTileingrid(new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                        m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y));
                }
            }
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
            
            Debug.Log("Calculating paths boyz");
            aCreature.m_CreatureAi.FindAllPaths();
        }
    }
    
    public void SetAttackPhase(Skills aSkill)
    {
        m_CreatureAttackingSkill = aSkill;
        m_SpellAttackFormations = GameManager.Instance.m_NodeFormation.NodeFormation();
        m_CombatInputState = CombatInputState.Attack;
        
        for (int i = 0; i < m_SpellAttackFormations.Count; i++)
        {
            Vector2Int TempSpellPosition = new Vector2Int(m_NodeTheCameraIsOn.m_PositionInGrid.x + m_SpellAttackFormations[i].x,
                m_NodeTheCameraIsOn.m_PositionInGrid.y + m_SpellAttackFormations[i].y);

            if (m_CombatCameraController.CheckingGridDimensionBoundrys(TempSpellPosition))
            {
                m_Grid.SetAttackingTileInGrid(new Vector2Int(m_NodeTheCameraIsOn.m_PositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_NodeTheCameraIsOn.m_PositionInGrid.y + m_SpellAttackFormations[i].y));
            }
        }
    }
    
    public void SetDomainPhase(int aRange)
    {
        m_CombatInputState = CombatInputState.Domain;
        m_Creature.m_CreatureAi.SetDomain(aRange);
    }

    public void ActivatedDomain()
    {
        if (m_CombatInputState != CombatInputState.Domain)
        {
            return;
        }
        UiManager.Instance.PopScreen();
        m_Creature.m_CreatureAi.ActivateDomain();
        m_CombatInputState = CombatInputState.Default;
        m_CommandBoardExists = false;
        m_CameraUiLayer.CameraStateChanged(m_NodeTheCameraIsOn);
        
        InputManager.Instance.m_MovementControls.Enable();
    }
    
    
    public void SetDevourPhase(int aRange)
    {
        m_Creature.m_CreatureAi.SetDevour(aRange);
        m_CombatInputState = CombatInputState.Devour;
    }

    public void ActivatedDevour()
    {

        if (m_CombatInputState != CombatInputState.Devour)
        {
            return;
        }
        UiManager.Instance.PopScreen();
        m_Creature.m_CreatureAi.ActivateDevour();
        m_CombatInputState = CombatInputState.Default;
        m_CommandBoardExists = false;
        m_CameraUiLayer.CameraStateChanged(m_NodeTheCameraIsOn);
        InputManager.Instance.m_MovementControls.Enable();
    }




}
