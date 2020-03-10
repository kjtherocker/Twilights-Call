using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CombatCameraWrapper : MonoBehaviour
{
    
    public CombatCameraController.CameraState m_cameraState;
    private List<Vector2Int> m_SpellAttackFormations;
    
    private Grid m_Grid;
    private Creatures m_Creature;
    public CombatNode m_NodeTheCameraIsOn;
    
    private Skills m_CreatureAttackingSkill;

    public HealthBar m_PlayerStatusSheet;
    public HealthBar m_EnemyStatusSheet;

    private Vector2Int m_CameraPositionInGrid;
    public GameObject m_Selector;
    
    public TextMeshProUGUI m_NodePositionText;
    public TextMeshProUGUI m_NodeType;
    public TextMeshProUGUI m_NodeProp;
    public TextMeshProUGUI m_NodeHeuristic;
    
    private bool m_CommandBoardExists;
    private bool m_MovementHasBeenCalculated;
    public void Initalize(CombatNode aDefaultPosition)
    {

        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => CreateCommandBoard();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => PlayerWalk();
        InputManager.Instance.m_MovementControls.Player.XButton.performed += XButton => AttackingIndividual();
        InputManager.Instance.m_MovementControls.Player.SquareButton.performed += SquareButton => ReturnToCommandboard();
        InputManager.Instance.m_MovementControls.Player.TriangleButton.performed += TriangleButton => EndTurn();

        m_NodeTheCameraIsOn = aDefaultPosition;
        
        m_Selector.gameObject.transform.position =
                new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f,
                    m_NodeTheCameraIsOn.transform.position.z);
        
        m_CommandBoardExists = false;

        m_Grid = Grid.Instance;
    }
    
    public void CameraStateChanged(CombatNode aCombatNode )
    {

        if (aCombatNode == null)
        {
            Debug.Log("Camera is giving the wrapper a null Combatnode");
            return;
        }

        m_CameraPositionInGrid = aCombatNode.m_PositionInGrid;
        
        Vector2Int TempInitalCameraPostion = aCombatNode.m_PositionInGrid;
        
        m_CameraPositionInGrid = aCombatNode.m_PositionInGrid;
        
        m_Selector.gameObject.transform.position =
            new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f,
                m_NodeTheCameraIsOn.transform.position.z);

        
        DesetAttackTile();
        SetAttackTile();
        DebugLogs();
    }

    public void DebugLogs()
    {
        m_NodePositionText.text = "Postion " + m_NodeTheCameraIsOn.m_PositionInGrid.ToString();
        m_NodeType.text = "Type " + m_NodeTheCameraIsOn.m_CombatsNodeType.ToString();
        m_NodeProp.text = "Prop " + m_NodeTheCameraIsOn.m_PropOnNode.ToString();
        m_NodeHeuristic.text = "Heuristic " + m_NodeTheCameraIsOn.m_Heuristic.ToString();
        // m_NodeHeuristic.text = "IsWalkable " + m_NodeTheCameraIsOn.m_IsWalkable.ToString();      
    }

    public void HandleStatus()
    {

            if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
            {
                if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Ally)
                {

                    CalculateCreaturesPath(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    
                    m_PlayerStatusSheet.gameObject.SetActive(true);
                    m_EnemyStatusSheet.gameObject.SetActive(false);
                    if (m_PlayerStatusSheet.Partymember != m_NodeTheCameraIsOn.m_CreatureOnGridPoint)
                    {
                        m_PlayerStatusSheet.SetCharacter(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    }
                }
                else if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Enemy)
                {
                    CalculateCreaturesPath(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    m_PlayerStatusSheet.gameObject.SetActive(false);
                    m_EnemyStatusSheet.gameObject.SetActive(true);
                    if (m_EnemyStatusSheet.Partymember != m_NodeTheCameraIsOn.m_CreatureOnGridPoint)
                    {
                        m_EnemyStatusSheet.SetCharacter(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    }
                }

            }
            else
            {
                
                
                
                m_PlayerStatusSheet.gameObject.SetActive(false);
                m_EnemyStatusSheet.gameObject.SetActive(false);

            }
        

    }


    public void ReturnToCommandboard()
    {
        if (m_cameraState != CombatCameraController.CameraState.PlayerAttack)
        {
            return;
        }

        GameManager.Instance.m_UiManager.ReturnToLastScreen();
    }

    public void EndTurn()
    {
        if (m_cameraState != CombatCameraController.CameraState.Normal)
        {
            return;
        }

        StartCoroutine(GameManager.Instance.m_CombatManager.EnemyTurn());

    }
    
     
     public void SetAttackPhase(Skills aSkill)
     {
         m_CreatureAttackingSkill = aSkill;
         m_SpellAttackFormations = GameManager.Instance.m_NodeFormation.NodeFormation();
         m_cameraState = CombatCameraController.CameraState.PlayerAttack;
        
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
         m_cameraState = CombatCameraController.CameraState.PlayerAttack;
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
        if (m_cameraState != CombatCameraController.CameraState.PlayerAttack)
        {
            return;
        }

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
        m_cameraState = CombatCameraController.CameraState.Normal;
    }


    
    public void SetAttackTile()
    {
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
    
    public void DesetAttackTile()
    {
        for (int i = 0; i < m_SpellAttackFormations.Count; i++)
        {
            Vector2Int TempSpellPosition = new Vector2Int(m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y);

            if (CheckingGridDimensionBoundrys(TempSpellPosition))
            {
                m_Grid.DeselectAttackingTileingrid(new Vector2Int(
                    m_CameraPositionInGrid.x + m_SpellAttackFormations[i].x,
                    m_CameraPositionInGrid.y + m_SpellAttackFormations[i].y));
            }
        }

    }


    public void SkillUsed(int ASpellNodePositionX, int ASpellNodePositionY)
    {
        CombatManager.Instance.InvokeSkill
           (m_CreatureAttackingSkill.UseSkill(m_Grid.GetNode(ASpellNodePositionX, ASpellNodePositionY).m_CreatureOnGridPoint ,m_Creature));
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


        if (m_CommandBoardExists == true)
        {
            return;
        }

        if (m_NodeTheCameraIsOn.m_IsWalkable == true )
        {
            
           m_Creature.m_CreatureAi.SetGoalPosition(m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y).m_PositionInGrid);
           m_Grid.GetNode(m_Creature.m_CreatureAi.m_InitalPosition.x, m_Creature.m_CreatureAi.m_InitalPosition.y).m_CreatureOnGridPoint = null;
           m_CommandBoardExists = false;
           m_MovementHasBeenCalculated = true;
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
        if (m_cameraState != CombatCameraController.CameraState.Normal)
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
