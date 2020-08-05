using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeCombatArena : MonoBehaviour
{
    public GridFormations m_CurrentLevel;
    public CombatCameraController m_BattleCamera;
    public TextAsset TextAsset;

    public void Start()
    {
        #if UNITY_EDITOR
        SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
        
        #endif
        StartCoroutine(Initialize());
    }

    
    public IEnumerator Initialize()
    {
        
        yield return new WaitForEndOfFrame();
        
        yield return new WaitUntil(() => Preloader.Instance.m_InitializationSteps == Preloader.InitializationSteps.Finished);

        //DialogueManager.instance.StartDialogue(TextAsset,DialogueManager.DialogueType.DialogueBox);
        
        CombatManager.Instance.CombatStart(m_CurrentLevel);
        m_BattleCamera.InitalizeCamera(); 
    }

}
