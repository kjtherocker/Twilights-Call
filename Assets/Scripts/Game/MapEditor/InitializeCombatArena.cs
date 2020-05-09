using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializeCombatArena : MonoBehaviour
{
    public GridFormations m_CurrentLevel;

    public CombatCameraController m_BattleCamera;


    public void Start()
    {
        #if UNITY_EDITOR
        SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
        
        #endif
        StartCoroutine(Initialize());
    }

    
    public IEnumerator Initialize()
    {
        
        yield return new WaitForSeconds(0.6f);
       
        CombatManager.Instance.CombatStart(m_CurrentLevel);
        m_BattleCamera.InitalizeCamera(); 
    }

}
