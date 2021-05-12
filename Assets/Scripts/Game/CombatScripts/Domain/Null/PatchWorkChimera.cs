using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PatchWorkChimera : Domain
{
    private Domain m_DomainCopy;
    private List<Creatures> m_CreaturesToCopy;
    public override void Start()
    {
        m_ElementalType = Skills.ElementalType.Null;
        m_SkillType = SkillType.Domain;


        m_SkillRange = 4;
        SkillName = "Patchwork Chimera";
        SkillDescription = "Steal the effect of another Domain";
        DomainUser = "";
        Domaintype = DomainType.OneTime;

        m_CreaturesToCopy = new List<Creatures>();
        Addressables.LoadAssetAsync<Material>("ChimeraFloor").Completed += OnLoadDomainMaterial;
        
    }

    public override void DomainEffect(ref Creatures m_CreatureOnDomain)
    {
        if (m_DomainCopy != null)
        {
            m_DomainCopy.DomainEffect(ref m_CreatureOnDomain);
        }
        
        m_CreaturesToCopy.Add(m_CreatureOnDomain); 


    }

    public override void UndoDomainEffect(ref Creatures m_CreatureOnDomain)
    {

    }
    
    public override void AdditionalDomainEffects()
    {

        float test = 0;
        //Here we can push a different screen with our copies
    }
}
