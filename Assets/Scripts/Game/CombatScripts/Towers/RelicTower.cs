using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicTower : Relic
{
    
    public DomainList.DomainListEnum m_DomainType;
    public bool m_OnByDefault;

    public override void Start()
    {
        CurrentHealth = 700;
        MaxHealth = 700;
        CurrentMana = 100;
        MaxMana = 200 ;

        Name = "RelicTower";

        AmountOfTurns = 0;
        
        SetCreature();


        m_DomainList = DomainList.DomainListEnum.PatchworkChimera;
        
        m_Domain = new Domain_PatchWorkChimera();
        m_Domain.Start();
        m_Domain.DomainUser = Name;


        charactertype = Charactertype.Undefined;
        elementalStrength = ElementalStrength.Fire;
        elementalWeakness = ElementalWeakness.Null;
    }
}
