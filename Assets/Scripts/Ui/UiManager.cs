﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public enum Screen
    {
        CommandBoard,
        SkillBoard,
        ArenaMenu,
        DomainBoard,
        PartyMenu,
        TurnIndicator,
        EndCombatMenu,
        Notifcation,
        PartyStats,
        Dialogue,


        _NumberOfScreens
    }

    public UiScreen[] m_UiScreens;

    public List<KeyValuePair<Screen, UiScreen>> m_ScreenStack = new List<KeyValuePair<Screen, UiScreen>>();
    
    public List<Screen> m_LastScreen = new List<Screen>();

    void OnValidate()
    {
        System.Array.Resize(ref m_UiScreens, (int)Screen._NumberOfScreens);
    }

    // Use this for initialization
    public void Start()
    {
        for (int i = 0; i < m_UiScreens.Length - 1; i++)
        {
            if (m_UiScreens[i] != null)
            {
                m_UiScreens[i].Initialize();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Screen GetTopScreenType()
    {
        return m_ScreenStack[m_ScreenStack.Count - 1].Key;
    }

    public UiScreen GetTopScreen()
    {
        return m_ScreenStack[m_ScreenStack.Count - 1].Value;
    }

    public UiScreen GetScreen(Screen aScreen)
    {
        for (int i = 0; i < m_ScreenStack.Count; i++)
        {
            if (m_ScreenStack[i].Key == aScreen)
            {
                return m_ScreenStack[i].Value;
            }
        }

        return null;
    }

    public void PushScreen(Screen aScreen)
    {
        if (m_ScreenStack.Count != 0)
        {
            m_ScreenStack[m_ScreenStack.Count - 1].Value.m_InputActive = true;
        }

        UiScreen screenToAdd = m_UiScreens[(int)aScreen];
        screenToAdd.OnPush();

        Debug.Log(screenToAdd.ToString());
        m_ScreenStack.Add(new KeyValuePair<Screen, UiScreen>(aScreen, screenToAdd));
        m_ScreenStack[m_ScreenStack.Count - 1].Value.m_InputActive = true;
    }

    public void PopScreen()
    {
        if (m_LastScreen.Count > 5)
        {
            m_LastScreen.RemoveAt(0);
        }
        m_LastScreen.Add(m_ScreenStack[m_ScreenStack.Count - 1].Key);
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
        m_ScreenStack.RemoveAt(m_ScreenStack.Count - 1);
       
    }

    public void PopScreenNoLastScreen()
    {
        if (m_ScreenStack.Count <= 0)
        {
            return;
        }

        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
        m_ScreenStack.RemoveAt(m_ScreenStack.Count - 1);
    }

    public void ReturnToLastScreen()
    {
        PopScreenNoLastScreen();
        PushScreen(m_LastScreen[m_LastScreen.Count - 1]);
        m_LastScreen.RemoveAt(m_LastScreen.Count - 1);
    }

    public void PopInvisivble()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPop();
    }

    public void PopAllInvisivble()
    {
        foreach (var screenPair in m_ScreenStack)
        {
            screenPair.Value.OnPop();
        }
    }

    public void PushToTurnOn()
    {
        m_ScreenStack[m_ScreenStack.Count - 1].Value.OnPush();
    }

    public void PopAllScreens()
    {
        foreach (var screenPair in m_ScreenStack)
        {
            UiScreen Screen = screenPair.Value;

            Screen.OnPop();
        }

        m_ScreenStack.Clear();
    }
}
