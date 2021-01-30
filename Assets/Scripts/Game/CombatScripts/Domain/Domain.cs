using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


[System.Serializable]
public class Domain : Skills
{
    public enum DomainType
    {
        OneTime,
        EndOfTurn
    }
    // Start is called before the first frame update


    // Use this for initialization


    public string DomainName;
    public DomainType Domaintype;
    public string DomainUser;
    public string DomainDescription;
    public Material m_DomainTexture;
    public Creatures m_Creature;
    public float m_CostToUse;
    public virtual void Start()
    {
        m_ElementalType = Skills.ElementalType.Null;
        DomainName = "Not Initalized";
        DomainDescription = "Something bad happend";
        DomainUser = "Mr Messup";
    }

    public void OnLoadDomainMaterial(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Material> obj)
    {
        m_DomainTexture = obj.Result;
    }

    
    public virtual void DomainEffect(ref Creatures m_CreatureOnDomain)
    {
    }
    
    public virtual void UndoDomainEffect(ref Creatures m_CreatureOnDomain)
    {
    }

    
    
    /// <summary>
    /// Method used for when there is something additional after the Domain Effect is used
    /// </summary>
    public virtual void AdditionalDomainEffects()
    {
        
    }

    public virtual bool CheckIfNodeIsClearAndReturnNodeIndex(CombatNode aNode, Vector2Int m_Position)
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

        if (nodeIndex.m_DomainCombatNode == CombatNode.DomainCombatNode.Domain)
        {
            return false;
        }

        // if the node can't be walked on, return -1 (an invalid tile index)
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

}
