using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{
    void Start()
    {
        UiManager.Instance.PushScreen(UiManager.Screen.ArenaMenu);
    }
}
