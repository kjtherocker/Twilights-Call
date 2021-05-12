using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Move : Action
{
    private Creatures m_ActionCreature;
    private CombatNode m_ActionCommandNode;
    
    private CombatNode m_PreviousActionCommandNode;
    public Vector3 CreatureOffset;
    
    
    public void SetupAction(Creatures aActionCreature , CombatNode aActionCommandNode)
    {
        m_ActionCreature = aActionCreature;
        m_ActionCommandNode = aActionCommandNode;
        m_PreviousActionCommandNode = m_ActionCreature.m_CreatureAi.Node_ObjectIsOn; 
        
        CreatureOffset = new Vector3(0, Constants.Helpers.m_HeightOffTheGrid, 0);
        
        Debug.Log(m_ActionCreature.m_Name + " Moved into " + m_ActionCommandNode.m_PositionInGrid);
    }
    public override void Undo()
    {
        m_ActionCreature.m_CreatureAi.ReturnToInitalPosition();
    }
    
    public override void Actions() 
    {
        
         m_ActionCreature.m_CreatureAi.m_Position = m_ActionCommandNode.m_PositionInGrid;

         m_ActionCreature.m_CreatureAi.m_HasMovedForThisTurn = true;
         //m_ActionCommandNode.m_NodeIsCovered = true;
         
        CreatureOffset = 
            new Vector3(0, Constants.Helpers.m_HeightOffTheGrid + Grid.instance.GetNode(m_ActionCommandNode.m_PositionInGrid).m_NodeHeightOffset, 0);
        
        Grid.instance.GetNode(m_ActionCommandNode.m_PositionInGrid).m_CreatureOnGridPoint = m_ActionCreature;
        m_ActionCreature.m_CreatureAi.transform.position = Grid.instance.GetNode(m_ActionCommandNode.m_PositionInGrid).transform.position + CreatureOffset;

    }
}
