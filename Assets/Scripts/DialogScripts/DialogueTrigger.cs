using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CharacterInCutsceneReferences
{
    public Creatures m_CharacterModel;
    public string m_CharacterName;
    public GameObject m_SpawnPosition;
    public bool m_SpawnOnStart;


    public void Initalize()
    {
       m_CharacterName = m_CharacterModel.Name;
    }
}



[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{

    public enum TriggerType
    {
        Default,
        Menu,
        WaitForObjectToCome,
        OnAwake
    }





    public List<Dialogue> m_Dialogue;

    public List<CharacterInCutsceneReferences> m_CharactersInCutscene; 
    public DialogueManager m_DialogueManager;
    public bool DialogueHasHappend;


    public UiManager.Screen m_UiScreen;

    public DialogueManager.DialogueType m_DialogueType;
    
    public bool DeleteStartAfterStart;
    public bool DeleteEndOnEnd;
    public bool UseStartObjectFulltime;
    
    public AudioClip m_Audioclip;

    public TextAsset m_JsonFile;

    public GameObject m_CutsceneArea;

    public TriggerType m_TriggerType;

    public bool DialogueIsDone;

    public OverWorldPlayer m_BasePlayer;

    public void Start()
    {
        DialogueHasHappend = false;
        //DeSerializeJsonDialogue(m_JsonFile);

        if (m_TriggerType == TriggerType.OnAwake)
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (DialogueHasHappend == false)
            {
                if (m_TriggerType == TriggerType.Default)
                {
                    TriggerDialogue();
                }
                else if (m_TriggerType == TriggerType.Menu)
                {
                    UiManager.Instance.PushScreen(m_UiScreen);
                    m_BasePlayer.m_IsInMenu = true;
                }
                else if (m_TriggerType == TriggerType.WaitForObjectToCome)
                {
                }
            }
        }
    }

    public void DeSerializeJsonDialogue(TextAsset a_JsonFile)
    {
        m_Dialogue.Clear();

        Dialogue[] m_DialogueFromJson = JsonHelper.FromJson<Dialogue>(a_JsonFile.text);

        for (int i = 0; i < m_DialogueFromJson.Length; i++)
        {
            m_Dialogue.Add(m_DialogueFromJson[i]);
            m_Dialogue[i].Initalize();
        }

    }


    public void TriggerDialogue()
    {

        Instantiate(m_CutsceneArea);
        DeSerializeJsonDialogue(m_JsonFile);
        AudioManager.Instance.PlaySoundRepeating(m_Audioclip);
        
        
        
        m_DialogueManager.m_DialogueTrigger = this;
        m_DialogueManager.StartDialogue(m_Dialogue,m_DialogueType);
        DialogueHasHappend = true;
        DialogueIsDone = false;
    }

}
