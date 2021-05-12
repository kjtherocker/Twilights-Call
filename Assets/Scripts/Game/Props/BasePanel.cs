using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            ActivateCommandPanel();
        }
    }

    public void ActivateCommandPanel()
    {
        
        UiManager.instance.PushScreen(UiManager.Screen.BasePanel);
        
    }


}