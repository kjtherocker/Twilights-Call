using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memoria : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Skills> m_Skills;
    public bool InUse;
    public Vector2Int m_NodePosition;
    public void Initialize()
    {
        InUse = false;
    }
    
    public void AttachSkills(List<Skills> aSkills)
    {
        InUse = true;
        m_Skills = aSkills;
    }

    public void DestroyMemoria()
    {

        Grid.Instance.GetNode(m_NodePosition).m_WalkOnTopTriggerTypes = CombatNode.WalkOntopTriggerTypes.None;
        gameObject.SetActive(false);
    }

}
