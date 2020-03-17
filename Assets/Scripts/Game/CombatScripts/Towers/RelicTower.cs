using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicTower : Relic
{
    
    public DomainList.DomainListEnum m_DomainType;
    public bool m_OnByDefault;
    public int LengthOfDomain;
    public bool m_TowerTakenOver;
    

    public override void Start()
    {
        CurrentHealth = 700;
        MaxHealth = 700;
        CurrentMana = 100;
        MaxMana = 200 ;

        Name = "RelicTower";

        AmountOfTurns = 0;

        m_TowerTakenOver = false;
        
        SetCreature();

        LengthOfDomain = 3;

        
        m_CreatureMovement = 8;

        charactertype = Charactertype.Undefined;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;
    }

    public void ActivateRelicTower(Domain aDomain)
    {
        if (m_TowerTakenOver == true)
        {
            return;
        }

        m_Domain = aDomain;
        m_Domain.Start();
        m_Domain.DomainUser = Name;
        DomainRecession();
        
        m_TowerTakenOver = true;

    }

    public void DomainRecession()
    {
        m_CreatureAi.Domain(8);
        LengthOfDomain--;
    }
}
