using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public PartyManager m_PartyManager;
    public PartyManager PartyManager { get { return m_PartyManager; } }
    
    public CombatCameraController m_BattleCamera;
    public CombatCameraController BattleCamera { get { return m_BattleCamera; } }

    public UiManager mUiManager;
    public UiManager UiManager { get { return mUiManager; } }


    
    
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

        QualitySettings.vSyncCount = 1;
        Physics.autoSimulation = false;


    }

    public PropList InitializePropList()
    {
        m_PropList = new PropList();
        m_PropList.Initialize();

        return m_PropList;
    }




}
