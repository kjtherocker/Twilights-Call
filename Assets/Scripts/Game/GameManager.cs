using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public TacticsCameraController m_TacticsCameraController;
    public TacticsCameraController MTacticsCameraController { get { return m_TacticsCameraController; } }

    public MovementList m_MovementList;
    public SkillList m_SkillList;
    public NodeFormations m_NodeFormation;
    public NameGenerator m_NameGenerator;

    public PropList m_PropList;

    // Use this for initialization


    public void Initialize()
    {
        m_MovementList = new MovementList();
        m_MovementList.Initialize();
        
        m_SkillList = new SkillList();
        m_SkillList.Initialize();


        m_NameGenerator = new NameGenerator();
        m_NameGenerator.Initialize();
        
        m_NodeFormation = new NodeFormations();
        

        QualitySettings.vSyncCount = 1;
        Physics.autoSimulation = false;


    }

}
