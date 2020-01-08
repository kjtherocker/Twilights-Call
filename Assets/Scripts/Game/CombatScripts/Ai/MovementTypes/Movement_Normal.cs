using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Normal : MovementType
{
    public override bool CheckIfNodeIsClearAndReturnNodeIndex(CombatNode aNode, Vector2Int m_Position)
    {
        // if the node is out of bounds, return -1 (an invalid tile index)

        if (aNode == null)
        {
            Debug.Log("YOU BROKE " + aNode.m_PositionInGrid.ToString());
        }

        CombatNode nodeIndex = aNode;

        // if the node is already closed, return -1 (an invalid tile index)
        if (nodeIndex.m_HeuristicCalculated == true)
        {
            return false;
        }
        // if the node can't be walked on, return -1 (an invalid tile index)
        if (nodeIndex.m_CombatsNodeType == CombatNode.CombatNodeTypes.Empty)
        {
            return false;
        }
        
        if (nodeIndex.m_CombatsNodeType == CombatNode.CombatNodeTypes.Wall)
        {
            return false;
        }
        
        if (nodeIndex.m_CombatsNodeType == CombatNode.CombatNodeTypes.Covered)
        {
            return false;
        }

        if (nodeIndex.m_PositionInGrid == m_Position)
        {
            return false;
        }


        if (nodeIndex.m_NodeHeight > 0)
        {
            return false;
        }
        // return a valid tile index
        return true;
    }
}
