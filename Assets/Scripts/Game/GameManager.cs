using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public GameObject Overworld_Objects;
    public GameObject Combat_Objects;
    public PartyManager m_PartyManager;
    public PartyManager PartyManager { get { return m_PartyManager; } }

    public CombatManager m_CombatManager;
    public CombatManager CombatManager { get { return m_CombatManager; } }
    public CombatCameraController m_BattleCamera;
    public CombatCameraController BattleCamera { get { return m_BattleCamera; } }

    public UiManager m_UiManager;
    public UiManager UiManager { get { return m_UiManager; } }


    
    
    public Grid m_Grid;
    public Grid Grid { get { return m_Grid; } }

    


    public MovementList m_MovementList;
    public SkillList m_SkillList;
    public NodeFormations m_NodeFormation;
    public NameGenerator m_NameGenerator;

    public PropList m_PropList;


    public enum GameStates
    {
        Overworld,
        Combat

    }

    public GameStates m_GameStates;

    // Use this for initialization


    private void Awake()
    {
        m_MovementList = new MovementList();
        m_MovementList.Initialize();
        
        m_SkillList = new SkillList();
        m_SkillList.Initialize();


        m_NameGenerator = new NameGenerator();
        m_NameGenerator.Initialize();
        
        m_NodeFormation = new NodeFormations();
        
 

        m_GameStates = GameStates.Overworld;
        if (m_PartyManager == null)
        {
            m_PartyManager = GameObject.Find("PartyManager").GetComponent<PartyManager>();
        }


        if (m_CombatManager == null)
        {
            m_CombatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        }
        
        QualitySettings.vSyncCount = 1;
        Physics.autoSimulation = false;
        

        
        
        SwitchToOverworld();
    }

    public PropList InitializePropList()
    {
        m_PropList = new PropList();
        m_PropList.Initialize();

        return m_PropList;
    }

    public void SwitchToOverworld()
    {
        m_GameStates = GameStates.Overworld;
        Overworld_Objects.SetActive(true);
        Combat_Objects.SetActive(false);
    }

    public void SwitchToBattle()
    {
        m_GameStates = GameStates.Combat;
        m_CombatManager.CombatStart();
        Overworld_Objects.SetActive(false);
        Combat_Objects.SetActive(true);
    }
}
