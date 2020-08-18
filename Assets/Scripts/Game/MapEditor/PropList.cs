using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;


[ExecuteInEditMode]
public class PropList
{

    public enum Props
    {
        None,

        //Trees
        Tree1,
        Tree2,
        Tree3,

        //Pillars
        Angled_Top_Broken_Pillar,
        Fully_Intact_Pillar,
        Middle_Broken_Pillar_1,
        Middle_Broken_Pillar_2,
        Middle_Dented_Pillar,
        Pillar_Stub,
        Top_Broken_Pillar,

        //Bridges
        StartBridge,
        MiddleBridge,
        EndBridge,

        //Ruin Walls

        Corner_Ruin_Wall,
        Modular_Wall_End,
        Modular_Wall_Mid1,
        Modular_Wall_Mid2,
        Modular_Wall_Mid3,
        Modular_Wall_Mid4,
        Modular_Wall_Mid5,
        Modular_Wall_Start,
        Singular_Ruin_Wall,



        //End
        NumberOfProps
    }

    public enum NodeReplacements
    {
        None,

        BridgeStart,
        BridgeMiddle,
        BridgeEnd,

        Stairs,
        

        //End
        NumberOfProps
    }



    //public List<GameObject> m_PropSet;

    private Dictionary <Props, GameObject> m_Props = new Dictionary <Props, GameObject>();
    private List<NodeReplacement> m_NodeReplacements;
    // Start is called before the first frame update
    public void Initialize()
    {

        Debug.Log("Initialized");
        //Tree
        AddPropToDictionary(Props.Tree1,
            "Objects/Props/Tree/Prefab/3D_Tree_1");
        
        AddPropToDictionary(Props.Tree2,
            "Objects/Props/Tree/Prefab/3D_Tree_2");
        
        AddPropToDictionary(Props.Tree3,
            "Objects/Props/Tree/Prefab/3D_Tree_3");
        
        
        //Bridge
        
        AddPropToDictionary(Props.EndBridge,
            "Objects/Props/Bridge/Prefab/3D_Bridge_End");
        
        AddPropToDictionary(Props.MiddleBridge,
            "Objects/Props/Bridge/Prefab/3D_Bridge_Middle");
        
        AddPropToDictionary(Props.EndBridge,
            "Objects/Props/Bridge/Prefab/3D_Bridge_Start");
        
        
        //Pillar
        
        AddPropToDictionary(Props.Pillar_Stub,
            "Objects/Props/Pillars/Models/3D_Pillar_Stub");
        
        AddPropToDictionary(Props.Fully_Intact_Pillar,
            "Objects/Props/Pillars/Models/3D_Fully_Intact_Pillar");
        
        AddPropToDictionary(Props.Middle_Broken_Pillar_1,
            "Objects/Props/Pillars/Models/3D_Middle_Broken_Pillar_1");
        
        AddPropToDictionary(Props.Middle_Broken_Pillar_2,
            "Objects/Props/Pillars/Models/3D_Middle_Broken_Pillar_2");
        
        AddPropToDictionary(Props.Middle_Dented_Pillar,
            "Objects/Props/Pillars/Models/3D_Middle_Dented_Pillar");
       
        
        //Wall
        AddPropToDictionary(Props.Modular_Wall_End,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Modular_Wall_End");
        
        AddPropToDictionary(Props.Corner_Ruin_Wall,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Corner_Ruin_Wall");
        
        AddPropToDictionary(Props.Modular_Wall_Mid1,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Modular_Wall_Mid1");
        
        AddPropToDictionary(Props.Modular_Wall_Mid2,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Modular_Wall_Mid2");
        
        AddPropToDictionary(Props.Modular_Wall_Mid3,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Modular_Wall_Mid3");
        
        AddPropToDictionary(Props.Modular_Wall_Mid4,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Modular_Wall_Mid4");
        
        AddPropToDictionary(Props.Modular_Wall_Mid5,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Modular_Wall_Mid5");
            
        AddPropToDictionary(Props.Modular_Wall_Start,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Modular_Wall_Start");
        
        AddPropToDictionary(Props.Singular_Ruin_Wall,
            "Objects/Props/Walls/RuinWalls/Prefab/3D_Singular_Ruin_Wall");
        
    }


    public void AddPropToDictionary(Props aPropType, string aPath)
    {
        if (m_Props.ContainsKey(aPropType) )
        {
            Debug.Log("Prop Type " + aPropType + " is already initialized");
            return;
            
        }

        GameObject tempgameobject = Resources.Load<GameObject>(aPath);
        
        Debug.Log("The prop we got from the resources was " + tempgameobject.name);
        
        m_Props.Add(aPropType, tempgameobject);
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            
           // Debug.Log("This is what testo is " + m_Testo);
        }
    }

   public IEnumerator LoadProps()
   {
    //  m_PropSet = new List<Prop>();
    //   yield return Addressables.LoadAssetsAsync<Prop>("prop", ob =>
    //   {
    //       
    //       Prop tempProp = ob.GetComponent<Prop>();
    //       
    //       m_PropSet.Add(tempProp);
    //       
    //   });
    
        yield break;
   }


    public GameObject ReturnPropData(Props aProp, string sourceName = "Global")
    {
        
        Debug.Log("Prop Found " + m_Props[aProp]);
        if (m_Props.ContainsKey(aProp))
        {
            return m_Props[aProp];
        }

        return null;
    }

    public NodeReplacement NodeReplacementData(NodeReplacements aProp, string sourceName = "Global")
    {
        return m_NodeReplacements[(int)aProp];
    }
}
