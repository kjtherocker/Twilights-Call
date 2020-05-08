using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraUiLayer : MonoBehaviour
{
    
    public CombatCameraController.CameraState m_cameraState;
    private List<Vector2Int> m_SpellAttackFormations;
    
    private Grid m_Grid;
    public CombatNode m_NodeTheCameraIsOn;
    
    public HealthBar m_PlayerStatusSheet;
    public HealthBar m_EnemyStatusSheet;

    public UiDomainStatus m_DomainTab;
    
    
    private Vector2Int m_CameraPositionInGrid;
    public GameObject m_Selector;
    
    public TextMeshProUGUI m_NodePositionText;
    public TextMeshProUGUI m_NodeType;
    public TextMeshProUGUI m_NodeProp;
    public TextMeshProUGUI m_NodeHeuristic;

    public CombatInputLayer m_CombatInput;
    
    private bool m_CommandBoardExists;
    private bool m_MovementHasBeenCalculated;
    public void Initalize(CombatNode aDefaultPosition, CombatInputLayer aCombatInput)
    {
        m_NodeTheCameraIsOn = aDefaultPosition;

        m_CombatInput = aCombatInput;
        
      // m_Selector.gameObject.transform.position =
      //         new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f,
      //             m_NodeTheCameraIsOn.transform.position.z);
        
        m_CommandBoardExists = false;

        m_Grid = Grid.Instance;
    }
    
    public void CameraStateChanged(CombatNode aCombatNode )
    {

        if (aCombatNode == null)
        {
            Debug.Log("Camera is giving the ui layer a null Combatnode");
            return;
        }

        m_CombatInput.CameraStateChanged(aCombatNode);
        
        m_CombatInput.DesetAttackTile();
        
        m_NodeTheCameraIsOn = aCombatNode;
        m_CombatInput.m_CameraPositionInGrid = aCombatNode.m_PositionInGrid;
        m_CameraPositionInGrid = aCombatNode.m_PositionInGrid;
        
        
        m_Selector.gameObject.transform.position =
            new Vector3(m_NodeTheCameraIsOn.transform.position.x, m_NodeTheCameraIsOn.transform.position.y + Constants.Constants.m_HeightOffTheGrid + 0.8f,
                m_NodeTheCameraIsOn.transform.position.z);


        HandleStatus();
        HandleDomainStatus();
        
        m_CombatInput.SetAttackTile();
        DebugLogs();
    }

    public void DebugLogs()
    {
        m_NodePositionText.text = "Postion " + m_NodeTheCameraIsOn.m_PositionInGrid.ToString();
        m_NodeType.text = "Type " + m_NodeTheCameraIsOn.m_CombatsNodeType.ToString();
        m_NodeProp.text = "Prop " + m_NodeTheCameraIsOn.m_PropOnNode.ToString();
        m_NodeHeuristic.text = "Heuristic " + m_NodeTheCameraIsOn.m_Heuristic.ToString();
       //  m_NodeHeuristic.text = "IsWalkable " + m_NodeTheCameraIsOn.m_IsWalkable.ToString();     

    }

    public void HandleStatus()
    {

            if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint != null)
            {
                if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Ally)
                {
                    

                    m_PlayerStatusSheet.gameObject.SetActive(true);
                    m_EnemyStatusSheet.gameObject.SetActive(false);
                    if (m_PlayerStatusSheet.Partymember != m_NodeTheCameraIsOn.m_CreatureOnGridPoint)
                    {
                        m_PlayerStatusSheet.SetCharacter(m_NodeTheCameraIsOn.m_CreatureOnGridPoint);
                    }
                }
                else if (m_NodeTheCameraIsOn.m_CreatureOnGridPoint.charactertype == Creatures.Charactertype.Enemy)
                {
                    
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


 
    public void HandleDomainStatus()
    {



        if (m_NodeTheCameraIsOn.m_DomainCombatNode != CombatNode.DomainCombatNode.Domain)
        {
            m_DomainTab.gameObject.SetActive(false);
        }
        else
        {
            m_DomainTab.SetDomainReference(m_NodeTheCameraIsOn.DomainOnNode);
            m_DomainTab.gameObject.SetActive(true);
        }

        
    }



}
