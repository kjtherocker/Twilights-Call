using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Use this for initialization
    public Animator m_HeartAnimator;
    public GameObject m_Cutscene;

    public void Playgame()
    {
        m_HeartAnimator.SetBool("PlayButton", true);

    }

    public void Update()
    {
        if(m_HeartAnimator.GetCurrentAnimatorStateInfo(0).IsName("PatchworkHeartFail") && 
           m_HeartAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            SceneManager.LoadScene(1);
        }
    }

 

    public void ExitGame()
    {
        Application.Quit();
    }
} 
