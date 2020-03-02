using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    
    public GameObject m_DebugItems;
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            m_DebugItems.SetActive(true);
        }
    }
}
