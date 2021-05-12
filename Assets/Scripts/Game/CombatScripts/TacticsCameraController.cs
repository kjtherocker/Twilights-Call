using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class CameraStates
{
    public Vector3 m_CameraPostion;
    public Vector3 m_CameraRotation;
}

public class TacticsCameraController : MonoBehaviour
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

    public enum CameraRotations
    {
        Up,
        Right,
        Down,
        Left,
    }
    
    public enum CameraMovementDirections
    {
        Up,
        Down,
        Left,
        Right
    }

    public CameraState m_cameraState;

    private Grid m_Grid;
    public Creatures m_ToFollowCreature;
    public CombatNode m_NodeTheCameraIsOn;

    public Vector2Int m_CameraPositionInGrid;

    public bool MovementInverted = true;

    public CameraUiLayer m_CameraUiLayer;
    public CombatInputLayer m_CombatInputLayer;
    public Vector3 m_CameraOffset;

    public CameraRotations m_CurrentRotation;
    public Dictionary<CameraRotations, CameraStates> m_CameraRotations;

    private float m_CameraSpeed;
    
    
    public void InitalizeCamera()
    {
        //Initializing Variables
        m_CameraPositionInGrid = new Vector2Int(9, 9);
        m_CameraSpeed = Constants.Helpers.m_TacticsCameraSpeed;
        
        
        //Setting input
        InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => DPadGridControls(movement.ReadValue<Vector2>());
        
        //Setting Depedencys 
        GameManager.Instance.m_TacticsCameraController = this;
        m_Grid = Grid.Instance;

        
        
        //Setting The Cameras Rotation
        Vector3 cameraDistance = Constants.Helpers.m_CameraCloseDistance;
        Vector3 cameraAngle = Constants.Helpers.m_CameraAngle;
        
        m_CurrentRotation = CameraRotations.Down;
        m_CameraRotations = new Dictionary<CameraRotations, CameraStates>();

       
        
        AddToCameraRotations(CameraRotations.Down, new Vector3(cameraDistance.x, cameraDistance.y, cameraDistance.z), new Vector3(cameraAngle.x, -cameraAngle.y, cameraAngle.z));
        AddToCameraRotations(CameraRotations.Left, new Vector3(-cameraDistance.x, cameraDistance.y, cameraDistance.z), new Vector3(cameraAngle.x, cameraAngle.y, cameraAngle.z));
        AddToCameraRotations(CameraRotations.Up, new Vector3(cameraDistance.x, cameraDistance.y, cameraDistance.z), new Vector3(-cameraAngle.x, cameraAngle.y, cameraAngle.z));
        
        
        //Final Setup
        if (m_NodeTheCameraIsOn != null)
        {
            m_NodeTheCameraIsOn = Grid.Instance.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y);
        }

        m_cameraState = CameraState.Normal;

        m_CameraUiLayer = GetComponent<CameraUiLayer>();
        m_CombatInputLayer = new CombatInputLayer();
       
        m_CameraUiLayer.Initalize(m_NodeTheCameraIsOn,m_CombatInputLayer);
        m_CombatInputLayer.Initialize(m_CameraUiLayer,this);
        
    }


    public void AddToCameraRotations(CameraRotations aCameraEnum, Vector3 aPosition, Vector3 aRotation)
    {
        CameraStates tempcamerastates = new CameraStates();

        tempcamerastates.m_CameraPostion = aPosition;
        tempcamerastates.m_CameraRotation = aRotation;
        
        m_CameraRotations.Add(aCameraEnum,tempcamerastates );

    }


    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    public void SetCameraState(CameraState aCameraState)
    {
        m_cameraState = aCameraState;
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
                        m_NodeTheCameraIsOn.transform.position.x + m_CameraRotations[m_CurrentRotation].m_CameraPostion.x,
                        m_NodeTheCameraIsOn.transform.position.y + m_CameraRotations[m_CurrentRotation].m_CameraPostion.y,
                        m_NodeTheCameraIsOn.transform.position.z - m_CameraRotations[m_CurrentRotation].m_CameraPostion.z), Time.deltaTime * m_CameraSpeed);
                    
                    Quaternion targetRotation = Quaternion.Euler(m_CameraRotations[m_CurrentRotation].m_CameraRotation);
                    transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * m_CameraSpeed);
                }
                //InputManager.Instance.m_MovementControls.Enable();
                

                break;

            case CameraState.PlayerMovement:

               if (m_Grid.m_GridPathArray != null)
               {
                   
                   //InputManager.Instance.m_MovementControls.Disable();
                   
                   m_NodeTheCameraIsOn = m_Grid.GetNode(m_ToFollowCreature.m_CreatureAi.m_Position.x, m_ToFollowCreature.m_CreatureAi.m_Position.y);
                    //m_Grid.GetNode(m_ToFollowCreature.m_CreatureAi.m_Position.x, m_ToFollowCreature.m_CreatureAi.m_Position.y);
                   
                   transform.position = Vector3.Lerp(transform.position, new Vector3(
                           m_NodeTheCameraIsOn.gameObject.transform.position.x + m_CameraRotations[m_CurrentRotation].m_CameraPostion.x,
                           m_NodeTheCameraIsOn.gameObject.transform.position.y + m_CameraRotations[m_CurrentRotation].m_CameraPostion.y,
                           m_NodeTheCameraIsOn.gameObject.transform.position.z - m_CameraRotations[m_CurrentRotation].m_CameraPostion.z), Time.deltaTime * m_CameraSpeed);
                   

               }


                break;

            case CameraState.PlayerAttack:
               //
               // m_Grid.SetAttackingTileInGrid(m_CameraPositionInGrid);
               // m_Grid.SetAttackingTileInGrid(m_CameraPositionInGrid + new Vector2Int(1,0));

                transform.position = Vector3.Lerp(transform.position, new Vector3(
                        m_NodeTheCameraIsOn.transform.position.x + m_CameraRotations[m_CurrentRotation].m_CameraPostion.x,
                        m_NodeTheCameraIsOn.transform.position.y + m_CameraRotations[m_CurrentRotation].m_CameraPostion.y,
                        m_NodeTheCameraIsOn.transform.position.z - m_CameraRotations[m_CurrentRotation].m_CameraPostion.z), Time.deltaTime * m_CameraSpeed);
               
               

               

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


    public void MoveCamera(CameraMovementDirections cameraMovementDirections )
    {

        Vector2Int TempInitalCameraPostion = Vector2Int.zero;

        
        //Inverted MovementControls
        if (MovementInverted == true)
        {
            if (cameraMovementDirections == CameraMovementDirections.Up)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x - 1, m_CameraPositionInGrid.y);
            }

            if (cameraMovementDirections == CameraMovementDirections.Down)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x + 1, m_CameraPositionInGrid.y);
            }


            if (cameraMovementDirections == CameraMovementDirections.Left)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y - 1);
            }


            if (cameraMovementDirections == CameraMovementDirections.Right)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y + 1);
            }
        }

        //None Inverted Movement Controls
        if (MovementInverted == false)
        {
            if (cameraMovementDirections == CameraMovementDirections.Up)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y + 1);
            }

            if (cameraMovementDirections == CameraMovementDirections.Down)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x , m_CameraPositionInGrid.y - 1);
            }


            if (cameraMovementDirections == CameraMovementDirections.Left)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x - 1, m_CameraPositionInGrid.y);
            }


            if (cameraMovementDirections == CameraMovementDirections.Right)
            {
                TempInitalCameraPostion = new Vector2Int(m_CameraPositionInGrid.x + 1, m_CameraPositionInGrid.y );
            }
        }



        if (CheckingGridDimensionBoundrys(TempInitalCameraPostion))
        {
            m_CameraPositionInGrid = TempInitalCameraPostion;
            m_NodeTheCameraIsOn = m_Grid.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y);;
            m_CameraUiLayer.CameraStateChanged(m_NodeTheCameraIsOn);
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

}