using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class OverWorldPlayer : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    public BaseInput m_BaseMovementControls;

    public Animator m_PlayerAnimatior;

    public float Player_Speed = 5;
    public float m_PlayerRotationSpeed;
    public bool m_IsInMenu;
    private float Player_Speed_Delta;

    
    private bool Player_Movment;
    float m_Gravity;
    float mass = 3.0f;
    Vector3 m_Velocity = Vector3.zero;

    public Vector2 MoveDirection;
    
    void Start ()
    {

        m_PlayerAnimatior = GetComponentInChildren<Animator>();
        
        m_BaseMovementControls = new BaseInput();
       m_BaseMovementControls.Enable();
       m_BaseMovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());
       m_BaseMovementControls.Player.Movement.canceled += movement => PlayerMovement(movement.ReadValue<Vector2>());
   //    InputManager.Instance.m_MovementControls.Player.Movement.performed += movement => PlayerMovement(movement.ReadValue<Vector2>());

    }

    public void PlayerMovement(Vector2 aDirection)
    {
       

        if (aDirection == Vector2.zero)
        {
            m_PlayerAnimatior.SetBool("b_IsWalking", false);
            MoveDirection = aDirection;
            return;
        }
        else
        {
            m_PlayerAnimatior.SetBool("b_IsWalking", true); 
        }
        
        MoveDirection = aDirection;
        Vector3 NextRotation = new Vector3(MoveDirection.x, 0, MoveDirection.y);
        
        float SpeedUpdate = m_PlayerRotationSpeed * Time.deltaTime;

        transform.rotation =  Quaternion.LookRotation( NextRotation,Vector3.up );

        
    }


    // Update is called once per frame
	void FixedUpdate ()
    {

        if (m_IsInMenu == true)
        {
            m_PlayerAnimatior.SetBool("b_IsWalking", false);
            MoveDirection = Vector2.zero;
            return;
        }
        
        float SpeedUpdate = Player_Speed * Time.deltaTime;
        
        m_Velocity = (new Vector3(MoveDirection.x ,0  ,MoveDirection.y )* SpeedUpdate );



        transform.position =  gameObject.transform.position + m_Velocity;


    }
    
}

