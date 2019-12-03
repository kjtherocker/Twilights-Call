using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : Singleton<EnemyList>
{
    public enum EnemyEnum
    {
        None,
        RedKnight1,
        RedKnight2,
        RedKnight3,
        RedKnight4
        
    }

    //public List<Skills> m_SkillTypes;

    public List<GameObject> m_Enemys;
    
    public GameObject ReturnEnemyData(EnemyEnum aEnemy, string sourceName = "Global")
    {
        return m_Enemys[(int)aEnemy];
    }

}
