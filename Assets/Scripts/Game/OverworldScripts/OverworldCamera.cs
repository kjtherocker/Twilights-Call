using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldCamera : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("PreloadScene", LoadSceneMode.Additive);
        StartCoroutine(Testo());
    }

    public IEnumerator Testo()
    {
        
        yield return new WaitForSeconds(0.1f);
        UiManager.Instance.PushScreen(UiManager.Screen.ArenaMenu);
        
    }

}
