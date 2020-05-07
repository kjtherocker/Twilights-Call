using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class MovementList
{

    public enum MovementCategories
    {
        Normal,
        Flying
        
    }


    private Dictionary<MovementCategories, MovementType> m_MovementList;


    public void Initialize()
    {
        m_MovementList = new Dictionary<MovementCategories, MovementType>();
        
        m_MovementList.Add(MovementCategories.Normal, new Movement_Normal());
        m_MovementList.Add(MovementCategories.Flying, new Movement_Flying());
        
        
    }


    public MovementType ReturnMovementType(MovementCategories aMovementCategory)
    {
        return m_MovementList[aMovementCategory];
        
    }


}
