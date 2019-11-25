using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public enum EnemyEnum
    {
        RedKnight1,
        RedKnight2,
        RedKnight3,
        RedKnight4
        
    }

    //public List<Skills> m_SkillTypes;

    public Dictionary<int, EnemyEnum> m_Enemys = new Dictionary<int, EnemyEnum>();
    // Use this for initialization
    void Start ()
    {

    }

}
