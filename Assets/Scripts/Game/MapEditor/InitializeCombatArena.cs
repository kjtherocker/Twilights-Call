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
    public bool PreloadScene = false;
    public void Start()
    {
#if UNITY_EDITOR

        if (PreloadScene == true)
        {
            SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
        }
#endif
        
        StartCoroutine(Initialize());
    }

    
    public IEnumerator Initialize()
    {
        
        yield return new WaitForEndOfFrame();
        
        yield return new WaitUntil(() => Preloader.Instance.m_InitializationSteps == Preloader.InitializationSteps.Finished);

        
        Debug.Log("Preload Is Initialized");
    
        TacticsManager.Instance.CombatStart(m_CurrentLevel);
        m_BattleCamera.InitalizeCamera(); 
    }

}
