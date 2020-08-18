using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class EnemyList 
{
    public enum EnemyTypes
    {
        None,
        RedKnight1,
        RedKnight2,
        RedKnight3,
        RedKnight4
        
    }

    //public List<Skills> m_SkillTypes;

   
    private Dictionary <EnemyTypes, GameObject> m_Enemys = new Dictionary <EnemyTypes, GameObject>();
    public void Initialize()
    {
        
        AddEnemyToDictionary(EnemyTypes.RedKnight1,
            "Objects/Battle/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase1");
        
        AddEnemyToDictionary(EnemyTypes.RedKnight2,
            "Objects/Battle/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase2");
        
        AddEnemyToDictionary(EnemyTypes.RedKnight3,
            "Objects/Battle/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase3");

        AddEnemyToDictionary(EnemyTypes.RedKnight4,
            "Objects/Battle/Enemy/Forest/RedKnights/Prefabs/Pref_RedKnight_Phase4");
    }
    public void AddEnemyToDictionary(EnemyTypes aEnemyType, string aPath)
    {
        if (m_Enemys.ContainsKey(aEnemyType) )
        {
            Debug.Log("Prop Type " + aEnemyType + " is already initialized");
            return;
            
        }

        GameObject tempgameobject = Resources.Load<GameObject>(aPath);
        
        Debug.Log("The prop we got from the resources was " + tempgameobject.name);
        
        m_Enemys.Add(aEnemyType, tempgameobject);
    }



    public GameObject ReturnEnemyData(EnemyTypes aEnemyType, string sourceName = "Global")
    {
        
        Debug.Log("Prop Found " + m_Enemys[aEnemyType]);
        if (m_Enemys.ContainsKey(aEnemyType))
        {
            return m_Enemys[aEnemyType];
        }

        return null;
    }

}
