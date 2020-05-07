using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeFormations 
{



    public List<Vector2Int> NodeFormation()
    {
        List<Vector2Int> TempNodeFormationList;
        TempNodeFormationList = new List<Vector2Int>();

        TempNodeFormationList.Add(new Vector2Int(0, 0));
        TempNodeFormationList.Add(new Vector2Int(0, 1));
        TempNodeFormationList.Add(new Vector2Int(0, -1));
        TempNodeFormationList.Add(new Vector2Int(1, 0));
        TempNodeFormationList.Add(new Vector2Int(-1, 0));
        TempNodeFormationList.Add(new Vector2Int(4, 0));


        return TempNodeFormationList;

    }

}