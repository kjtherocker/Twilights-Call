using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;


[ExecuteInEditMode]
public class PropList : Singleton<PropList>
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
    public  List<Prop> m_PropSet = new List<Prop>();
    public List<NodeReplacement> m_NodeReplacements;
    // Start is called before the first frame update
    void Start()
    {

       // Addressables.LoadAssetAsync<Prop>("Tree1").Completed += OnLoadDone;
        StartCoroutine(LoadProps());

    }


    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Prop> obj)
    {
        m_PropSet.Add(obj.Result);

    }


    // Update is called once per frame
    void Update()
    {
        
    }

   public IEnumerator LoadProps()
   {
       m_PropSet = new List<Prop>();
        yield return Addressables.LoadAssetsAsync<Prop>("default", ob =>
        {
            
            Prop tempProp = ob.GetComponent<Prop>();
            
            m_PropSet.Add(tempProp);
            
        });
   }


    public GameObject ReturnPropData(Props aProp, string sourceName = "Global")
    {

        for (int i = 0; i < m_PropSet.Count() - 1; i++)
        {
            if (m_PropSet[i].m_Prop == aProp)
            {
                return m_PropSet[i].gameObject;
            }
        }

        Debug.Log("Couldnt find " + aProp);
        
        return null;
 
    }

    public NodeReplacement NodeReplacementData(NodeReplacements aProp, string sourceName = "Global")
    {
        return m_NodeReplacements[(int)aProp];
    }
}
