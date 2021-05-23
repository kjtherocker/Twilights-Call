using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    // Start is called before the first frame update

    public CombatNode m_AttachedCombatnode;
    
    
    void Start()
    {
        m_AttachedCombatnode.SetWalkedOnTopCallBack(ReAddCreatureToPartyManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            ActivateCommandPanel();
        }
    }

    public void ReAddCreatureToPartyManager(Creatures aCreatures)
    {
        
        PartyManager.instance.AddReserveToGame(aCreatures, PartyManager.PartyTransfer.InGameToReserve);
        Debug.Log("Creature went down");

        Destroy(aCreatures.ModelInGame);

    }


    public void ActivateCommandPanel()
    {
        
        UiManager.instance.PushScreen(UiManager.Screen.BasePanel);
        
    }


}
