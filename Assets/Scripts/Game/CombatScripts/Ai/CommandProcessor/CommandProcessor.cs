﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor : Singleton<CommandProcessor>
{
    public List<Action> m_ActionsStack;
    
    public List<Action> m_LastActionsStack;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            UndoTopMostAction();
        }
        
        if (Input.GetKeyDown("e"))
        {
            UndoTopMostAction();
        }

    }

    public void AddActionToList( Action aAction)
    {
        m_ActionsStack.Add(aAction);
    }
    
    public void UndoTopMostAction()
    {
        m_ActionsStack[m_ActionsStack.Count - 1].Undo();
        m_LastActionsStack.Add(m_ActionsStack[m_ActionsStack.Count - 1]);
        m_ActionsStack.RemoveAt(m_ActionsStack.Count - 1);
    }
    
    public void RedoLastAction() 
    {
        m_ActionsStack[m_ActionsStack.Count - 1].Actions();
        m_ActionsStack.RemoveAt(m_ActionsStack.Count - 1);
    }
}
