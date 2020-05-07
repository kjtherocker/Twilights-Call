using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Behaviour
{
    public virtual Creatures AllyToAttack(List<Creatures> aCharacterList)
    {

//        for (int i = 0; i < aCharacterList.Count; i++)
//        {
//            for (int j = 0; j < aCharacterList.Count; j++)
//            {
//                if (aCharacterList[j].CurrentHealth < aCharacterList[j + 1].CurrentHealth)
//                {
//                    Creatures tempA = aCharacterList[j];
//                    Creatures tempB = aCharacterList[j + 1];
//                    swap(ref tempA, ref tempB);
//                }
//            }
//        }

        return aCharacterList[0];
    }
    
    protected void swap(ref Creatures xp, ref Creatures yp)
    {
        Creatures temp = xp;
        xp = yp;
        yp = temp;
    }
    
    public virtual bool CheckIfNodeIsClearForRange(CombatNode aNode)
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

        if (nodeIndex.m_CombatsNodeType == CombatNode.CombatNodeTypes.Wall)
        {
            return false;
        }
        
        if (nodeIndex.m_CombatsNodeType == CombatNode.CombatNodeTypes.Empty)
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
    
    public virtual Dictionary<CombatNode, Dictionary<CombatNode, int>> GetGraphRangeEdges(List<CombatNode> NodeList, CombatNode ANodeObjectIsOn)
    {
        Dictionary<CombatNode, Dictionary<CombatNode, int>> ret = new Dictionary<CombatNode, Dictionary<CombatNode, int>>();

        foreach (CombatNode Node in NodeList)
        {
            if (CheckIfNodeIsClearForRange(Node) == true || Node.Equals(ANodeObjectIsOn))
            {
                ret[Node] = new Dictionary<CombatNode, int>();
                foreach (CombatNode neighbour in Node.GetNeighbours(NodeList))
                {
                    if (CheckIfNodeIsClearForRange(neighbour) == true)
                    {
                        ret[Node][neighbour] = neighbour.m_MovementCost;
                    }
                }
            }
        }
        return ret;
    }
}
