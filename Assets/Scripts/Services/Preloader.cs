using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloader : Singleton<Preloader>
{

    public enum InitializationSteps
    {
        GameManager,
        PartyManager,
        Ui,
        Input,
        Finished
        
    }


    // Start is called before the first frame update

     public InitializationSteps m_InitializationSteps;
     public Camera m_TestCamera;

     
    void Start()
    {
        InitializePreloadObjects();
    }

    public void InitializePreloadObjects()
    {
       // m_TestCamera.gameObject.SetActive(false);
        
        
        m_InitializationSteps = InitializationSteps.GameManager;
        GameManager.Instance.Initialize();

        m_InitializationSteps = InitializationSteps.Ui;
        UiManager.Instance.Initialize();


        if (InputManager.Instance == null)
        {
            gameObject.GetComponentInChildren<InputManager>().Initialize();
        }
        else
        {
            InputManager.instance.Initialize();
        }

        
     // switch (m_InitializationSteps)
     // {
     //     case InitializationSteps.Input:
     //     {

     //         m_InitializationSteps = InitializationSteps.PartyManager;

     //     }
     //         break;

     //     case InitializationSteps.PartyManager:
     //     {

     //         PartyManager.instance.Initialize();
     //         m_InitializationSteps = InitializationSteps.Finished;
     //     }
     //         break;

     //     case InitializationSteps.Finished:
     //     {


     //     }
     //         break;
     // }

        m_InitializationSteps = InitializationSteps.Input;
        
        
        m_InitializationSteps = InitializationSteps.PartyManager;
        PartyManager.instance.Initialize();
        
        m_InitializationSteps = InitializationSteps.Finished;

    }
}
