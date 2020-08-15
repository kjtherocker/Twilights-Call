﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldCamera : MonoBehaviour
{
    public bool PreloadScene = false;
    void Start()
    {

#if UNITY_EDITOR

        if (PreloadScene == true)
        {
            SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
        }
#endif
        StartCoroutine(Testo());
    }

    public IEnumerator Testo()
    {
        
        yield return new WaitForSeconds(0.1f);
        UiManager.Instance.PushScreen(UiManager.Screen.ArenaMenu);
        
    }

}
