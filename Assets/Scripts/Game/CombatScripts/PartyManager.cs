 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Assertions;

 public class PartyManager : Singleton<PartyManager>
{
    public List<Creatures> m_InCombatParty;
    public List<Creatures> m_ReservePartymembers;

    public enum PartyTransfer
    {
        ReserveToInGame,
        InGameToReserve
    }


    // Use this for initialization
    public void Initialize()
    {
        m_ReservePartymembers.Add(gameObject.AddComponent<Sigma>());
        m_ReservePartymembers.Add(gameObject.AddComponent<Fide>());
        m_ReservePartymembers.Add(gameObject.AddComponent<Cavia>());
        m_ReservePartymembers.Add(gameObject.AddComponent<Vella>());


        foreach (Creatures ACreatures in m_ReservePartymembers)
        {
            ACreatures.Initialize();
        }
    }
    

    public void CombatEnd()
    {
        for (int i = 0; i < m_ReservePartymembers.Count; i++)
        {
            m_ReservePartymembers[i].CurrentHealth = m_ReservePartymembers[i].MaxHealth;
        }
    }

    public void RemoveReservePartyMember(int aCreatureIndex)
    {
        m_ReservePartymembers.RemoveAt(aCreatureIndex);
    }

    public void AddReserveToGame(Creatures aCreature, PartyTransfer aPartyTransfer)
    {
        
        
        switch (aPartyTransfer)
        {
            case PartyTransfer.InGameToReserve:

                SwapCreatureLists(aCreature, ref m_InCombatParty,  ref m_ReservePartymembers);
                break;
            
            case PartyTransfer.ReserveToInGame:
                
                SwapCreatureLists(aCreature, ref m_ReservePartymembers,  ref m_InCombatParty);
                break;
            
        }

        
    }

    public void SwapCreatureLists(Creatures aCreature, ref List<Creatures> aRemoveList , ref List<Creatures> aAddList)
    {
        Creatures TransferCreature = null;

        
        for(int i = 0 ; i < aRemoveList.Count ; i++)
        {
            if (aRemoveList[i] == aCreature)
            {
                TransferCreature = aRemoveList[i];
                
                aRemoveList.RemoveAt(i); 
                
                break;
            }
        }

        if (TransferCreature == null)
        {
            Debug.Assert(false ,"TransferedCreature is null");
        }
        else
        {
            aAddList.Add(TransferCreature);    
        }
    }

    public void ReserveToParty(int CurrentPartyPosition, int CurrentReservePosition)
    {
        Creatures TransferBuffer = m_InCombatParty[CurrentPartyPosition];
        m_InCombatParty[CurrentPartyPosition] = m_ReservePartymembers[CurrentReservePosition];
        m_ReservePartymembers[CurrentReservePosition] = TransferBuffer;

    }
}
