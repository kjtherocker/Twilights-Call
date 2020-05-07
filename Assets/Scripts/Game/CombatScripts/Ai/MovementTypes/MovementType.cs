using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class MovementType
{
    public virtual bool CheckIfNodeIsClearAndReturnNodeIndex(CombatNode aNode, Vector2Int m_Position)
    {

        return true;
    }
}
