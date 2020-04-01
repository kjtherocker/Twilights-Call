﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{


    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
        UiManager.Instance.PushScreen(UiManager.Screen.ArenaMenu);
    }

    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
    }
}
