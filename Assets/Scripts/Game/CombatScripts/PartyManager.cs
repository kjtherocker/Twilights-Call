 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : Singleton<PartyManager>
{
    public List<Creatures> m_CurrentParty;
    public List<Creatures> m_ReservePartymembers;
    

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

    public void ReserveToParty(int CurrentPartyPosition, int CurrentReservePosition)
    {
        Creatures TransferBuffer = m_CurrentParty[CurrentPartyPosition];
        m_CurrentParty[CurrentPartyPosition] = m_ReservePartymembers[CurrentReservePosition];
        m_ReservePartymembers[CurrentReservePosition] = TransferBuffer;

    }
}
