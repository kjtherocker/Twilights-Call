using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainList : Singleton<DomainList>
{

    public enum DomainListEnum
    {
        PatchworkChimera
    }

    //public List<Skills> m_SkillTypes;

    public Dictionary<int, Domain> m_DomainTypes = new Dictionary<int, Domain>();
    // Use this for initialization
    void Start ()
    {

        
        m_DomainTypes.Add((int)DomainListEnum.PatchworkChimera, new Domain_PatchWorkChimera());
        
        //for (int i = 0; i < m_DomainTypes.Count; i++)
        //{
        //    m_DomainTypes[i].Start();
        //    
        //}
       // Debug.Log(m_SkillTypes[(int)Skills.HolyWater].GetSkillType().ToString());
    }
	


    public Domain SetDomain(DomainListEnum aDomain, string sourceName = "Global")
    {
       return m_DomainTypes[(int)aDomain];
    }
}
