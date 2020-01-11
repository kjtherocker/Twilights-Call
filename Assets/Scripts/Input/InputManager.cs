using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : Singleton<InputManager>
{
    public PlayerInput m_MovementControls;


    // Use this for initialization
    void Awake()
    {
        m_MovementControls = new PlayerInput();
        m_MovementControls.Enable();

      
    }


}
