using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {

        SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
        

        StartCoroutine(Initialize());
    }

    
    public IEnumerator Initialize()
    {
        
        yield return new WaitForEndOfFrame();
        
        yield return new WaitUntil(() => Preloader.Instance.m_InitializationSteps == Preloader.InitializationSteps.Finished);
        
        
        UiManager.instance.PushScreen(UiManager.Screen.MainMenu);
    }
}
