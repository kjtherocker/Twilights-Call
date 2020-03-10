using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine.Assertions;

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
    public Creatures m_ToFollowCreature;
    public CombatNode m_NodeTheCameraIsOn;

    public Vector2Int m_CameraPositionInGrid;



    public CombatCameraWrapper m_CombatCameraWrapper;

    public void InitalizeCamera()
    {
        InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => DPadGridControls(movement.ReadValue<Vector2>());
        m_CameraPositionInGrid = new Vector2Int(5, 5);
        GameManager.Instance.m_BattleCamera = this;

        m_Grid = Grid.Instance;
        
        if (m_NodeTheCameraIsOn != null)
        {
            m_NodeTheCameraIsOn = Grid.Instance.GetNode(m_CameraPositionInGrid.x, m_CameraPositionInGrid.y);
        }

        
        Debug.Log("Initialized Camera");

        m_cameraState = CameraState.Normal;

        m_CombatCameraWrapper = GetComponent<CombatCameraWrapper>();

        m_CombatCameraWrapper.Initalize(m_NodeTheCameraIsOn);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
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
                   
                   m_NodeTheCameraIsOn = m_NodeTheCameraIsOn = m_Grid.GetNode(m_ToFollowCreature.m_CreatureAi.m_Position.x, m_ToFollowCreature.m_CreatureAi.m_Position.y);
                    //m_Grid.GetNode(m_ToFollowCreature.m_CreatureAi.m_Position.x, m_ToFollowCreature.m_CreatureAi.m_Position.y);
                   
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


    public void MoveCamera(CameraMovementDirections cameraMovementDirections )
    {

        Vector2Int TempInitalCameraPostion = m_CameraPositionInGrid;
        

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
            m_CombatCameraWrapper.CameraStateChanged(m_NodeTheCameraIsOn);
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



