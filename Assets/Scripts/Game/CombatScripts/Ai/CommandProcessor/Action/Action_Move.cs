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
        
        CreatureOffset = new Vector3(0, Constants.Constants.m_HeightOffTheGrid, 0);
        
        Debug.Log(m_ActionCreature.Name + " Moved into " + m_ActionCommandNode.m_PositionInGrid);
    }
    public override void Undo()
    {
        m_ActionCreature.m_CreatureAi.ReturnToInitalPosition();
    }
    
    public override void Actions() 
    {
        
         m_ActionCreature.m_CreatureAi.m_Position = m_ActionCommandNode.m_PositionInGrid;

         m_ActionCreature.m_CreatureAi.m_HasMovedForThisTurn = true;
         m_ActionCommandNode.m_CombatsNodeType = CombatNode.CombatNodeTypes.Covered;
         
        CreatureOffset = 
            new Vector3(0, Constants.Constants.m_HeightOffTheGrid + GameManager.Instance.m_Grid.GetNode(m_ActionCommandNode.m_PositionInGrid).m_NodeHeightOffset, 0);
        
        GameManager.Instance.m_Grid.GetNode(m_ActionCommandNode.m_PositionInGrid).m_CreatureOnGridPoint = m_ActionCreature;
        m_ActionCreature.m_CreatureAi.transform.position = GameManager.Instance.m_Grid.GetNode(m_ActionCommandNode.m_PositionInGrid).transform.position + CreatureOffset;

    }
}
